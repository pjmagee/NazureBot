// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChannelMessageReceivedEventArgs.cs" company="Patrick Magee">
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
//   The channel message received event args.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Modules.Events
{
    #region Using directives

    using System;

    using NazureBot.Modules.Irc;
    using NazureBot.Modules.Messages;

    #endregion

    /// <summary>
    /// The channel message received event args.
    /// </summary>
    public class ChannelMessageReceivedEventArgs : EventArgs
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelMessageReceivedEventArgs"/> class.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <param name="server">
        /// The server.
        /// </param>
        /// <param name="channel">
        /// The channel.
        /// </param>
        /// <param name="format">
        /// The format.
        /// </param>
        /// <param name="broadcast">
        /// The broadcast.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        public ChannelMessageReceivedEventArgs(IUser user, IServer server, IChannel channel, MessageFormat format, MessageBroadcast broadcast, string message)
        {
            this.User = user;
            this.Server = server;
            this.Channel = channel;
            this.Format = format;
            this.Broadcast = broadcast;
            this.Message = message;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the broadcast.
        /// </summary>
        /// <value>
        /// The broadcast.
        /// </value>
        public MessageBroadcast Broadcast { get; set; }

        /// <summary>
        /// Gets or sets the channel.
        /// </summary>
        /// <value>
        /// The channel.
        /// </value>
        public IChannel Channel { get; set; }

        /// <summary>
        /// Gets or sets the format.
        /// </summary>
        /// <value>
        /// The format.
        /// </value>
        public MessageFormat Format { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the server.
        /// </summary>
        /// <value>
        /// The server.
        /// </value>
        public IServer Server { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public IUser User { get; set; }

        #endregion
    }
}