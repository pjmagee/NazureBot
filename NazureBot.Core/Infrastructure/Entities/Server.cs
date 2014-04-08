// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Server.cs" company="Patrick Magee">
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
//   The server.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Core.Infrastructure.Entities
{
    #region Using directives

    using System;

    using NazureBot.Modules.Irc;

    #endregion

    /// <summary>
    /// The server.
    /// </summary>
    public class Server : IServer
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is ssl.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is SSL]; otherwise, <c>false</c>.
        /// </value>
        public bool IsSsl { get; set; }

        /// <summary>
        /// Gets or sets the network.
        /// </summary>
        /// <value>
        /// The network.
        /// </value>
        public virtual Network Network { get; set; }

        /// <summary>
        /// Gets or sets the network id.
        /// </summary>
        /// <value>
        /// The network identifier.
        /// </value>
        public Guid? NetworkId { get; set; }

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>
        /// The port.
        /// </value>
        public int? Port { get; set; }

        #endregion

        #region Explicit Interface Properties

        /// <summary>
        /// Gets the port.
        /// </summary>
        /// <value>
        /// The port.
        /// </value>
        int IServer.Port
        {
            get
            {
                return this.Port.GetValueOrDefault(6667);
            }
        }

        /// <summary>
        /// Gets a value indicating whether ssl.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [SSL]; otherwise, <c>false</c>.
        /// </value>
        bool IServer.Ssl
        {
            get
            {
                return this.IsSsl;
            }
        }

        #endregion
    }
}