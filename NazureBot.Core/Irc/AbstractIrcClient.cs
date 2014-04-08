// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AbstractIrcClient.cs" company="Patrick Magee">
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
//   The abstract irc client.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Core.Irc
{
    #region Using directives

    using System;
    using System.Threading.Tasks;

    using NazureBot.Modules.Events;
    using NazureBot.Modules.Irc;
    using NazureBot.Modules.Messages;

    #endregion

    /// <summary>
    /// The abstract irc client.
    /// </summary>
    public abstract class AbstractIrcClient : IIrcClient
    {
        #region Public Events

        /// <summary>
        /// Occurs when [private message received].
        /// </summary>
        public event EventHandler<QueryMessageReceivedEventArgs> PrivateMessageReceived;

        /// <summary>
        /// Occurs when [public message received].
        /// </summary>
        public event EventHandler<ChannelMessageReceivedEventArgs> PublicMessageReceived;

        /// <summary>
        /// Occurs when [topic changed].
        /// </summary>
        public event EventHandler<TopicChangedEventArgs> TopicChanged;

        /// <summary>
        /// Occurs when [user joined].
        /// </summary>
        public event EventHandler<UserJoinedEventArgs> UserJoined;

        /// <summary>
        /// Occurs when [user kicked].
        /// </summary>
        public event EventHandler<UserKickedEventArgs> UserKicked;

        /// <summary>
        /// Occurs when [user quit].
        /// </summary>
        public event EventHandler<UserQuitEventArgs> UserQuit;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public abstract string Description { get; }

        /// <summary>
        /// Gets a value indicating whether [is connected].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is connected]; otherwise, <c>false</c>.
        /// </value>
        public abstract bool IsConnected { get; }

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
        public abstract Task Connect(INetwork network);

        /// <summary>
        /// Connects the specified server.
        /// </summary>
        /// <param name="server">
        /// The server.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public abstract Task Connect(IServer server);

        /// <summary>
        /// Disconnects this instance.
        /// </summary>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        public abstract Task Disconnect();

        /// <summary>
        /// Sends the response asynchronous.
        /// </summary>
        /// <param name="response">
        /// The response.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public abstract Task SendResponseAsync(IResponse response);

        #endregion

        #region Methods

        /// <summary>
        /// Raises the <see cref="E:PrivateMessageReceived"/> event.
        /// </summary>
        /// <param name="e">
        /// The <see cref="QueryMessageReceivedEventArgs"/> instance containing the event data.
        /// </param>
        protected virtual void OnPrivateMessageReceived(QueryMessageReceivedEventArgs e)
        {
            EventHandler<QueryMessageReceivedEventArgs> handler = this.PrivateMessageReceived;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:PublicMessageReceived"/> event.
        /// </summary>
        /// <param name="e">
        /// The <see cref="ChannelMessageReceivedEventArgs"/> instance containing the event data.
        /// </param>
        protected virtual void OnPublicMessageReceived(ChannelMessageReceivedEventArgs e)
        {
            EventHandler<ChannelMessageReceivedEventArgs> handler = this.PublicMessageReceived;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:TopicChanged"/> event.
        /// </summary>
        /// <param name="e">
        /// The <see cref="TopicChangedEventArgs"/> instance containing the event data.
        /// </param>
        protected virtual void OnTopicChanged(TopicChangedEventArgs e)
        {
            EventHandler<TopicChangedEventArgs> handler = this.TopicChanged;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:UserJoined"/> event.
        /// </summary>
        /// <param name="e">
        /// The <see cref="UserJoinedEventArgs"/> instance containing the event data.
        /// </param>
        protected virtual void OnUserJoined(UserJoinedEventArgs e)
        {
            EventHandler<UserJoinedEventArgs> handler = this.UserJoined;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:UserKicked"/> event.
        /// </summary>
        /// <param name="e">
        /// The <see cref="UserKickedEventArgs"/> instance containing the event data.
        /// </param>
        protected virtual void OnUserKicked(UserKickedEventArgs e)
        {
            EventHandler<UserKickedEventArgs> handler = this.UserKicked;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:UserQuit"/> event.
        /// </summary>
        /// <param name="e">
        /// The <see cref="UserQuitEventArgs"/> instance containing the event data.
        /// </param>
        protected virtual void OnUserQuit(UserQuitEventArgs e)
        {
            EventHandler<UserQuitEventArgs> handler = this.UserQuit;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion
    }
}