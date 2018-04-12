using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace queue_communication_manager
{
    class Program
    {
        static void Main(string[] args)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnection"));

            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            CloudQueue queue = queueClient.GetQueueReference("tasks");

            queue.CreateIfNotExists();

            CloudQueueMessage message = new CloudQueueMessage("Hi Universe");
            var time = new TimeSpan(24, 0, 0);
            queue.AddMessage(message, time, null, null);

            CloudQueueMessage message1 = queue.GetMessage();
            Console.WriteLine(message1.AsString);
            queue.DeleteMessage(message1);

            CloudQueueMessage message2 = queue.PeekMessage();
            Console.WriteLine(message2.AsString);

            Console.ReadKey();
        }

    }
}
