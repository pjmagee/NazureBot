// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Connection.cs" company="Patrick Magee">
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
//   The connection.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Core.Messaging
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Threading.Tasks;

    using NazureBot.Core.Factories;
    using NazureBot.Modules;
    using NazureBot.Modules.Commands;
    using NazureBot.Modules.Events;
    using NazureBot.Modules.Messages;
    using NazureBot.Modules.Messaging;

    using Ninject;

    public sealed class Connection : IConnection, IStartable
    {
        private readonly IIrcClientFactory ircClientFactory;
        private readonly IRegistrationService registrationService;
        private readonly IRequestFactory requestFactory;

        private IChatClient client;
        private INetwork network;
        private IServer server;

        public event EventHandler<ConnectionModulesChangedEventArgs> ModulesChanged;      

        [Inject]
        public Connection(IRegistrationService registrationService, IRequestFactory requestFactory, IIrcClientFactory ircClientFactory) : this()
        {
            Contract.Requires<ArgumentNullException>(registrationService != null, "registrationService");
            Contract.Requires<ArgumentNullException>(requestFactory != null, "requestFactory");
            Contract.Requires<ArgumentNullException>(ircClientFactory != null, "ircClientFactory");

            this.registrationService = registrationService;
            this.requestFactory = requestFactory;
            this.ircClientFactory = ircClientFactory;
        }

        private Connection()
        {
            this.Modules = Enumerable.Empty<Module>();
        }   

        [ImportMany(typeof(Module), AllowRecomposition = true, RequiredCreationPolicy = CreationPolicy.Any, Source = ImportSource.Any)]
        public IEnumerable<Module> Modules { get; set; }

        public IChatClient Client
        {
            get { return this.client; }
            private set { this.client = value; }
        }

        public INetwork Network
        {
            get { return this.network; }
        }

        public IServer Server
        {
            get { return this.server; }
        }

        public async Task ConnectAsync(INetwork network)
        {
            this.network = network;
            await this.client.Connect(network);
        }

        public async Task ConnectAsync(IServer server)
        {
            this.server = server;
            await this.Client.Connect(server);
        }

        public void OnImportsSatisfied()
        {
            this.RegisterModuleCommands();
            this.OnModulesChanged(new ConnectionModulesChangedEventArgs(this.Modules));
        }

        public async Task SendResponseAsync(IResponse response)
        {
            Contract.Requires<ArgumentNullException>(response != null, "response");

            await this.client.SendResponseAsync(response);
        }

        public void Start()
        {
            this.Client = this.ircClientFactory.Create();
            this.WireEvents();
            this.RegisterModuleCommands();
        }

        public void Stop()
        {
            this.UnwireEvents();
        }

        private void OnModulesChanged(ConnectionModulesChangedEventArgs e)
        {
            EventHandler<ConnectionModulesChangedEventArgs> handler = this.ModulesChanged;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        private async void OnPrivateMessageReceived(object sender, PrivateMessageReceivedEventArgs e)
        {
            await Task.Run(() =>
                {
                    foreach (var command in this.registrationService.RegisteredCommands)
                    {
                        if (e.Message.StartsWith(command.Trigger))
                        {
                            var  request = this.requestFactory.Create(e.User, e.Server, e.Format, e.Broadcast, e.Message, this);
                            command.Handler(request);
                        }
                    }

                    foreach (var module in this.Modules)
                    {
                        module.OnQueryMessageReceived(this, e);
                    }

                });
        }

        private async void OnPublicMessageReceived(object sender, PublicMessageReceivedEventArgs e)
        {
            foreach (var command in this.registrationService.RegisteredCommands)
            {
                if (e.Message.StartsWith(command.Trigger))
                {
                    IRequest request = this.requestFactory.Create(e.FromUser, e.Server, e.Format, e.Broadcast, e.Message, this);
                    await command.Handler(request);
                }
            }

            foreach (var module in this.Modules)
            {
                module.OnChannelMessageReceived(this, e);
            }
        }

        private async void OnTopicChanged(object sender, TopicChangedEventArgs e)
        {
            foreach (var module in this.Modules)
            {
                module.OnTopicChanged(this, e);
            }
        }

        private async void OnUserJoined(object sender, UserJoinedEventArgs e)
        {
            await Task.Run(() =>
                {
                    foreach (var module in this.Modules)
                    {
                        module.OnUserJoined(this, e);
                    }
                });
        }

        private async void OnUserKicked(object sender, UserKickedEventArgs e)
        {
            await Task.Run(() =>
                {
                    foreach (var module in this.Modules)
                    {
                        module.OnUserKicked(this, e);
                    }
                });
        }

        private async void OnUserQuit(object sender, UserQuitEventArgs e)
        {
            await Task.Run(() =>
                {
                    foreach (var module in this.Modules)
                    {
                        module.OnUserQuit(this, e);
                    }
                });
        }

        private void RegisterModuleCommands()
        {
            this.registrationService.Clear();

            foreach (var module in this.Modules)
            {
                module.RegisterCommands(this.registrationService);
            }
        }

        private void UnwireEvents()
        {
            this.client.PrivateMessageReceived -= this.OnPrivateMessageReceived;
            this.client.PublicMessageReceived -= this.OnPublicMessageReceived;
            this.client.TopicChanged -= this.OnTopicChanged;
            this.client.UserJoined -= this.OnUserJoined;
            this.client.UserKicked -= this.OnUserKicked;
            this.client.UserQuit -= this.OnUserQuit;
        }

        private void WireEvents()
        {
            this.client.PrivateMessageReceived += this.OnPrivateMessageReceived;
            this.client.PublicMessageReceived += this.OnPublicMessageReceived;
            this.client.TopicChanged += this.OnTopicChanged;
            this.client.UserJoined += this.OnUserJoined;
            this.client.UserKicked += this.OnUserKicked;
            this.client.UserQuit += this.OnUserQuit;
        }
    }
}