using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NazureBot.UI.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.ServiceBus.Messaging;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult UploadModule(HttpPostedFileBase file)
        {
            var storage = CloudStorageAccount.DevelopmentStorageAccount;

            var client = storage.CreateCloudBlobClient();

            CloudBlobContainer container = client.GetContainerReference("modules");

            container.CreateIfNotExists();

            container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(file.FileName);
            
            using (var stream = file.InputStream)
            {
                blockBlob.UploadFromStream(stream);
            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Start()
        {
            var brokeredMessage = new BrokeredMessage();

            brokeredMessage.Properties.Add("start", true);

            await QueueConnector.Client.SendAsync(brokeredMessage);

            return RedirectToAction("Index", "Home");
        }

    }
}