// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Channel.cs" company="Patrick Magee">
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
//   The channel.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Core.Infrastructure.Entities
{
    #region Using directives

    using System;

    using NazureBot.Modules.Irc;

    #endregion

    /// <summary>
    /// The channel.
    /// </summary>
    public class Channel : IChannel
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the network.
        /// </summary>
        public virtual Network Network { get; set; }

        /// <summary>
        /// Gets or sets the network id.
        /// </summary>
        public Guid? NetworkId { get; set; }

        #endregion

        #region Explicit Interface Properties

        /// <summary>
        /// Gets the network.
        /// </summary>
        INetwork IChannel.Network
        {
            get
            {
                return this.Network;
            }
        }

        #endregion
    }
}