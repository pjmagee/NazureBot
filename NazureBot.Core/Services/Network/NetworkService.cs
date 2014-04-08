// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NetworkService.cs" company="Patrick Magee">
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
//   The network service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Core.Services.Network
{
    #region Using directives

    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Diagnostics.Contracts;
    using System.Threading.Tasks;

    using NazureBot.Core.Infrastructure.EF;
    using NazureBot.Modules.Irc;

    using Ninject;

    #endregion

    /// <summary>
    /// The network service.
    /// </summary>
    public class NetworkService : INetworkService
    {
        #region Fields

        /// <summary>
        /// The database context
        /// </summary>
        private readonly DatabaseContext databaseContext;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkService"/> class.
        /// </summary>
        /// <param name="databaseContext">
        /// The database context.
        /// </param>
        [Inject]
        public NetworkService(DatabaseContext databaseContext)
        {
            Contract.Requires<ArgumentNullException>(databaseContext != null, "databaseContext");

            this.databaseContext = databaseContext;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Gets the networks asynchronous.
        /// </summary>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        public async Task<IEnumerable<INetwork>> GetNetworksAsync()
        {
            return await this.databaseContext.Networks.ToListAsync();
        }

        #endregion
    }
}