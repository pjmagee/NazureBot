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
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition.Hosting;
    using System.Diagnostics;

    using Ninject;

    /// <summary>
    /// The module service.
    /// </summary>
    public class ModuleService : IModuleService, IStartable
    {
        public static readonly string CacheFolder;

        private readonly AggregateCatalog aggregateCatalog;
        private readonly IBot bot;
        private readonly CompositionContainer moduleContainer;
        private readonly AzureStorageCatalog storageCatalog;

        static ModuleService()
        {
            CacheFolder = RoleEnvironment.GetLocalResource("ModulesCache").RootPath;
        }

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

        public IEnumerable<Lazy<NazureBot.Modules.Module, IDictionary<string, object>>> GetModuleExports()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
        
        }

        public void Stop()
        {
            this.storageCatalog.Dispose();
            this.aggregateCatalog.Dispose();
            this.moduleContainer.Dispose();
        }

        private void ModuleContainerOnExportsChanged(object sender, ExportsChangeEventArgs exportsChangeEventArgs)
        {
            Trace.TraceInformation("Module container exports changed");
        }

        private void StorageCatalogOnChanged(object sender, ComposablePartCatalogChangeEventArgs composablePartCatalogChangeEventArgs)
        {
            Trace.TraceInformation("Storage catalog changed");
        }
    }
}