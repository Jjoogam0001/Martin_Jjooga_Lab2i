
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Queue;
using System.Device.Location;

namespace WorkerRole2
{
    public class WorkerRole : RoleEntryPoint
    {
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent runCompleteEvent = new ManualResetEvent(false);
        private string accountName = "martinstorage2019";
        private string accountKey = "arqVSiIar1LFWYdToGh47gQ9+LkqnwUBf0T31y5oFQ1FjEv+oW5uqLKh9i9ia1f4kyWZtJO4zO4Aqgjr6/agrw==";
        private StorageCredentials creds;
        private CloudStorageAccount storageAccount;
        private CloudQueueClient client;
        private CloudQueue inqueue, outqueue;
        private CloudQueueMessage inMessage, outMessage;

        private string[] airportCodes = { "STO", "CPH", "CDG", "LHR", "FRA" };
        private string[] airportNames = { "Stockholm", "Copenhagen", "Paris", "London", "Frankfurt" };
        private double[] latitudes = { 59.6519, 55.6181, 49.0097, 51.4707, 50.1167 };
        private double[] longitudes = { 17.9186, 12.6561, 2.5478, -0.4543, 8.6833 };


        private void InitQueue()
        {
            creds = new StorageCredentials(accountName, accountKey);
            storageAccount = new CloudStorageAccount(creds, useHttps: true);

            client = storageAccount.CreateCloudQueueClient();
            inqueue = client.GetQueueReference("workerqueue");
            inqueue.CreateIfNotExists();
            inqueue.Clear();
            outqueue = client.GetQueueReference("webqueue");
            outqueue.CreateIfNotExists();
            outqueue.Clear();
        }

