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
    public partial class _Default : Page
    {
        private string accountName = "martinstorage2019";
        private string accountKey = "arqVSiIar1LFWYdToGh47gQ9+LkqnwUBf0T31y5oFQ1FjEv+oW5uqLKh9i9ia1f4kyWZtJO4zO4Aqgjr6/agrw==";

        protected void Submit(object sender, EventArgs e)
        {
            string from = From_city.SelectedItem.Value;
            string to = To_city.SelectedItem.Value;
            string nbrOfInfant = Infant.SelectedItem.Value;
            string nbrOfChildren = Child.SelectedItem.Value;
            string nbrOfAdult = Adult.SelectedItem.Value;
            string nbrOfSenior = Senior.SelectedItem.Value;

            string result = from + "," + to + "," + nbrOfAdult + "," + nbrOfChildren + "," + nbrOfInfant + "," + nbrOfSenior;


            StorageCredentials creds = new StorageCredentials(accountName, accountKey);
            CloudStorageAccount storageAccount = new CloudStorageAccount(creds, true);

            CloudQueueClient client = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a specific queue
            CloudQueue queue = client.GetQueueReference("workerqueue");

            // Create the queue if it doesn't already exist
            queue.CreateIfNotExists();

            //remove any existing messages (just in case)
            queue.Clear();

            // Create a message and add it to the queue.
            queue.AddMessage(new CloudQueueMessage(result));
        }

        protected void GotoHotelResPortal(object sender, EventArgs e)
        {
            Response.Redirect("~/HotelBookingSystem.aspx");
        }

        protected void GotoCarRentPortal(object sender, EventArgs e)
        {
            Response.Redirect("~/CarRentalSystem.aspx");
        }



        protected void Get_Sum(object sender, EventArgs e)
        {
            StorageCredentials creds = new StorageCredentials(accountName, accountKey);
            CloudStorageAccount storageAccount = new CloudStorageAccount(creds, useHttps: true);
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a queue
            CloudQueue queue = queueClient.GetQueueReference("webqueue");
            try
            {
                // Create the queue if it doesn't already exist
                queue.CreateIfNotExists();

                // retrieve the next message
                CloudQueueMessage readMessage = queue.GetMessage();

                if (readMessage != null)
                { // Display message (populate the textbox with the message you just retrieved.
                    Sum.Text = readMessage.AsString;

                    //Delete the message just read to avoid reading it over and over again
                    queue.DeleteMessage(queue.GetMessage());
                }

            }
            catch (Exception ee)
            {
                Debug.WriteLine("Problem reading from queue");
            }
        }

        protected void GotoPayment1(object sender, EventArgs e)
        {
            string from = From_city.SelectedItem.Value;
            string to = To_city.SelectedItem.Value;
            string nbrOfInfant = Infant.SelectedItem.Value;
            string nbrOfChildren = Child.SelectedItem.Value;
            string nbrOfAdult = Adult.SelectedItem.Value;
            string nbrOfSenior = Senior.SelectedItem.Value;

            string result = from + "," + to + "," + nbrOfAdult + "," + nbrOfChildren + "," + nbrOfInfant + "," + nbrOfSenior;


            StorageCredentials creds = new StorageCredentials(accountName, accountKey);
            CloudStorageAccount storageAccount = new CloudStorageAccount(creds, true);

            CloudQueueClient client = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a specific queue
            CloudQueue queue = client.GetQueueReference("workerqueue");

            // Create the queue if it doesn't already exist
            queue.CreateIfNotExists();

            //remove any existing messages (just in case)
            queue.Clear();

            // Create a message and add it to the queue.
            queue.AddMessage(new CloudQueueMessage(result));
            Response.Redirect("~/Payment.aspx");
        }
    }
}