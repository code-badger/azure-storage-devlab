using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;

namespace blob_connection_manager
{
    class Program
    {
        static void Main(string[] args)
        {
            var storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnection"));

            var blogClient = storageAccount.CreateCloudBlobClient();

            var container = blogClient.GetContainerReference("images");

            //UploadFile(container);
            //DownloadFile(container);
            //SetMetaData(container);
            GetMetaData(container);

            Console.ReadKey();
        }

        static void UploadFile(CloudBlobContainer container)
        {
            var blockBlob = container.GetBlockBlobReference("testUpload.txt");

            using (var fileStream = System.IO.File.OpenRead(@"C:\testUpload.txt"))
            {
                blockBlob.UploadFromStream(fileStream);
            }
        }

        static void DownloadFile(CloudBlobContainer container)
        {
            var blockBlob = container.GetBlockBlobReference("testUpload.txt");

            using (var fileStream = System.IO.File.OpenWrite(@"C:\testUpload.txt"))
            {
                blockBlob.DownloadToStream(fileStream);
            }
        }

        static void SetMetaData(CloudBlobContainer container)
        {
            container.Metadata.Clear();
            container.Metadata.Add("Owner", "Admin");
            container.Metadata["Updated"] = DateTime.Now.ToString();
            container.SetMetadata();
        }

        static void GetMetaData(CloudBlobContainer container)
        {
            container.FetchAttributes();
            Console.WriteLine("Container MetaData: \n");
            foreach (var item in container.Metadata)
            {
                Console.WriteLine(string.Format("{0}: {1}", item.Key, item.Value));
            }
        }
    }
}