        public override void Run()
        {
            Trace.TraceInformation("WorkerRole1 is running");

            try
            {
                this.RunAsync(this.cancellationTokenSource.Token).Wait();
            }
            finally
            {
                this.runCompleteEvent.Set();
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at https://go.microsoft.com/fwlink/?LinkId=166357.

            bool result = base.OnStart();

            Trace.TraceInformation("WorkerRole1 has been started");

            return result;
        }

        public override void OnStop()
        {
            Trace.TraceInformation("WorkerRole1 is stopping");

            this.cancellationTokenSource.Cancel();
            this.runCompleteEvent.WaitOne();

            base.OnStop();

            Trace.TraceInformation("WorkerRole1 has stopped");
        }

        private async Task RunAsync(CancellationToken cancellationToken)
        {
            InitQueue();
            while (!cancellationToken.IsCancellationRequested)
            {
                inMessage = await inqueue.GetMessageAsync();
                if (inMessage != null)
                {
                    string s = inMessage.AsString;

                    //Create array from the received String
                    string[] info = s.Split(',');
                    string from = info[0];
                    string to = info[1];
                    int nbrAdult = int.Parse(info[2]);
                    int nbrChild = int.Parse(info[3]);
                    int nbrInfant = int.Parse(info[4]);
                    int nbrSenior = int.Parse(info[5]);

                    string f = from.ToString().Trim();
                    string t = to.ToString().Trim();

                    var departureCityCoord = new GeoCoordinate(GetLat(f), GetLong(f));
                    var destinationCityCoord = new GeoCoordinate(GetLat(t), GetLong(t));

                    double distance = departureCityCoord.GetDistanceTo(destinationCityCoord) / 1000;

                    double baserate = Base_rate(f, t);

                    double offer = Offer(nbrAdult, nbrChild, nbrInfant, nbrSenior, distance, baserate);

                    string amount = string.Format("{0:N2}", offer);
                    Trace.TraceInformation("*** Worker Received " + s);
                    Trace.TraceInformation("*** Worker Processed " + from + "" + to + "  " + GetLong(to) + "  " + GetLat(to));
                    Trace.TraceInformation("*** Worker get distance in KG:  " + distance);
                    Trace.TraceInformation("*** Worker get offer:  " + offer);
                    Trace.TraceInformation("*** Worker get amount:  " + amount);


                    // Async delete the message
                    await inqueue.DeleteMessageAsync(inMessage);

                    // Create a message and add it to the queue.
                    outMessage = new CloudQueueMessage(amount);
                    outqueue.AddMessage(outMessage);


                    Trace.TraceInformation("Working");
                    await Task.Delay(1000);
                }
            }
        }

        private double Offer(int numberAdult, int numberOfChildren, int numberOfInfant, int numberOfSenior, double distance, double baserate)
        {
            double adultCost = distance * baserate * numberAdult;
            double childrenCost = distance * (baserate * 0.33) * numberOfChildren;
            double infantCost = distance * (baserate * 0.90) * numberOfInfant;
            double seniorCost = distance * (baserate * 0.25) * numberOfSenior;

            return adultCost + childrenCost + infantCost + seniorCost;
        }

        double LatValue;
        private double GetLat(string airportCode)
        {

            if (airportCode.Equals("STO"))
            {
                LatValue = 59.6519;
            }
            else if (airportCode.Equals("CPH"))
            {
                LatValue = 55.6181;
            }
            else if (airportCode.Equals("CDG"))
            {
                LatValue = 49.0097;
            }
            else if (airportCode.Equals("LHR"))
            {
                LatValue = 51.4707;
            }
            else if (airportCode.Equals("FRA"))
            {
                LatValue = 50.1167;
            }
            return LatValue;
        }
        double longValue;
        private double GetLong(string airportCode)
        {

            if (airportCode.Equals("STO"))
            {
                longValue = 17.9186;
            }
            else if (airportCode.Equals("CPH"))
            {
                longValue = 12.6561;
            }
            else if (airportCode.Equals("CDG"))
            {
                longValue = 2.5478;
            }
            else if (airportCode.Equals("LHR"))
            {
                longValue = -0.4543;
            }
            else if (airportCode.Equals("FRA"))
            {
                longValue = 8.6833;
            }
            return longValue;
        }




        double rate;


        private double Base_rate(string from, string to)
        {
            if (from.Equals("STO"))
            {
                if (to.Equals("CPH"))
                {
                    rate = 2;
                }
                else if (to.Equals("CDG"))
                {
                    rate = 1.2;
                }
                else if (to.Equals("LHK"))
                {
                    rate = 2;
                }
                else if (to.Equals("FRA"))
                {
                    rate = 2;
                }
            }
            if (from.Equals("CPH"))
            {
                if (to.Equals("STO"))
                {
                    rate = 2;
                }
                else if (to.Equals("CDG"))
                {
                    rate = 2;
                }
                else if (to.Equals("LHK"))
                {
                    rate = 3;
                }
                else if (to.Equals("FRA"))
                {
                    rate = 1.2;
                }
            }
            if (from.Equals("CDG"))
            {
                if (to.Equals("CPH"))
                {
                    rate = 2;
                }
                else if (to.Equals("STO"))
                {
                    rate = 1.7;
                }
                else if (to.Equals("LHK"))
                {
                    rate = 2;
                }
                else if (to.Equals("FRA"))
                {
                    rate = 1.3;
                }
            }
            if (from.Equals("LHK"))
            {
                if (to.Equals("CPH"))
                {
                    rate = 1.4;
                }
                else if (to.Equals("CDG"))
                {
                    rate = 1.9;
                }
                else if (to.Equals("LHK"))
                {
                    rate = 2.3;
                }
                else if (to.Equals("FRA"))
                {
                    rate = 1.5;
                }
            }
            if (from.Equals("FRA"))
            {
                if (to.Equals("CPH"))
                {
                    rate = 2;
                }
                else if (to.Equals("CDG"))
                {
                    rate = 2.1;
                }
                else if (to.Equals("LHK"))
                {
                    rate = 2.3;
                }
                else if (to.Equals("FRA"))
                {
                    rate = 1.3;
                }
            }
            return rate;
        }
    }




}
