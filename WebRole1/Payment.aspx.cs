using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebRole1
{
    public partial class Payment : System.Web.UI.Page
    {
        private string accountName = "martinstorage2019";
        private string accountKey = "arqVSiIar1LFWYdToGh47gQ9+LkqnwUBf0T31y5oFQ1FjEv+oW5uqLKh9i9ia1f4kyWZtJO4zO4Aqgjr6/agrw==";

        private string flightCost, hotelCost, carRentCost;

        protected void Unnamed3_Click(object sender, EventArgs e)
        {

          



        }

        protected void Page_Load(object sender, EventArgs e)
        {
            StorageCredentials creds = new StorageCredentials(accountName, accountKey);
            CloudStorageAccount storageAccount = new CloudStorageAccount(creds, useHttps: true);
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a queue
            CloudQueue queue1 = queueClient.GetQueueReference("webqueue");
            CloudQueue queue2 = queueClient.GetQueueReference("hotelwebqueue");
            CloudQueue queue3 = queueClient.GetQueueReference("carwebqueue");



            try
            {



                // Create the queue if it doesn't already exist
                queue1.CreateIfNotExists();
                queue2.CreateIfNotExists();
                queue3.CreateIfNotExists();

                // retrieve the next message
                CloudQueueMessage readMessage1 = queue1.GetMessage();
                CloudQueueMessage readMessage2 = queue2.GetMessage();
                CloudQueueMessage readMessage3 = queue3.GetMessage();

                if (readMessage1 != null)
                { // Display message (populate the textbox with the message you just retrieved.

                    flightCost = readMessage1.AsString;
                    

                    //Delete the message just read to avoid reading it over and over again
                    queue1.DeleteMessage(queue1.GetMessage());
                }
                if (readMessage2 != null)
                { // Display message (populate the textbox with the message you just retrieved.

                    hotelCost = readMessage2.AsString;
                  

                    //Delete the message just read to avoid reading it over and over again
                    queue2.DeleteMessage(queue2.GetMessage());
                }
                if (readMessage3 != null)
                { // Display message (populate the textbox with the message you just retrieved.

                    carRentCost = readMessage3.AsString;
                  




                    //Delete the message just read to avoid reading it over and over again
                    queue3.DeleteMessage(queue3.GetMessage());
                }

                double totalCost = double.Parse(flightCost) + double.Parse(hotelCost) + double.Parse(carRentCost);
                Sumtopay.Text = totalCost.ToString();



            }
            catch (Exception ee)
            {
                Debug.WriteLine("Problem reading from queue");
            }


        }
    }
}