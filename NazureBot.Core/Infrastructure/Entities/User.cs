// --------------------------------------------------------------------------------------------------------------------
// <copyright file="User.cs" company="Patrick Magee">
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
//   The user.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Core.Infrastructure.Entities
{
    #region Using directives

    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    using NazureBot.Modules.Irc;
    using NazureBot.Modules.Security;

    #endregion

    /// <summary>
    /// The user.
    /// </summary>
    public class User : IUser
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
        {
            this.Id = Guid.NewGuid();
            this.AccessLevel = AccessLevel.Guest;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="hostMask">
        /// The host mask.
        /// </param>
        public User(string hostMask) : this()
        {
            Contract.Requires<ArgumentNullException>(hostMask != null, "hostMask");

            this.Host = new KnownHost(hostMask, this);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the access level.
        /// </summary>
        /// <value>
        /// The access level.
        /// </value>
        public AccessLevel AccessLevel { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the host.
        /// </summary>
        /// <value>
        /// The host.
        /// </value>
        public KnownHost Host { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the known hosts.
        /// </summary>
        /// <value>
        /// The known hosts.
        /// </value>
        public virtual ICollection<KnownHost> KnownHosts { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }

        #endregion

        #region Explicit Interface Properties

        /// <summary>
        /// Gets the access level.
        /// </summary>
        /// <value>
        /// The access level.
        /// </value>
        AccessLevel IUser.AccessLevel
        {
            get
            {
                return this.AccessLevel;
            }
        }

        /// <summary>
        /// Gets the host.
        /// </summary>
        /// <value>
        /// The host.
        /// </value>
        IKnownHost IUser.Host
        {
            get
            {
                return this.Host;
            }
        }

        #endregion
    }
}
