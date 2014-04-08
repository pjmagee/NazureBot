// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FakeUserService.cs" company="Patrick Magee">
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
//   The fake user service used for producing fake irc users.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Core.Services.User
{
    #region Using directives

    using System.Collections.Generic;
    using System.Threading.Tasks;

    using NazureBot.Core.Infrastructure.Entities;
    using NazureBot.Modules.Irc;

    #endregion

    /// <summary>
    /// The fake user service used for producing fake irc users.
    /// </summary>
    public class FakeUserService : IUserService
    {
        #region Public Methods and Operators

        /// <summary>
        /// Gets the or create by hostmask asynchronous.
        /// </summary>
        /// <param name="hostmask">
        /// The hostmask.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<IUser> GetOrCreateByHostmaskAsync(string hostmask)
        {
            return await Task.FromResult(new User("Peej!patrick.magee@192.168.0.1"));
        }

        /// <summary>
        /// Gets the users asynchronous.
        /// </summary>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        public async Task<IEnumerable<IUser>> GetUsersAsync()
        {
            var user1 = new User("user1!patrick.magee@192.168.0.1");
            var user2 = new User("user2!patrick.magee@192.168.0.1");

            var users = new[] { user1, user2 };

            return await Task.FromResult(users);
        }

        #endregion
    }
}