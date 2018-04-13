using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using sotrage_access_manager.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace sotrage_access_manager.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnection"));

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference("images");

            var blobs = new List<BlobImage>();

            foreach (var blob in container.ListBlobs())
            {
                if (blob.GetType() == typeof(CloudBlockBlob))
                {
                    blobs.Add(new BlobImage { BlobUri = blob.Uri.ToString() });
                }
            }

            return View(blobs);
        }
    }
}