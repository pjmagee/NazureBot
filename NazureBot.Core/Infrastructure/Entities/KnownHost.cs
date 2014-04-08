// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KnownHost.cs" company="Patrick Magee">
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
//   The known host.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Core.Infrastructure.Entities
{
    #region Using directives

    using System;
    using System.Diagnostics.Contracts;

    using NazureBot.Modules.Irc;

    #endregion

    /// <summary>
    /// The known host.
    /// </summary>
    public class KnownHost : IKnownHost, IEquatable<string>, IComparable<string>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="KnownHost" /> class.
        /// </summary>
        public KnownHost()
        {
            this.Id = Guid.NewGuid();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KnownHost"/> class.
        /// </summary>
        /// <param name="hostmask">
        /// The hostmask.
        /// </param>
        public KnownHost(string hostmask) : this()
        {
            Contract.Requires<ArgumentNullException>(hostmask != null, "hostmask");

            this.HostMask = hostmask;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KnownHost"/> class.
        /// </summary>
        /// <param name="hostmask">
        /// The hostmask.
        /// </param>
        /// <param name="user">
        /// The user.
        /// </param>
        public KnownHost(string hostmask, User user) : this(hostmask)
        {
            Contract.Requires<ArgumentNullException>(user != null, "user");

            this.User = user;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the host.
        /// </summary>
        /// <value>
        /// The host.
        /// </value>
        public string Host
        {
            get
            {
                return this.HostMask.Split('!', '@')[2];
            }
        }

        /// <summary>
        /// Gets or sets the host mask.
        /// </summary>
        /// <value>
        /// The host mask.
        /// </value>
        public string HostMask { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets the ident.
        /// </summary>
        /// <value>
        /// The ident.
        /// </value>
        public string Ident
        {
            get
            {
                return this.HostMask.Split('!', '@')[1];
            }
        }

        /// <summary>
        /// Gets the nick.
        /// </summary>
        /// <value>
        /// The nick.
        /// </value>
        public string Nick
        {
            get
            {
                return this.HostMask.Split('!')[0];
            }
        }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public Guid? UserId { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The compare to.
        /// </summary>
        /// <param name="hostmask">
        /// The hostmask.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int CompareTo(string hostmask)
        {
            return string.Compare(this.HostMask, hostmask, StringComparison.Ordinal);
        }

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="hostmask">
        /// The hostmask.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Equals(string hostmask)
        {
            return this.HostMask.Equals(hostmask, StringComparison.OrdinalIgnoreCase);
        }

        #endregion
    }
}