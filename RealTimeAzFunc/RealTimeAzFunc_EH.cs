// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.EventHubs;
using System.Text;
using System.IO;
using System;
using Newtonsoft.Json;
using System.Web;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;

namespace RealTimeAzFunc
{
    public static class RealTimeAzFunc_EH
    {
        [FunctionName("RealTimeAzFunc_EH")]
        [return: EventHub("%EventHubName%", Connection = "EventHubConnectionAppSetting")]
        public static async Task<string> RunAsync([EventGridTrigger]EventGridEvent eventGridEvent,
            ILogger log)
        {
            string CONTAINER_NAME = Environment.GetEnvironmentVariable("CONTAINER_NAME");
            //This is an error
            if (eventGridEvent.Data is null || "".Equals(eventGridEvent.Data))
            {
                throw new InvalidDataException("Message was empty");
            }

            log.LogInformation(eventGridEvent.Data.ToString());

            EventGridData eventGridData = JsonConvert.DeserializeObject<EventGridData>(eventGridEvent.Data.ToString());
            string fileUrl = eventGridData.url;
            log.LogInformation("fileUrl = "+fileUrl);

            //URL Decode it
            fileUrl = HttpUtility.UrlDecode(fileUrl);
            string fileLocation = fileUrl.Substring(fileUrl.LastIndexOf(CONTAINER_NAME) + CONTAINER_NAME.Length + 1);
            log.LogInformation("fileLocation = " + fileLocation);

            //Get storage account
            CloudStorageAccount storageAccount = GetAccount();

            //Get a client and connection to the container
            CloudBlobClient client = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = client.GetContainerReference(CONTAINER_NAME);

            //Get blob file reference
            CloudBlob blob = container.GetBlobReference(fileLocation);

            //Bring it down to a stream (async but wait for it)
            Byte[] output = new byte[10000];

            //Download blob
            await blob.DownloadToByteArrayAsync(output, 0);

            log.LogInformation("Message sent to Event Hub - \n"+ Encoding.UTF8.GetString(output));

            //Return blob as string to Event Hub
            return Encoding.UTF8.GetString(output);
        }

        private static CloudStorageAccount GetAccount()
        {
            string connectionString = Environment.GetEnvironmentVariable(
                "AzureBlobStorage"
            );

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            return storageAccount;
        }
    }
}
