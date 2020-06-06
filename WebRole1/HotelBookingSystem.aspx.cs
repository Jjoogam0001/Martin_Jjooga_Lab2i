using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebRole1
{
    public partial class HotelBookingSystem : System.Web.UI.Page
    {
        private string accountName = "martinstorage2019";
        private string accountKey = "arqVSiIar1LFWYdToGh47gQ9+LkqnwUBf0T31y5oFQ1FjEv+oW5uqLKh9i9ia1f4kyWZtJO4zO4Aqgjr6/agrw==";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GoToPayment(object sender, EventArgs e)
        {
            string NbrCustomers = NumberOfCustomer.SelectedItem.Value;
            string MainGuess = Main_Guest_Name.Text;
            string NbrOfNights = NumberOfNight.Text;
            string RoomTypes = RoomType.SelectedItem.Value;
            string NumberOfSenior = Number_Of_Senior.Text;

            string result = NbrCustomers + "," + NbrOfNights + "," + NumberOfSenior + "," + MainGuess + "," + RoomTypes;

            StorageCredentials creds = new StorageCredentials(accountName, accountKey);
            CloudStorageAccount storageAccount = new CloudStorageAccount(creds, true);

            CloudQueueClient client = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a specific queue
            CloudQueue queue = client.GetQueueReference("hotelworkerqueue");

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