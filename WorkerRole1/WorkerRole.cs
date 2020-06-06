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

namespace WorkerRole1
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
            inqueue = client.GetQueueReference("hotelworkerqueue");
            inqueue.CreateIfNotExists();
            inqueue.Clear();
            outqueue = client.GetQueueReference("hotelwebqueue");
            outqueue.CreateIfNotExists();
            outqueue.Clear();
        }

        public override void Run()
        {
            Trace.TraceInformation("WorkerRole2 is running");

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

            Trace.TraceInformation("WorkerRole2 has been started");

            return result;
        }

        public override void OnStop()
        {
            Trace.TraceInformation("WorkerRole2 is stopping");

            this.cancellationTokenSource.Cancel();
            this.runCompleteEvent.WaitOne();

            base.OnStop();

            Trace.TraceInformation("WorkerRole2 has stopped");
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
                    int numberOfCustomer = int.Parse(info[0]);
                    int numberOfNights = int.Parse(info[1]);
                    int numberOfSeniors = int.Parse(info[2]);
                    string mainGuessName = info[3];
                    string roomType = info[4];



                    double hotelCost = Cost(numberOfCustomer, numberOfSeniors, numberOfNights, roomType);
                    string amount = string.Format("{0:N2}", hotelCost);
                    Trace.TraceInformation("*** Worker Received " + s);
                    Trace.TraceInformation("*** Worker hotel cost " + amount);



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

        double cost;
        private double Cost(int numOfCustomer, int numOfSenior, int numOfNight, string roomType)
        {
            int fullPriceCustomer = numOfCustomer - numOfSenior;
            if (roomType.Equals("1"))
            {
                double fullPriceCustomerCost = fullPriceCustomer * 600 * numOfNight;
                double seniorCustomerCost = numOfSenior * 300 * numOfNight;
                cost = fullPriceCustomerCost + seniorCustomerCost;
            }
            else
            {
                double fullPriceCustomerCost = fullPriceCustomer * 900 * numOfNight;
                double seniorCustomerCost = numOfSenior * 450 * numOfNight;
                cost = fullPriceCustomerCost + seniorCustomerCost;
            }
            return cost;
        }
    }
}
