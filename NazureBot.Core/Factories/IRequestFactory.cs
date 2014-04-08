// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRequestFactory.cs" company="Patrick Magee">
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
//   The RequestFactory interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Core.Factories
{
    #region Using directives

    using NazureBot.Modules.Irc;
    using NazureBot.Modules.Messages;

    #endregion

    /// <summary>
    /// The RequestFactory interface.
    /// </summary>
    public interface IRequestFactory
    {
        #region Public Methods and Operators

        /// <summary>
        /// Creates the specified user.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <param name="server">
        /// The server.
        /// </param>
        /// <param name="messageFormat">
        /// The message format.
        /// </param>
        /// <param name="messageBroadcast">
        /// The message broadcast.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="connection">
        /// The connection.
        /// </param>
        /// <returns>
        /// The <see cref="IRequest"/>.
        /// </returns>
        IRequest Create(IUser user, IServer server, MessageFormat messageFormat, MessageBroadcast messageBroadcast, string message, IConnection connection);

        #endregion
    }
}