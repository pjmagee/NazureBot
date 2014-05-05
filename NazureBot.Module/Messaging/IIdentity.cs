// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IIdentity.cs" company="Patrick Magee">
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
//   The Identity interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Modules.Messaging
{
    #region Using directives

    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// The Identity interface.
    /// </summary>
    /// <remarks>
    /// Used as a way for the bot to give an identity when registering with external messaging services. 
    /// This enables users to easily identify the bot.
    /// </remarks>
    public interface IIdentity
    {
        #region Public Properties

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        string Description { get; }

        /// <summary>
        /// Gets the networks.
        /// </summary>
        /// <value>
        /// The networks.
        /// </value>
        IEnumerable<INetwork> Networks { get; }

        /// <summary>
        /// Gets the name of the nick.
        /// </summary>
        /// <value>
        /// The name of the nick.
        /// </value>
        string NickName { get; }

        /// <summary>
        /// Gets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        string Password { get; }

        /// <summary>
        /// Gets the name of the real.
        /// </summary>
        /// <value>
        /// The name of the real.
        /// </value>
        string RealName { get; }

        /// <summary>
        /// Gets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        string UserName { get; }

        #endregion
    }
}