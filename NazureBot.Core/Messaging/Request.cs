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
    using System;
    using System.Diagnostics.Contracts;
    using System.Threading.Tasks;

    using NazureBot.Core.Factories;
    using NazureBot.Modules.Messages;
    using NazureBot.Modules.Messaging;

    using Ninject;

    /// <summary>
    /// The request.
    /// </summary>
    public sealed class Request : IRequest
    {
        private readonly IConnection connection;
        private readonly IResponseFactory responseFactory;

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

        private Request() { }

        public MessageBroadcast Broadcast { get; private set; }
        public IChannel Channel { get; private set; }
        public MessageFormat Format { get; private set; }
        public string Message { get; private set; }
        public IServer Server { get; private set; }
        public IUser User { get; private set; }

        public IResponse CreateResponse()
        {
            IResponse response = this.responseFactory.Create(this);

            return response;
        }

        public async Task SendResponseAsync(IResponse response)
        {
            Contract.Requires<ArgumentNullException>(response != null, "response");

            await this.connection.Client.SendResponseAsync(response);
        }
    }
}