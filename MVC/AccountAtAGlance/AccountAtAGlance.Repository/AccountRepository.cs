using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Configuration;
using AccountAtAGlance.Model;

namespace AccountAtAGlance.Repository
{

    public class AccountRepository : RepositoryBase<AccountAtGlance>, IAccountRepository
    {
        readonly bool _LocalDataOnly = Boolean.Parse(ConfigurationManager.AppSettings["LocalDataOnly"]);
        public Customer GetCustomer(string custId)
        {
            using (var context = DataContext)
            {
                return context.Customers
                        .Include("BrokerageAccounts")
                        .Where(c => c.CustomerCode == custId).SingleOrDefault();
            }
        }

        public BrokerageAccount GetAccount(string acctNumber)
        {
            return UpdateAccount(acctNumber);
        }

        private BrokerageAccount UpdateAccount(string acctNumber)
        {
            //Force update of security values
            ISecurityRepository securityRepo = new SecurityRepository();
            securityRepo.UpdateSecurities();

            BrokerageAccount acct = null;
            acct = DataContext.BrokerageAccounts
                .Include("Orders")
                .Include("WatchList")
                .Include("WatchList.Securities")
                .Include("Positions")
                .Include("Positions.Security")
                .Where(ba => ba.AccountNumber == acctNumber).SingleOrDefault();

            if (acct != null && acct.Positions != null && !_LocalDataOnly)
            {
                acct.Positions = acct.Positions.OrderBy(p => p.Total).ToList();

                //Get account position securities
                var securities = acct.Positions.Select(p => p.Security).Distinct().ToList();

                var positions = acct.Positions;
                foreach (var pos in positions)
                {
                    pos.Total = pos.Shares * pos.Security.Last;
                    DataContext.Entry(pos).State = System.Data.Entity.EntityState.Modified;
                }
                acct.PositionsTotal = acct.Positions.Sum(p => p.Total);
                acct.Total = acct.PositionsTotal + acct.CashTotal;

                DataContext.Entry(acct).State = System.Data.Entity.EntityState.Modified;

                try
                {
                    DataContext.SaveChanges();
                }
                catch
                {
                    //If we fail simply continue for sample app
                }
            }

            return acct;
        }

        public BrokerageAccount GetAccount(int id)
        {
            using (var context = DataContext)
            {
                return context.BrokerageAccounts
                        .Include("Orders")
                        .Include("WatchList")
                        .Include("Positions")
                        .Where(ba => ba.Id == id).SingleOrDefault();
            }
        }

        public OperationStatus RefreshAccountsData()
        {
            var opStatus = new OperationStatus { Status = true};
            using (var ts = new TransactionScope())
            {
                using (var context = DataContext)
                {
                    var securities = context.Securities.ToList();
                    try
                    {
                        //Delete existing account info
                        opStatus.Status = context.DeleteAccounts() >= 0;

                        if (opStatus.Status)
                        {
                            var cust = new Customer
                            {
                                FirstName = "Marcus",
                                LastName = "Hightower",
                                Address = "1234 Anywhere St.",
                                City = "Phoenix",
                                State = "AZ",
                                Zip = 85229,
                                CustomerCode = "C15643",
                            };

                            AddBrokerageAccounts(securities, cust);
                            context.Customers.Add(cust);


                            opStatus.Status = context.SaveChanges() > 0;
                        }
                    }
                    catch (Exception exp)
                    {
                        opStatus = OperationStatus.CreateFromException("Error updating security exchange: " + exp.Message, exp);
                    }
                }

                if (opStatus.Status) ts.Complete();

            } //end transactionscope

            return opStatus;
        }

        private static void AddBrokerageAccounts(List<Security> securities, Customer cust)
        {
            string[] accountTitles = { "IRA", "Joint Brokerage", "Brokerage Account" };
            for (int i = 0; i < accountTitles.Length; i++)
            {
                var acct = new BrokerageAccount
                {
                    AccountNumber = "Z48573988" + i.ToString(),
                    AccountTitle = accountTitles[i],
                    IsRetirement = (i==0)?true:false,
                    CashTotal = (i + 1) * 5000,
                    WatchList = new WatchList { Title = "My Watch Securities" }
                };

                FillAccountSecurities(securities, acct, i);

                acct.PositionsTotal = acct.Positions.Sum(p => p.Total);
                acct.Total = acct.PositionsTotal + acct.CashTotal;
                acct.MarginBalance = (acct.IsRetirement) ? 0.00M : Math.Round(acct.Total / 3, 2);

                cust.BrokerageAccounts.Add(acct);
            }
        }

        private static void FillAccountSecurities(List<Security> securities, BrokerageAccount acct, int seed)
        {
            var rdm = new Random((int)DateTime.Now.Ticks + seed);
            for (int index = 0; index < 10; index++)
            {
                int pos = rdm.Next(securities.Count - 1);
                var sec = securities[pos];
                if (!acct.Positions.Any(p => p.Security.Symbol == sec.Symbol))
                {
                    var multiplier = (pos == 0) ? 1 : pos;
                    var shares = multiplier * 100;
                    var total = shares * sec.Last;
                    acct.Positions.Add(new Position { Security = sec, Shares = shares, Total = total });
                }
            }

            var watchListSecs = securities.Where(s => !acct.Positions.Any(p => p.SecurityId == s.Id));
            if (watchListSecs.Count() > 10) watchListSecs = watchListSecs.Take(8);

            foreach (var watchSec in watchListSecs)
            {
                acct.WatchList.Securities.Add(watchSec);
            }
        }
    }
}
