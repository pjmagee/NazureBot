// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QueryMessageReceivedEventArgs.cs" company="Patrick Magee">
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
//   The query message received event args.
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
    /// The query message received event args.
    /// </summary>
    public class QueryMessageReceivedEventArgs : EventArgs
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryMessageReceivedEventArgs"/> class.
        /// </summary>
        public QueryMessageReceivedEventArgs()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryMessageReceivedEventArgs"/> class.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <param name="server">
        /// The server.
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
        public QueryMessageReceivedEventArgs(IUser user, IServer server, MessageFormat format, MessageBroadcast broadcast, string message)
        {
            this.User = user;
            this.Server = server;
            this.Format = format;
            this.Broadcast = broadcast;
            this.Message = message;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the broadcast.
        /// </summary>
        public MessageBroadcast Broadcast { get; private set; }

        /// <summary>
        /// Gets the format.
        /// </summary>
        public MessageFormat Format { get; private set; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Gets the server.
        /// </summary>
        public IServer Server { get; private set; }

        /// <summary>
        /// Gets the user.
        /// </summary>
        public IUser User { get; private set; }

        #endregion
    }
}