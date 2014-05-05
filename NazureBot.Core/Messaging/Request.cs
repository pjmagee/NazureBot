// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Request.cs" company="Patrick Magee">
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
//   The request.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Core.Messaging
{
    #region Using directives

    using System;
    using System.Diagnostics.Contracts;
    using System.Threading.Tasks;

    using NazureBot.Core.Factories;
    using NazureBot.Modules.Messages;
    using NazureBot.Modules.Messaging;

    using Ninject;

    #endregion

    /// <summary>
    /// The request.
    /// </summary>
    public sealed class Request : IRequest
    {
        #region Fields

        /// <summary>
        /// The connection
        /// </summary>
        private readonly IConnection connection;

        /// <summary>
        /// The response factory
        /// </summary>
        private readonly IResponseFactory responseFactory;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Request"/> class.
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
        [Inject]
        public Request(IUser user, IServer server, MessageFormat messageFormat, MessageBroadcast messageBroadcast, string message, IConnection connection)
        {
            Contract.Requires<ArgumentNullException>(user != null, "user");
            Contract.Requires<ArgumentNullException>(server != null, "server");
            Contract.Requires<ArgumentNullException>(message != null, "message");
            Contract.Requires<ArgumentNullException>(connection != null, "connection");

            this.User = user;
            this.Server = server;
            this.Format = messageFormat;
            this.Broadcast = messageBroadcast;
            this.Message = message;
            this.connection = connection;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Request"/> class.
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
        /// <param name="connection">
        /// The connection.
        /// </param>
        [Inject]
        public Request(IUser user, IServer server, IChannel channel, MessageFormat format, MessageBroadcast broadcast, string message, IConnection connection)
        {
            Contract.Requires<ArgumentNullException>(user != null, "user");
            Contract.Requires<ArgumentNullException>(server != null, "server");
            Contract.Requires<ArgumentNullException>(channel != null, "channel");
            Contract.Requires<ArgumentNullException>(message != null, "message");
            Contract.Requires<ArgumentNullException>(connection != null, "connection");

            this.User = user;
            this.Server = server;
            this.Channel = channel;
            this.Format = format;
            this.Broadcast = broadcast;
            this.Message = message;
            this.connection = connection;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="Request"/> class from being created.
        /// </summary>
        private Request()
        {
            
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the broadcast.
        /// </summary>
        /// <value>
        /// The broadcast.
        /// </value>
        public MessageBroadcast Broadcast { get; private set; }

        /// <summary>
        /// Gets the channel.
        /// </summary>
        /// <value>
        /// The channel.
        /// </value>
        public IChannel Channel { get; private set; }

        /// <summary>
        /// Gets the format.
        /// </summary>
        /// <value>
        /// The format.
        /// </value>
        public MessageFormat Format { get; private set; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; private set; }

        /// <summary>
        /// Gets the server.
        /// </summary>
        /// <value>
        /// The server.
        /// </value>
        public IServer Server { get; private set; }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public IUser User { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Creates the response.
        /// </summary>
        /// <returns>
        /// The <see cref="IResponse" />.
        /// </returns>
        public IResponse CreateResponse()
        {
            IResponse response = this.responseFactory.Create(this);

            return response;
        }

        /// <summary>
        /// Sends the response asynchronous.
        /// </summary>
        /// <param name="response">
        /// The response.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task SendResponseAsync(IResponse response)
        {
            Contract.Requires<ArgumentNullException>(response != null, "response");

            await this.connection.Client.SendResponseAsync(response);
        }

        #endregion
    }
}