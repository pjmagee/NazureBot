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
    using System;
    using System.Collections.Generic;

    using NazureBot.Modules.Messaging;

    public class Network : INetwork
    {
        public Guid Id { get; set; }

        public virtual Identity Identity { get; set; }
        public Guid? IdentityId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Server> Servers { get; set; }
        public virtual ICollection<Channel> Channels { get; set; }

        IEnumerable<IChannel> INetwork.Channels
        {
            get { return this.Channels; }
        }

        IIdentity INetwork.Identity
        {
            get { return this.Identity; }
        }

        string INetwork.Name
        {
            get { return this.Name; }
        }

        IEnumerable<IServer> INetwork.Servers
        {
            get { return this.Servers; }
        }
    }
}