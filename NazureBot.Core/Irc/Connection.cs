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

namespace NazureBot.Core.Irc
{
    #region Using directives

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
    using NazureBot.Modules.Irc;
    using NazureBot.Modules.Messages;

    using Ninject;

    #endregion

    /// <summary>
    /// The connection.
    /// </summary>
    public sealed class Connection : IConnection, IStartable
    {
        #region Fields

        /// <summary>
        /// The irc client factory
        /// </summary>
        private readonly IIrcClientFactory ircClientFactory;

        /// <summary>
        /// The command registration service
        /// </summary>
        private readonly IRegistrationService registrationService;

        /// <summary>
        /// The request factory
        /// </summary>
        private readonly IRequestFactory requestFactory;

        /// <summary>
        /// The client.
        /// </summary>
        private IIrcClient client;

        /// <summary>
        /// The network
        /// </summary>
        private INetwork network;

        /// <summary>
        /// The server
        /// </summary>
        private IServer server;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Connection"/> class.
        /// </summary>
        /// <param name="registrationService">
        /// The registration service.
        /// </param>
        /// <param name="requestFactory">
        /// The request factory.
        /// </param>
        /// <param name="ircClientFactory">
        /// The irc client factory.
        /// </param>
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

        /// <summary>
        /// Prevents a default instance of the <see cref="Connection" /> class from being created.
        /// </summary>
        private Connection()
        {
            this.Modules = Enumerable.Empty<Module>();
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Occurs when [modules changed].
        /// </summary>
        public event EventHandler<ConnectionModulesChangedEventArgs> ModulesChanged;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the client.
        /// </summary>
        /// <value>
        /// The client.
        /// </value>
        public IIrcClient Client
        {
            get
            {
                return this.client;
            }

            private set
            {
                this.client = value;
            }
        }

        /// <summary>
        /// Gets or sets the modules.
        /// </summary>
        /// <value>
        /// The modules.
        /// </value>
        [ImportMany(typeof(Module), AllowRecomposition = true, RequiredCreationPolicy = CreationPolicy.Any, Source = ImportSource.Any)]
        public IEnumerable<Module> Modules { get; set; }

        /// <summary>
        /// Gets the network.
        /// </summary>
        /// <value>
        /// The network.
        /// </value>
        public INetwork Network
        {
            get
            {
                return this.network;
            }
        }

        /// <summary>
        /// Gets the server.
        /// </summary>
        /// <value>
        /// The server.
        /// </value>
        public IServer Server
        {
            get
            {
                return this.server;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Connects the asynchronous.
        /// </summary>
        /// <param name="network">
        /// The network.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task ConnectAsync(INetwork network)
        {
            this.network = network;
            await this.client.Connect(network);
        }

        /// <summary>
        /// Connects the asynchronous.
        /// </summary>
        /// <param name="server">
        /// The server.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task ConnectAsync(IServer server)
        {
            this.server = server;
            await this.Client.Connect(server);
        }

        /// <summary>
        /// Called when a part's imports have been satisfied and it is safe to use.
        /// </summary>
        public void OnImportsSatisfied()
        {
            this.RegisterModuleCommands();
            this.OnModulesChanged(new ConnectionModulesChangedEventArgs(this.Modules));
        }

        /// <summary>
        /// The send response.
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

            await this.client.SendResponseAsync(response);
        }

        /// <summary>
        /// Starts this instance. Called during activation.
        /// </summary>
        public void Start()
        {
            this.Client = this.ircClientFactory.Create();
            this.WireEvents();
            this.RegisterModuleCommands();
        }

        /// <summary>
        /// Stops this instance. Called during deactivation.
        /// </summary>
        public void Stop()
        {
            this.UnwireEvents();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Raises the <see cref="E:ModulesChanged"/> event.
        /// </summary>
        /// <param name="e">
        /// The <see cref="ConnectionModulesChangedEventArgs"/> instance containing the event data.
        /// </param>
        private void OnModulesChanged(ConnectionModulesChangedEventArgs e)
        {
            EventHandler<ConnectionModulesChangedEventArgs> handler = this.ModulesChanged;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Called when [private message received].
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="QueryMessageReceivedEventArgs"/> instance containing the event data.
        /// </param>
        private async void OnPrivateMessageReceived(object sender, QueryMessageReceivedEventArgs e)
        {
            await Task.Run(() =>
                {
                    foreach (var command in this.registrationService.RegisteredCommands)
                    {
                        if (e.Message.StartsWith(command.Trigger))
                        {
                            IRequest request = this.requestFactory.Create(e.User, e.Server, e.Format, e.Broadcast, e.Message, this);
                            command.Handler(request);
                        }
                    }

                    foreach (var module in this.Modules)
                    {
                        module.OnQueryMessageReceived(this, e);
                    }

                });
        }

        /// <summary>
        /// Called when [public message received].
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="ChannelMessageReceivedEventArgs"/> instance containing the event data.
        /// </param>
        private async void OnPublicMessageReceived(object sender, ChannelMessageReceivedEventArgs e)
        {
            await Task.Run(async () =>
                {
                    foreach (var command in this.registrationService.RegisteredCommands)
                    {
                        if (e.Message.StartsWith(command.Trigger))
                        {
                            IRequest request = this.requestFactory.Create(e.User, e.Server, e.Format, e.Broadcast, e.Message, this);
                            await command.Handler(request);
                        }
                    }

                    foreach (var module in this.Modules)
                    {
                        module.OnChannelMessageReceived(this, e);
                    }
                });
        }

        /// <summary>
        /// Called when [topic changed].
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="TopicChangedEventArgs"/> instance containing the event data.
        /// </param>
        private async void OnTopicChanged(object sender, TopicChangedEventArgs e)
        {
            await Task.Run(() =>
                {
                    foreach (var module in this.Modules)
                    {
                        module.OnTopicChanged(this, e);
                    }
                });
        }

        /// <summary>
        /// Called when [user joined].
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="UserJoinedEventArgs"/> instance containing the event data.
        /// </param>
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

        /// <summary>
        /// Called when [user kicked].
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="UserKickedEventArgs"/> instance containing the event data.
        /// </param>
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

        /// <summary>
        /// Called when [user quit].
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="UserQuitEventArgs"/> instance containing the event data.
        /// </param>
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

        /// <summary>
        /// Registers the module commands.
        /// </summary>
        private void RegisterModuleCommands()
        {
            this.registrationService.Clear();

            foreach (var module in this.Modules)
            {
                module.RegisterCommands(this.registrationService);
            }
        }

        /// <summary>
        /// Unwires the events.
        /// </summary>
        private void UnwireEvents()
        {
            this.client.PrivateMessageReceived -= this.OnPrivateMessageReceived;
            this.client.PublicMessageReceived -= this.OnPublicMessageReceived;
            this.client.TopicChanged -= this.OnTopicChanged;
            this.client.UserJoined -= this.OnUserJoined;
            this.client.UserKicked -= this.OnUserKicked;
            this.client.UserQuit -= this.OnUserQuit;
        }

        /// <summary>
        /// Wires the events.
        /// </summary>
        private void WireEvents()
        {
            this.client.PrivateMessageReceived += this.OnPrivateMessageReceived;
            this.client.PublicMessageReceived += this.OnPublicMessageReceived;
            this.client.TopicChanged += this.OnTopicChanged;
            this.client.UserJoined += this.OnUserJoined;
            this.client.UserKicked += this.OnUserKicked;
            this.client.UserQuit += this.OnUserQuit;
        }

        #endregion
    }
}