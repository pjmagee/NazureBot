// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IChatClient.cs" company="Patrick Magee">
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
//   The IrcClient interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Modules.Messaging
{
    #region Using directives

    using System;
    using System.Threading.Tasks;

    using NazureBot.Modules.Events;
    using NazureBot.Modules.Messages;

    #endregion

    /// <summary>
    /// The IChatClient interface.
    /// </summary>
    public interface IChatClient
    {
        #region Public Events

        /// <summary>
        /// Occurs when [private message received].
        /// </summary>
        event EventHandler<PrivateMessageReceivedEventArgs> PrivateMessageReceived;

        /// <summary>
        /// Occurs when [public message received].
        /// </summary>
        event EventHandler<PublicMessageReceivedEventArgs> PublicMessageReceived;

        /// <summary>
        /// Occurs when [topic changed].
        /// </summary>
        event EventHandler<TopicChangedEventArgs> TopicChanged;

        /// <summary>
        /// Occurs when [user joined].
        /// </summary>
        event EventHandler<UserJoinedEventArgs> UserJoined;

        /// <summary>
        /// Occurs when [user kicked].
        /// </summary>
        event EventHandler<UserKickedEventArgs> UserKicked;

        /// <summary>
        /// Occurs when [user quit].
        /// </summary>
        event EventHandler<UserQuitEventArgs> UserQuit;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        string Description { get; }

        /// <summary>
        /// Gets a value indicating whether [is connected].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is connected]; otherwise, <c>false</c>.
        /// </value>
        bool IsConnected { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Connects the specified network.
        /// </summary>
        /// <param name="network">
        /// The network.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task Connect(INetwork network);

        /// <summary>
        /// Connects the specified server.
        /// </summary>
        /// <param name="server">
        /// The server.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task Connect(IServer server);

        /// <summary>
        /// Disconnects this instance.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task Disconnect();

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
