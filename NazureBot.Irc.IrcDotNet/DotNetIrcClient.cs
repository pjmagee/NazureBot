// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DotNetIrcClient.cs" company="Patrick Magee">
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
//   The ircdotnet third party client library implementation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Irc.IrcDotNet
{
    #region Using directives

    using System;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using global::IrcDotNet;

    using global::IrcDotNet.Ctcp;

    using NazureBot.Core.Messaging;
    using NazureBot.Core.Services.User;
    using NazureBot.Modules.Events;
    using NazureBot.Modules.Messages;
    using NazureBot.Modules.Messaging;

    #endregion

    /// <summary>
    /// The ircdotnet third party client library implementation.
    /// </summary>
    public class DotNetIrcClient : AbstractClient
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
        /// The CTCP client
        /// </summary>
        private CtcpClient ctcpClient;

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
        /// Initializes a new instance of the <see cref="DotNetIrcClient"/> class.
        /// </summary>
        /// <param name="userService">
        /// The user service.
        /// </param>
        public DotNetIrcClient(IUserService userService) : this()
        {
            Contract.Requires<ArgumentNullException>(userService != null, "userService");

            this.userService = userService;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="DotNetIrcClient"/> class from being created.
        /// </summary>
        private DotNetIrcClient()
        {
            this.ircClient = new IrcClient();
            this.ctcpClient = new CtcpClient(this.ircClient);
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Occurs when [message received].
        /// </summary>
        public event EventHandler<PublicMessageReceivedEventArgs> MessageReceived;

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
                return Assembly.GetAssembly(typeof(IrcClient)).GetName().FullName;
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

        #region Properties

        /// <summary>
        /// Gets or sets the registration information.
        /// </summary>
        /// <value>
        /// The registration information.
        /// </value>
        private IrcUserRegistrationInfo RegistrationInfo { get; set; }

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
            await Task.Run(() =>
                 {
                     Contract.Requires<ArgumentNullException>(network != null, "userService");

                     this.network = network;
                     this.CreateRegistrationInfo();
                     this.server = network.Servers.First();
                     return this.Connect(this.server);
                 });
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
            await Task.Run(() =>
                    {
                        Contract.Requires<ArgumentNullException>(server != null, "server");

                        this.ircClient.Connect(server.Address, server.Port, server.Ssl, this.RegistrationInfo);
                        this.ircClient.Connected -= this.OnConnected;
                        this.ircClient.Connected += this.OnConnected;
                    });
        }

        /// <summary>
        /// Disconnects this instance.
        /// </summary>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        public override async Task Disconnect()
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
        public override async Task SendResponseAsync(IResponse response)
        {
            Contract.Requires<ArgumentNullException>(response != null, "response");

            await Task.Run(() => Console.WriteLine("sending message: {0}", response.Message));
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the registration information.
        /// </summary>
        private void CreateRegistrationInfo()
        {
            this.RegistrationInfo = new IrcUserRegistrationInfo 
            {
                NickName = this.network.Identity.NickName, 
                Password = this.network.Identity.Password, 
                RealName = this.network.Identity.RealName, 
                UserName = this.network.Identity.UserName
            };
        }

        /// <summary>
        /// Called when [connected].
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="EventArgs"/> instance containing the event data.
        /// </param>
        private void OnConnected(object sender, EventArgs e)
        {
            this.ircClient.LocalUser.MessageReceived += this.OnMessageReceived;
        }

        /// <summary>
        /// Called when [message received].
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="IrcMessageEventArgs"/> instance containing the event data.
        /// </param>
        private void OnMessageReceived(object sender, IrcMessageEventArgs e)
        {
            var user = sender as IrcUser;

            if (user != null)
            {
                this.userService.GetOrCreateByHostmaskAsync(user.HostName).ContinueWith(task =>
                    {
                        var eventArgs = new PrivateMessageReceivedEventArgs(task.Result, this.server, MessageFormat.Message, MessageBroadcast.Private, e.Text);
                        this.OnPrivateMessageReceived(eventArgs);
                    });
            }
        }

        #endregion
    }
}
