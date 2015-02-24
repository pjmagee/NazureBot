// --------------------------------------------------------------------------------------------------------------------
// <copyright file="fromUser.cs" company="Patrick Magee">
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
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    using NazureBot.Modules.Messaging;
    using NazureBot.Modules.Security;

    public class User : IUser
    {
        public Guid Id { get; set; }
        public AccessLevel AccessLevel { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public KnownHost Host { get; set; }        
        public string Password { get; set; }

        public virtual ICollection<KnownHost> KnownHosts { get; set; }

        public User()
        {
            this.Id = Guid.NewGuid();
            this.AccessLevel = AccessLevel.Guest;
        }

        public User(string hostMask) : this()
        {
            Contract.Requires<ArgumentNullException>(hostMask != null, "hostMask");

            this.Host = new KnownHost(hostMask, this);
        }
        
        AccessLevel IUser.AccessLevel
        {
            get { return this.AccessLevel; }
        }

        IKnownHost IUser.Host
        {
            get { return this.Host; }
        }
    }
}
