// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FakeIrcClient.cs" company="Patrick Magee">
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
//   Defines the FakeIrcClient type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Core.Messaging
{
    #region Using directives

    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;

    using NazureBot.Core.Infrastructure.Entities;
    using NazureBot.Modules.Events;
    using NazureBot.Modules.Messages;
    using NazureBot.Modules.Messaging;

    using Ninject;

    #endregion

    /// <summary>
    /// The fake irc client used to simulate irc chatter from an irc server.
    /// </summary>
    public class FakeIrcClient : AbstractClient, IStartable
    {
        #region Fields

        /// <summary>
        /// The is connected
        /// </summary>
        private bool isConnected;

        /// <summary>
        /// The network
        /// </summary>
        private INetwork network;

        /// <summary>
        /// The server
        /// </summary>
        private IServer server;

        /// <summary>
        /// The token source
        /// </summary>
        private CancellationTokenSource tokenSource;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the description.
        /// </summary>
        public override string Description
        {
            get
            {
                return typeof(FakeIrcClient).FullName;
            }
        }

        /// <summary>
        /// Gets a value indicating whether [is connected].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is connected]; otherwise, <c>false</c>.
        /// </value>
        public override bool IsConnected
        {
            get
            {
                return this.isConnected;
            }
        }

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
        public override async Task Connect(INetwork network)
        {
            this.network = network;
            Trace.TraceInformation("Connected to: {0}", network.Name);
            this.isConnected = true;
        }

        /// <summary>
        /// Connects the specified server.
        /// </summary>
        /// <param name="server">
        /// The server.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public override async Task Connect(IServer server)
        {
            this.server = server;
            Trace.TraceInformation("Connected to: {0}", server.Address);
            this.isConnected = true;
        }

        /// <summary>
        /// Disconnects this instance.
        /// </summary>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        public override async Task Disconnect()
        {
            this.isConnected = false;
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
        public override async Task SendResponseAsync(IResponse response)
        {
            Trace.TraceInformation("Sending response: {0}", response.Message);
        }

        /// <summary>
        /// Starts this instance. Called during activation.
        /// </summary>
        public void Start()
        {
            this.tokenSource = new CancellationTokenSource();

            Task.Factory.StartNew(() =>
                {
                    while (true)
                    {
                        if (this.isConnected)
                        {
                            var user = new User("Peej!patrick.magee@192.168.0.1");
                            var channel = new Channel { Name = "#fake", Network = this.network as Network };

                            this.OnPrivateMessageReceived(new PrivateMessageReceivedEventArgs(user, this.server, MessageFormat.Message, MessageBroadcast.Private, "fake message"));
                            this.OnPublicMessageReceived(new PublicMessageReceivedEventArgs(user, this.server, channel, MessageFormat.Message, MessageBroadcast.Public, "fake message"));
                        }

                        Thread.Sleep(TimeSpan.FromSeconds(20));
                    }
                }, this.tokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Current);
        }

        /// <summary>
        /// Stops this instance. Called during deactivation.
        /// </summary>
        public void Stop()
        {
            this.tokenSource.Cancel();
        }

        #endregion
    }
}