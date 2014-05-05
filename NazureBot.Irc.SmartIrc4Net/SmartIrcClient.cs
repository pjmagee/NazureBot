// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SmartIrcClient.cs" company="Patrick Magee">
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
//   The smart irc client.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Irc.SmartIrc4Net
{
    #region Using directives

    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Meebey.SmartIrc4net;

    using NazureBot.Core.Messaging;
    using NazureBot.Core.Services.User;
    using NazureBot.Modules.Events;
    using NazureBot.Modules.Messages;
    using NazureBot.Modules.Messaging;

    #endregion

    /// <summary>
    /// The smart irc client.
    /// </summary>
    public class SmartIrcClient : AbstractClient
    {
        #region Fields

        /// <summary>
        /// The irc client
        /// </summary>
        private readonly IrcClient ircClient;

        /// <summary>
        /// The user service
        /// </summary>
        private readonly IUserService userService;

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
        /// Initializes a new instance of the <see cref="SmartIrcClient"/> class.
        /// </summary>
        /// <param name="userService">
        /// The user service.
        /// </param>
        public SmartIrcClient(IUserService userService) : this()
        {
            this.userService = userService;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="SmartIrcClient"/> class from being created.
        /// </summary>
        private SmartIrcClient()
        {
            this.ircClient = new IrcClient
            {
                AutoRejoinOnKick = true,
                AutoRelogin = true,
                AutoReconnect = true,
                AutoRetryLimit = 99,
                AutoRetryDelay = 30,
                AutoNickHandling = true
            };
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public override string Description
        {
            get
            {
                return typeof(IrcClient).Assembly.GetName().FullName;
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
                return this.ircClient != null && this.ircClient.IsConnected;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Connects the specified server.
        /// </summary>
        /// <param name="server">
        /// The server.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async override Task Connect(IServer server)
        {
            this.ircClient.Connect(server.Address, server.Port);
            this.ircClient.OnConnected -= this.OnConnected;
            this.ircClient.OnConnected += this.OnConnected;
        }

        /// <summary>
        /// The connect.
        /// </summary>
        /// <param name="network">
        /// The network.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async override Task Connect(INetwork network)
        {
            this.network = network;
            this.server = network.Servers.First();
        }

        /// <summary>
        /// Disconnects this instance.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async override Task Disconnect()
        {
            await Task.Run(() => this.ircClient.Disconnect());
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
        public async override Task SendResponseAsync(IResponse response)
        {
            await Task.Run(() =>
            {
                var sendType = SendType.Message;

                switch (response.Format)
                {
                    case MessageFormat.Message:
                        sendType = SendType.Message; break;
                    case MessageFormat.Action:
                        sendType = SendType.Action; break;
                    case MessageFormat.Notice:
                        sendType = SendType.Notice; break;
                }

                foreach (var target in response.Targets)
                {
                    this.ircClient.SendMessage(sendType, target, response.Message);
                }
            });
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called when [away].
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="AwayEventArgs"/> instance containing the event data.
        /// </param>
        private void OnAway(object sender, AwayEventArgs e)
        {
            
        }

        /// <summary>
        /// Called when [channel message].
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="IrcEventArgs"/> instance containing the event data.
        /// </param>
        private void OnChannelMessageReceived(object sender, IrcEventArgs e)
        {
            // this.OnPublicMessageReceived(new PublicMessageReceivedEventArgs());
        }

        /// <summary>
        /// Called when [connected].
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="eventArgs">
        /// The <see cref="EventArgs"/> instance containing the event data.
        /// </param>
        private void OnConnected(object sender, EventArgs eventArgs)
        {
            this.ircClient.Login(this.network.Identity.NickName, this.network.Identity.RealName);

            this.WireEvents();
        }

        /// <summary>
        /// Called when [query message received].
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="IrcEventArgs"/> instance containing the event data.
        /// </param>
        private void OnQueryMessageReceived(object sender, IrcEventArgs e)
        {
            this.OnPrivateMessageReceived(new PrivateMessageReceivedEventArgs());
        }

        /// <summary>
        /// The on registered.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void OnRegistered(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Called when [un away].
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="IrcEventArgs"/> instance containing the event data.
        /// </param>
        private void OnUnAway(object sender, IrcEventArgs e)
        {

        }

        /// <summary>
        /// Wires the events.
        /// </summary>
        private void WireEvents()
        {
            // Registered
            this.ircClient.OnRegistered -= this.OnRegistered;
            this.ircClient.OnRegistered += this.OnRegistered;

            // Private Message
            this.ircClient.OnQueryMessage -= this.OnQueryMessageReceived;
            this.ircClient.OnQueryMessage += this.OnQueryMessageReceived;

            // Public Message
            this.ircClient.OnChannelMessage -= this.OnChannelMessageReceived;
            this.ircClient.OnChannelMessage += this.OnChannelMessageReceived;

            // Away
            this.ircClient.OnAway -= this.OnAway;
            this.ircClient.OnAway += this.OnAway;

            // Back
            this.ircClient.OnUnAway -= this.OnUnAway;
            this.ircClient.OnUnAway += this.OnUnAway;
        }

        #endregion
    }
}
