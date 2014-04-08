// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModuleService.cs" company="Patrick Magee">
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
//   The module service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Core.Services.Module
{
    #region Using directives

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition.Hosting;
    using System.Diagnostics;

    using Microsoft.WindowsAzure.ServiceRuntime;

    using Ninject;

    #endregion

    /// <summary>
    /// The module service.
    /// </summary>
    public class ModuleService : IModuleService, IStartable
    {
        #region Static Fields

        /// <summary>
        /// The cache folder
        /// </summary>
        public static readonly string CacheFolder;

        #endregion

        #region Fields

        /// <summary>
        /// The aggregate catalog
        /// </summary>
        private readonly AggregateCatalog aggregateCatalog;

        /// <summary>
        /// The bot
        /// </summary>
        private readonly IBot bot;

        /// <summary>
        /// The module container
        /// </summary>
        private readonly CompositionContainer moduleContainer;

        /// <summary>
        /// The storage catalog
        /// </summary>
        private readonly AzureStorageCatalog storageCatalog;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes static members of the <see cref="ModuleService" /> class.
        /// </summary>
        static ModuleService()
        {
            CacheFolder = RoleEnvironment.GetLocalResource("ModulesCache").RootPath;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleService"/> class.
        /// </summary>
        /// <param name="bot">
        /// The bot.
        /// </param>
        [Inject]
        public ModuleService(IBot bot)
        {
            this.bot = bot;

            this.storageCatalog = new AzureStorageCatalog(string.Empty, string.Empty);
            this.aggregateCatalog = new AggregateCatalog(this.storageCatalog);
            this.moduleContainer = new CompositionContainer(this.aggregateCatalog);

            this.moduleContainer.ExportsChanged += this.ModuleContainerOnExportsChanged;
            this.storageCatalog.Changed += this.StorageCatalogOnChanged;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get module exports.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable" />.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<Lazy<NazureBot.Modules.Module, IDictionary<string, object>>> GetModuleExports()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The start.
        /// </summary>
        public void Start()
        {
        }

        /// <summary>
        /// The stop.
        /// </summary>
        public void Stop()
        {
            this.storageCatalog.Dispose();
            this.aggregateCatalog.Dispose();
            this.moduleContainer.Dispose();
        }

        #endregion

        #region Methods

        /// <summary>
        /// The module container on exports changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="exportsChangeEventArgs">The exports change event args.</param>
        private void ModuleContainerOnExportsChanged(object sender, ExportsChangeEventArgs exportsChangeEventArgs)
        {
            Trace.TraceInformation("Module container exports changed");
        }

        /// <summary>
        /// The storage catalog on changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="composablePartCatalogChangeEventArgs">The composable part catalog change event args.</param>
        private void StorageCatalogOnChanged(object sender, ComposablePartCatalogChangeEventArgs composablePartCatalogChangeEventArgs)
        {
            Trace.TraceInformation("Storage catalog changed");
        }

        #endregion
    }
}