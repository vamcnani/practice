using System;
using System.Collections.Generic;
using System.Linq;
using AccountAtAGlance.Repository.Helpers;
using System.Transactions;
using System.Configuration;
using AccountAtAGlance.Model;

namespace AccountAtAGlance.Repository
{
    public class SecurityRepository : RepositoryBase<AccountAtGlance>, ISecurityRepository
    {
        bool localDataOnly = Boolean.Parse(ConfigurationManager.AppSettings["LocalDataOnly"]);
        //Some random symbols to use in order to get data into the database
        private readonly string[] _StockSymbols = {"AMZN", "BAC", "C", "DIS", "EMC", "FDX", "GE", "H", "INTC", "JPM", "K", 
                                                   "LLY", "MSFT", "NKE", "ORCL", "PG", "Q", "RBS", "S", "T", "UL", "V", "WMT", 
                                                   "XRX", "YHOO", "ZION", "AAPL", "IBM", "NOK", "CSCO", "FCX", "MTH", "SPF", 
                                                   "CRM", "CAT", "LMT", "GD", "XOM", "CVX", "SLB", "BA", "F", "X", "AA", 
                                                   "NOC", "RTN","FMAGX", "FDGFX", "FCNTX", "GOOG", "ITRGX", "EBAY", "AOL", "BIDU" };

        public Security GetSecurity(string symbol)
        {
            using (DataContext)
            {
                var sec = DataContext.Securities.Where(s => s.Symbol == symbol).SingleOrDefault();
                if (sec == null)
                {
                    var engine = new StockEngine();
                    sec = engine.GetSecurityQuotes(symbol).FirstOrDefault();
                    DataContext.Securities.Add(sec);
                    var opStatus = Save(sec);
                    if (!opStatus.Status)
                    {
                        sec = new Stock { Company = "Error getting quote." };
                    }
                }
                if (sec is Stock)
                {
                    sec.DataPoints = new DataSimulator().GetDataPoints(sec.Last);
                }
                return sec;
            }
        }

        public List<TickerQuote> GetSecurityTickerQuotes()
        {
            using (DataContext)
            {
                return DataContext.Securities.Select(s => 
                    new TickerQuote 
                    { 
                        Symbol = s.Symbol, 
                        Change = s.Change, 
                        Last = s.Last 
                    }).OrderBy(tq => tq.Symbol).ToList();
            }
        }

        public OperationStatus UpdateSecurities()
        {
            var opStatus = new OperationStatus { Status = true };
            if (localDataOnly) return opStatus;

            var securities = DataContext.Securities; //Get existing securities
            var engine = new StockEngine();
            var updatedSecurities = engine.GetSecurityQuotes(securities.Select(s => s.Symbol).ToArray());
            //Return if updatedSecurities is null
            if (updatedSecurities == null) return new OperationStatus { Status = false};
            
            foreach (var security in securities)
            {
                //Grab updated version of security
                var updatedSecurity = updatedSecurities.Where(s => s.Symbol == security.Symbol).Single();
                security.Change = updatedSecurity.Change;
                security.Last = updatedSecurity.Last;
                security.PercentChange = updatedSecurity.PercentChange;
                security.RetrievalDateTime = updatedSecurity.RetrievalDateTime;
                security.Shares = updatedSecurity.Shares;
                DataContext.Entry(security).State = System.Data.Entity.EntityState.Modified;
            }

            //Insert records
            try
            {
                DataContext.SaveChanges();
            }
            catch (Exception exp)
            {
                return OperationStatus.CreateFromException("Error updating security quote.", exp);
            }
            return opStatus;
        }

        public OperationStatus InsertSecurityData()
        {
            var engine = new StockEngine();
            var securities = engine.GetSecurityQuotes(_StockSymbols);
            var exchanges = securities.OfType<Stock>().Select(s => s.Exchange.Title).Distinct(); 

            if (securities != null && securities.Count > 0)
            {
                using (var ts = new TransactionScope())
                {
                    using (DataContext)
                    {
                        var opStatus = DeleteSecurityRecords(DataContext);
                        if (!opStatus.Status) return opStatus;

                        opStatus = InsertExchanges(exchanges, DataContext);
                        if (!opStatus.Status) return opStatus;

                        opStatus = InsertSecurities(securities, DataContext);
                        if (!opStatus.Status) return opStatus;
                    }
                    ts.Complete();
                }
            }
            return new OperationStatus { Status = true };
        }

        private static OperationStatus InsertSecurities(List<Security> securities, AccountAtGlance context)
        {
            foreach (var security in securities)
            {                        
                //Update stock's exchange ID so we don't get dups
                if (security is Stock)
                {
                    var stock = (Stock)security;
                    stock.Exchange = context.Exchanges.Where(e => e.Title == stock.Exchange.Title).First();
                }
                if (security is MutualFund)
                {
                    ((MutualFund)security).MorningStarRating = 4;
                }
                //Add security into collection and then insert into DB
                context.Securities.Add(security);
            }

            //Insert records
            try
            {
                context.SaveChanges();
            }
            catch (Exception exp)
            {
                return OperationStatus.CreateFromException("Error updating security quote.", exp);
            }
            return new OperationStatus { Status = true };
        }

        private OperationStatus InsertExchanges(IEnumerable<string> exchanges, AccountAtGlance context)
        {
            //Insert Exchanges
            foreach (var exchange in exchanges)
            {
                context.Exchanges.Add(new Exchange { Title = exchange });
            }
            try
            {
                context.SaveChanges(); //Save exchanges so we can get their IDs
            }
            catch (Exception exp)
            {
                return OperationStatus.CreateFromException("Error updating security exchange.", exp);
            }
            return new OperationStatus { Status = true };
        }

        private OperationStatus DeleteSecurityRecords(AccountAtGlance context)
        {
            var opStatus = new OperationStatus { Status = false };
            try
            {
                opStatus.Status = context.DeleteSecuritiesAndExchanges() == 0;
            }
            catch (Exception exp)
            {
                return OperationStatus.CreateFromException("Error deleting security/exchange data.", exp);
            }
            return new OperationStatus { Status = true };
        }
    }
}
