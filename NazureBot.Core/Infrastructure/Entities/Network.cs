// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Network.cs" company="Patrick Magee">
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
//   The network.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Core.Infrastructure.Entities
{
    #region Using directives

    using System;
    using System.Collections.Generic;

    using NazureBot.Modules.Messaging;

    #endregion

    /// <summary>
    /// The network.
    /// </summary>
    public class Network : INetwork
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the channels.
        /// </summary>
        public virtual ICollection<Channel> Channels { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the identity.
        /// </summary>
        public virtual Identity Identity { get; set; }

        /// <summary>
        /// Gets or sets the identity id.
        /// </summary>
        public Guid? IdentityId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the servers.
        /// </summary>
        public virtual ICollection<Server> Servers { get; set; }

        #endregion

        #region Explicit Interface Properties

        /// <summary>
        /// Gets the channels.
        /// </summary>
        /// <value>
        /// The channels.
        /// </value>
        IEnumerable<IChannel> INetwork.Channels
        {
            get
            {
                return this.Channels;
            }
        }

        /// <summary>
        /// Gets the identity.
        /// </summary>
        /// <value>
        /// The identity.
        /// </value>
        IIdentity INetwork.Identity
        {
            get
            {
                return this.Identity;
            }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string INetwork.Name
        {
            get
            {
                return this.Name;
            }
        }

        /// <summary>
        /// Gets the servers.
        /// </summary>
        /// <value>
        /// The servers.
        /// </value>
        IEnumerable<IServer> INetwork.Servers
        {
            get
            {
                return this.Servers;
            }
        }

        #endregion
    }
}