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
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NazureBot.Modules.Messaging;

    public class Identity : IIdentity
    {
        public Guid Id { get; set; }

        public string Description { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }
        public string RealName { get; set; }
        public string UserName { get; set; }

        public virtual ICollection<Network> Networks { get; set; }

        IEnumerable<INetwork> IIdentity.Networks
        {
            get
            {
                return this.Networks.ToList();
            }
        }
    }

    
}