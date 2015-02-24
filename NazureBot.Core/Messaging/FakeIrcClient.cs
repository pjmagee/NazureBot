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
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;

    using NazureBot.Core.Infrastructure.Entities;
    using NazureBot.Modules.Events;
    using NazureBot.Modules.Messages;
    using NazureBot.Modules.Messaging;

    using Ninject;

    /// <summary>
    /// The fake irc client used to simulate irc chatter from an irc server.
    /// </summary>
    public class FakeIrcClient : AbstractClient, IStartable
    {
        private bool isConnected;
        private INetwork network;
        private IServer server;
        private CancellationTokenSource tokenSource;

        public override string Description
        {
            get { return typeof(FakeIrcClient).FullName; }
        }

        public override bool IsConnected
        {
            get { return this.isConnected; }
        }

        public override async Task Connect(INetwork network)
        {
            this.network = network;
            Trace.TraceInformation("Connected to: {0}", network.Name);
            this.isConnected = true;
        }

        public override async Task Connect(IServer server)
        {
            this.server = server;
            Trace.TraceInformation("Connected to: {0}", server.Address);
            this.isConnected = true;
        }

        public override async Task Disconnect()
        {
            this.isConnected = false;
        }

        public override async Task SendResponseAsync(IResponse response)
        {
            Trace.TraceInformation("Sending response: {0}", response.Message);
        }

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

        public void Stop()
        {
            this.tokenSource.Cancel();
        }
    }
}