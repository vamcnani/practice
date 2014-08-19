using Client.EvalServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //CallEvalService();
            //CallEvalServiceChannel();
            //CallEvalServiceClient();
            //CallEvalServiceClientAsync();
            CallEvalServiceByMetadataResolver();
            Console.ReadLine();
        }

        private static void CallEvalServiceByMetadataResolver()
        {
            ServiceEndpointCollection endpoints =
                MetadataResolver.Resolve(typeof(IEvalService),
                new EndpointAddress("http://localhost:8080/evals/mex"));

            Console.WriteLine("Retrieving endpoints via MEX");
            foreach (var se in endpoints)
            {
                EvalServiceClient channel = new EvalServiceClient(se.Binding, se.Address);

                Eval eval = new Eval
                {
                    Submitter = "Vamsi",
                    Comments = "This is test from eval service client",
                    TimeSent = DateTime.Now
                };

                try
                {
                    channel.SubmitEval(eval);
                    channel.SubmitEval(eval);
                    List<Eval> evals = channel.GetEvals();
                    Console.WriteLine("{0}-Number of evals:{1}", se.Name.Substring(0,10), evals.Count);
                    channel.Close();
                }
                catch (FaultException fe)
                {
                    Console.WriteLine("FaultException Handler: {0}", fe.GetType());
                    channel.Abort();
                }
                catch (CommunicationException ce)
                {
                    Console.WriteLine("CommunicationException Handler: {0}", ce.GetType());
                    channel.Abort();
                }
                catch (TimeoutException te)
                {
                    Console.WriteLine("TimeoutException Handler: {0}", te.GetType());
                    channel.Abort();
                }  
            }
        }

        private static void CallEvalServiceClientAsync()
        {
            EvalServiceClient channel = new EvalServiceClient("WSHttpBinding_IEvalService");
            Eval eval = new Eval
            {
                Submitter = "Throw ",
                Comments = "This is test from eval service client",
                TimeSent = DateTime.Now
            };

            try
            {
                channel.SubmitEval(eval);
                channel.SubmitEval(eval);
                channel.GetEvalsCompleted += channel_GetEvalsCompleted;
                channel.GetEvalsAsync();

                Console.Write("Waiting.....");
              
                channel.Close();
            }
            catch (FaultException fe)
            {
                Console.WriteLine("FaultException Handler: {0}", fe.GetType());
                channel.Abort();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("CommunicationException Handler: {0}", ce.GetType());
                channel.Abort();
            }
            catch (TimeoutException te)
            {
                Console.WriteLine("TimeoutException Handler: {0}", te.GetType());
                channel.Abort();
            }  
        }

        static void channel_GetEvalsCompleted(object sender, GetEvalsCompletedEventArgs e)
        {
            Console.WriteLine("Number of evals:{0}", e.Result.Count);
        }

        private static void CallEvalServiceClient()
        {
            EvalServiceClient channel = new EvalServiceClient("WSHttpBinding_IEvalService");
            Eval eval = new Eval
            {
                Submitter = "Throw ",
                Comments = "This is test from eval service client",
                TimeSent = DateTime.Now
            };

            try
            {
                channel.SubmitEval(eval);
                channel.SubmitEval(eval);
                List<Eval> evals = channel.GetEvals();
                Console.WriteLine("Number of evals:{0}", evals.Count);
                channel.Close();
            }
            catch (FaultException fe)
            {
                Console.WriteLine("FaultException Handler: {0}", fe.GetType());
                channel.Abort();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("CommunicationException Handler: {0}", ce.GetType());
                channel.Abort();
            }
            catch (TimeoutException te)
            {
                Console.WriteLine("TimeoutException Handler: {0}", te.GetType());
                channel.Abort();
            }  
        }

        private static void CallEvalServiceChannel()
        {
            ChannelFactory<IEvalServiceChannel> cf = new ChannelFactory<IEvalServiceChannel>("NetTcpBinding_IEvalService");

            IEvalServiceChannel channel = cf.CreateChannel();
            Eval eval = new Eval
            {
                Submitter = "Vamsi",
                Comments = "This is test from eval service client",
                TimeSent = DateTime.Now
            };

            channel.SubmitEval(eval);
            channel.SubmitEval(eval);

            List<Eval> evals = channel.GetEvals();
            Console.WriteLine("Number of evals:{0}", evals.Count);
            channel.Close();
        }

        private static void CallEvalService()
        {
            ChannelFactory<IEvalService> cf = new ChannelFactory<IEvalService>("NetTcpBinding_IEvalService");

            IEvalService channel = cf.CreateChannel();
            Eval eval = new Eval
            {
                Submitter = "Vamsi",
                Comments = "This is test",
                TimeSent = DateTime.Now
            };

            channel.SubmitEval(eval);
            channel.SubmitEval(eval);

            List<Eval> evals = channel.GetEvals();
            Console.WriteLine("Number of evals:{0}", evals.Count);

            ((IClientChannel)channel).Close();
        }
    }
}
