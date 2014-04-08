// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Identity.cs" company="Patrick Magee">
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
//   The identity.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Core.Infrastructure.Entities
{
    #region Using directives

    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NazureBot.Modules.Irc;

    #endregion

    /// <summary>
    /// The identity.
    /// </summary>
    public class Identity : IIdentity
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the networks.
        /// </summary>
        public virtual ICollection<Network> Networks { get; set; }

        /// <summary>
        /// Gets or sets the nick name.
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the real name.
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; }

        #endregion

        #region Explicit Interface Properties

        /// <summary>
        /// Gets the networks.
        /// </summary>
        IEnumerable<INetwork> IIdentity.Networks
        {
            get
            {
                return this.Networks.ToList();
            }
        }

        #endregion
    }

    
}