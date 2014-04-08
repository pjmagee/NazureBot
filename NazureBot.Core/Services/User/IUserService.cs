// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserService.cs" company="Patrick Magee">
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
//   The UserService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Core.Services.User
{
    #region Using directives

    using System.Collections.Generic;
    using System.Threading.Tasks;

    using NazureBot.Modules.Irc;

    #endregion

    /// <summary>
    /// The UserService interface.
    /// </summary>
    public interface IUserService
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
        Task<IUser> GetOrCreateByHostmaskAsync(string hostmask);

        /// <summary>
        /// Gets the users asynchronous.
        /// </summary>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        Task<IEnumerable<IUser>> GetUsersAsync();

        #endregion
    }
}