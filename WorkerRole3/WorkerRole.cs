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

namespace WorkerRole3
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

        private void InitQueue()
        {
            creds = new StorageCredentials(accountName, accountKey);
            storageAccount = new CloudStorageAccount(creds, useHttps: true);

            client = storageAccount.CreateCloudQueueClient();
            inqueue = client.GetQueueReference("carworkerqueue");
            inqueue.CreateIfNotExists();
            inqueue.Clear();
            outqueue = client.GetQueueReference("carwebqueue");
            outqueue.CreateIfNotExists();
            outqueue.Clear();
        }

        public override void Run()
        {
            Trace.TraceInformation("WorkerRole3 is running");

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

            Trace.TraceInformation("WorkerRole3 has been started");

            return result;
        }

        public override void OnStop()
        {
            Trace.TraceInformation("WorkerRole3 is stopping");

            this.cancellationTokenSource.Cancel();
            this.runCompleteEvent.WaitOne();

            base.OnStop();

            Trace.TraceInformation("WorkerRole3 has stopped");
        }

        private async Task RunAsync(CancellationToken cancellationToken)
        {
            // TODO: Replace the following with your own logic.
            InitQueue();
            while (!cancellationToken.IsCancellationRequested)
            {
                inMessage = await inqueue.GetMessageAsync();
                if (inMessage != null)
                {
                    string s = inMessage.AsString;
                    //Create array from the received String
                    string[] info = s.Split(',');
                    int numberOfSeats = int.Parse(info[0]);
                    int YearModel = int.Parse(info[1]);
                    int DriveAge = int.Parse(info[2]);
                    string GazType = info[3]; //1or2

                    double cost = FinalCost(GazType, YearModel, numberOfSeats, DriveAge);

                    string amount = string.Format("{0:N2}", cost);
                    Trace.TraceInformation("*** Worker Received " + s);



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
        double finalCost;
        private double FinalCost(string gazType, int yearModel, int nbrOfSeats, int age)
        {
            if (gazType.Equals("2"))
            {
                finalCost = BasePrice(yearModel, nbrOfSeats) + ExtraCost(age);
            }
            else
            {
                finalCost = (BasePrice(yearModel, nbrOfSeats) + ExtraCost(age)) * 1.2;
            }
            return finalCost;
        }
        double price;
        private double BasePrice(int yearModel, int nbrOfSeats)
        {
            if (nbrOfSeats < 6)
            {
                price = (yearModel - 2010) * 600;
            }
            else
            {
                price = (yearModel - 2010) * 900;
            }
            return price;
        }

        double extra_cost;
        private double ExtraCost(int age)
        {
            if (age < 45)
            {
                extra_cost = (50 - age) * 200;
            }
            else
            {
                extra_cost = (age - 40) * 225;
            }


            return extra_cost;
        }
    }
}
