// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRequest.cs" company="Patrick Magee">
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
//   The Request interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Modules.Messages
{
    #region Using directives

    using System.Threading.Tasks;

    using NazureBot.Modules.Irc;

    #endregion

    /// <summary>
    /// The Request interface.
    /// </summary>
    public interface IRequest
    {
        #region Public Properties

        /// <summary>
        /// Gets the broadcast.
        /// </summary>
        /// <value>
        /// The broadcast.
        /// </value>
        MessageBroadcast Broadcast { get; }

        /// <summary>
        /// Gets the channel.
        /// </summary>
        /// <value>
        /// The channel.
        /// </value>
        IChannel Channel { get; }

        /// <summary>
        /// Gets the format.
        /// </summary>
        /// <value>
        /// The format.
        /// </value>
        MessageFormat Format { get; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        string Message { get; }

        /// <summary>
        /// Gets the server.
        /// </summary>
        /// <value>
        /// The server.
        /// </value>
        IServer Server { get; }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        IUser User { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Creates the response.
        /// </summary>
        /// <returns>
        /// The <see cref="IResponse"/>.
        /// </returns>
        IResponse CreateResponse();

        /// <summary>
        /// Sends the response asynchronous.
        /// </summary>
        /// <param name="response">
        /// The response.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task SendResponseAsync(IResponse response);

        #endregion
    }
}