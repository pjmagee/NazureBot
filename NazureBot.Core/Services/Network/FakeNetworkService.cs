// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FakeNetworkService.cs" company="Patrick Magee">
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
//   A
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Core.Services.Network
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    using NazureBot.Core.Infrastructure.Entities;
    using NazureBot.Modules.Messaging;

    public class FakeNetworkService : INetworkService
    {
        public async Task<IEnumerable<INetwork>> GetNetworksAsync()
        {
            var networks = new[]
                {
                    new Network 
                    {
                            Name = "network 1", 
                            Servers = new Collection<Server>(new List<Server>(new[] { new Server() }))
                    }, 
                    new Network 
                    {
                            Name = "network 2", 
                            Servers = new Collection<Server>(new List<Server>(new[] { new Server() }))
                    }, 
                    new Network 
                    {
                            Name = "network 3",  
                            Servers = new Collection<Server>(new List<Server>(new[] { new Server() }))
                    } 
                };

            return await Task.FromResult(networks);
        }
    }
}