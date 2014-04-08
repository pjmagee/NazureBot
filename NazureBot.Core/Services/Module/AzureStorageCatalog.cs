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
    #region Using directives

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;
    using System.IO;
    using System.Linq;

    using Microsoft.WindowsAzure.ServiceRuntime;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;

    #endregion

    /// <summary>
    /// The azure storage catalog.
    /// </summary>
    public class AzureStorageCatalog : ComposablePartCatalog
    {
        #region Fields

        /// <summary>
        ///     The directory catalog.
        /// </summary>
        private readonly DirectoryCatalog directoryCatalog;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureStorageCatalog"/> class.
        /// </summary>
        /// <param name="storageSetting">
        /// The storage setting.
        /// </param>
        /// <param name="containerName">
        /// The container name.
        /// </param>
        public AzureStorageCatalog(string storageSetting, string containerName)
        {
            string localCatalogDirectory = this.GetStorageCatalog(storageSetting, containerName);
            this.directoryCatalog = new DirectoryCatalog(localCatalogDirectory);

            this.directoryCatalog.Changed += this.OnChanged;
            this.directoryCatalog.Changing += this.OnChanging;
        }

        #endregion

        #region Public Events

        /// <summary>
        ///     Occurs when [changed].
        /// </summary>
        public event EventHandler<ComposablePartCatalogChangeEventArgs> Changed;

        /// <summary>
        ///     Occurs when [changing].
        /// </summary>
        public event EventHandler<ComposablePartCatalogChangeEventArgs> Changing;

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the part definitions that are contained in the catalog.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition" /> contained in the
        ///     <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartCatalog" />.
        /// </returns>
        public override IQueryable<ComposablePartDefinition> Parts
        {
            get
            {
                return this.directoryCatalog.Parts;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Releases the unmanaged resources used by the
        ///     <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartCatalog"/> and optionally releases the
        ///     managed resources.
        /// </summary>
        /// <param name="disposing">
        /// true to release both managed and unmanaged resources; false to release only unmanaged
        ///     resources.
        /// </param>
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

        /// <summary>
        /// The on changed.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected virtual void OnChanged(ComposablePartCatalogChangeEventArgs e)
        {
            EventHandler<ComposablePartCatalogChangeEventArgs> handler = this.Changed;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// The on changing.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected virtual void OnChanging(ComposablePartCatalogChangeEventArgs e)
        {
            EventHandler<ComposablePartCatalogChangeEventArgs> handler = this.Changing;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Gets the storage catalog.
        /// </summary>
        /// <param name="storageSetting">
        /// The storage setting.
        /// </param>
        /// <param name="containerName">
        /// Name of the container.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetStorageCatalog(string storageSetting, string containerName)
        {
            CloudStorageAccount storageAccount = RoleEnvironment.IsEmulated
                                                     ? CloudStorageAccount.DevelopmentStorageAccount
                                                     : CloudStorageAccount.Parse(storageSetting);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            var location = new Uri(storageAccount.BlobEndpoint + "/" + containerName);

            var blobContainer = new CloudBlobContainer(location, blobClient.Credentials);

            IEnumerable<IListBlobItem> blobs = blobContainer.ListBlobs(
                useFlatBlobListing: true, 
                blobListingDetails: BlobListingDetails.All);

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

        /// <summary>
        /// Called when [changed].
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="ComposablePartCatalogChangeEventArgs"/> instance containing the event data.
        /// </param>
        private void OnChanged(object sender, ComposablePartCatalogChangeEventArgs e)
        {
            this.OnChanged(e);
        }

        /// <summary>
        /// Called when [changing].
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="ComposablePartCatalogChangeEventArgs"/> instance containing the event data.
        /// </param>
        private void OnChanging(object sender, ComposablePartCatalogChangeEventArgs e)
        {
            this.OnChanging(e);
        }

        #endregion
    }
}