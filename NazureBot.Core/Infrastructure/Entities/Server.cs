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
    using System;
    using NazureBot.Modules.Messaging;

    public class Server : IServer
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public bool IsSsl { get; set; }
        public virtual Network Network { get; set; }
        public Guid? NetworkId { get; set; }
        public int? Port { get; set; }

        int IServer.Port
        {
            get { return this.Port.GetValueOrDefault(6667); }
        }

        bool IServer.Ssl
        {
            get { return this.IsSsl; }
        }
    }
}