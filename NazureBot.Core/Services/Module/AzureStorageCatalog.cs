// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AzureStorageCatalog.cs" company="Patrick Magee">
//   Copyright © 2013 Patrick Magee
//   
//   This program is free software: you can redistribute it and/or modify it
//   under the +terms of the GNU General Public License as published by 
//   the Free Software Foundation, either version 3 of the License, 
//   or (at your option) any later version.
//   
//   This program is distributed in the hope that it will be useful, 
//   but WITHOUT ANY WARRANTY; without even the implied warranty of 
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the 
//   GNU General Public License for more details.
//   
//   You should have received a copy of the GNU General Public License
//   along with this program. If not, see http://www.gnu.org/licenses/.
// </copyright>
// <summary>
//   The azure storage catalog.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Core.Services.Module
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;
    using System.IO;
    using System.Linq;

    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;

    public class AzureStorageCatalog : ComposablePartCatalog
    {
        private readonly DirectoryCatalog directoryCatalog;

        public AzureStorageCatalog(string storageSetting, string containerName)
        {
            string localCatalogDirectory = this.GetStorageCatalog(storageSetting, containerName);
            
            this.directoryCatalog = new DirectoryCatalog(localCatalogDirectory);

            this.directoryCatalog.Changed += this.OnChanged;
            this.directoryCatalog.Changing += this.OnChanging;
        }

        public event EventHandler<ComposablePartCatalogChangeEventArgs> Changed;
        public event EventHandler<ComposablePartCatalogChangeEventArgs> Changing;

        public override IQueryable<ComposablePartDefinition> Parts
        {
            get { return this.directoryCatalog.Parts; }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.directoryCatalog.Changed -= this.OnChanged;
                this.directoryCatalog.Changing -= this.OnChanging;
                this.directoryCatalog.Dispose();
            }

            base.Dispose(disposing);
        }

        protected virtual void OnChanged(ComposablePartCatalogChangeEventArgs e)
        {
            var handler = this.Changed;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnChanging(ComposablePartCatalogChangeEventArgs e)
        {
            var handler = this.Changing;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        private string GetStorageCatalog(string storageSetting, string containerName)
        {
            CloudStorageAccount storageAccount = RoleEnvironment.IsEmulated
                                                     ? CloudStorageAccount.DevelopmentStorageAccount
                                                     : CloudStorageAccount.Parse(storageSetting);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            var location = new Uri(storageAccount.BlobEndpoint + "/" + containerName);

            var blobContainer = new CloudBlobContainer(location, blobClient.Credentials);

            IEnumerable<IListBlobItem> blobs = blobContainer.ListBlobs(useFlatBlobListing: true, blobListingDetails: BlobListingDetails.All);

            foreach (IListBlobItem item in blobs)
            {
                string fileAbsPath = item.Uri.AbsolutePath.ToLower();
                fileAbsPath = fileAbsPath.Substring(fileAbsPath.LastIndexOf('/') + 1);

                try
                {
                    var blob = new CloudPageBlob(item.Uri);
                    blob.DownloadToFile(ModuleService.CacheFolder + fileAbsPath, FileMode.Create);
                }
                catch (Exception e)
                {
                    // Ignore exceptions, if we can't write it's because we've already got the file, move on
                }
            }

            return ModuleService.CacheFolder;
        }

        private void OnChanged(object sender, ComposablePartCatalogChangeEventArgs e)
        {
            this.OnChanged(e);
        }

        private void OnChanging(object sender, ComposablePartCatalogChangeEventArgs e)
        {
            this.OnChanging(e);
        }
    }
}