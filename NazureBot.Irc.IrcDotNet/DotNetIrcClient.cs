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

    /// <summary>
    /// The ircdotnet third party client library implementation.
    /// </summary>
    public class DotNetIrcClient : AbstractClient
    {
        private readonly IrcClient ircClient;
        private readonly IUserService userService;

        private CtcpClient ctcpClient;
        private INetwork network;
        private IServer server;

        public DotNetIrcClient(IUserService userService) : this()
        {
            Contract.Requires<ArgumentNullException>(userService != null, "userService");

            this.userService = userService;
        }

        private DotNetIrcClient()
        {
            this.ircClient = new IrcClient();
            this.ctcpClient = new CtcpClient(this.ircClient);
        }

        public event EventHandler<PublicMessageReceivedEventArgs> MessageReceived;

        public override string Description
        {
            get
            {
                return Assembly.GetAssembly(typeof(IrcClient)).GetName().FullName;
            }
        }

        public override bool IsConnected
        {
            get
            {
                return this.ircClient != null && this.ircClient.IsConnected;
            }
        }

        private IrcUserRegistrationInfo RegistrationInfo { get; set; }

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

        public override async Task Disconnect()
        {
            await Task.Run(() => this.ircClient.Disconnect());
        }

        public override async Task SendResponseAsync(IResponse response)
        {
            Contract.Requires<ArgumentNullException>(response != null, "response");

            await Task.Run(() => Console.WriteLine("sending message: {0}", response.Message));
        }

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

        private void OnConnected(object sender, EventArgs e)
        {
            this.ircClient.LocalUser.MessageReceived += this.OnMessageReceived;
        }

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
    }
}
