// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Bot.cs" company="Patrick Magee">
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
//   The bot implementation.
//   Also to be injected as a singleton instance.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Core
{
    #region Using directives

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    using NazureBot.Core.Events;
    using NazureBot.Core.Factories;
    using NazureBot.Core.Services.Host;
    using NazureBot.Core.Services.Module;
    using NazureBot.Core.Services.Network;
    using NazureBot.Modules.Messaging;

    using Ninject;

    #endregion

    /// <summary>
    /// The bot implementation.
    /// Also to be injected as a singleton instance.
    /// </summary>
    public sealed class Bot : IBot, IStartable
    {
        #region Fields

        /// <summary>
        /// The connection factory
        /// </summary>
        private readonly IConnectionFactory connectionFactory;
        

        /// <summary>
        /// The connection service.
        /// </summary>
        private readonly IConnectionService connectionService;

        /// <summary>
        /// The connections
        /// </summary>
        private readonly List<IConnection> connections;

        /// <summary>
        /// The module service.
        /// </summary>
        private readonly IModuleService moduleService;

        /// <summary>
        /// The network service.
        /// </summary>
        private readonly INetworkService networkService;

        /// <summary>
        /// The status
        /// </summary>
        private BotStatus status;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Bot"/> class.
        /// </summary>
        /// <param name="networkService">
        /// The network Service.
        /// </param>
        /// <param name="connectionFactory">
        /// The connection factory.
        /// </param>
        [Inject]
        public Bot(INetworkService networkService, IConnectionFactory connectionFactory) : this()
        {
            this.networkService = networkService;
            this.connectionFactory = connectionFactory;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="Bot"/> class from being created.
        /// </summary>
        private Bot()
        {
            this.connections = new List<IConnection>();
            this.Status = BotStatus.Stopped;
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Occurs when [status changed].
        /// </summary>
        public event EventHandler<BotStatusChangedEventArgs> StatusChanged;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public BotStatus Status
        {
            get
            {
                return this.status;
            }

            private set
            {
                this.OnStatusChanged(new BotStatusChangedEventArgs(value, this.status));
                this.status = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Starts this instance. Called during activation.
        /// </summary>
        public async void Start()
        {
            IEnumerable<INetwork> networks = await this.networkService.GetNetworksAsync();

            foreach (var network in networks)
            {
                // connect to network
                var connection = this.connectionFactory.Create(network);
                this.connections.Add(connection);
                Trace.TraceInformation("Added connection: {0}", connection);
            } 
        }

        /// <summary>
        /// Stops this instance. Called during deactivation.
        /// </summary>
        public void Stop()
        {
            
        }

        #endregion

        #region Methods

        /// <summary>
        /// Raises the <see cref="E:StatusChanged"/> event.
        /// </summary>
        /// <param name="e">
        /// The <see cref="BotStatusChangedEventArgs"/> instance containing the event data.
        /// </param>
        private void OnStatusChanged(BotStatusChangedEventArgs e)
        {
            Trace.TraceInformation("Bot status changed. Old: {0} New: {1}", e.OldBotStatus, e.NewBotStatus);

            EventHandler<BotStatusChangedEventArgs> handler = this.StatusChanged;
            
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion
    }
}