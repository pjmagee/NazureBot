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
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Meebey.SmartIrc4net;

    using NazureBot.Core.Messaging;
    using NazureBot.Core.Services.User;
    using NazureBot.Modules.Events;
    using NazureBot.Modules.Messages;
    using NazureBot.Modules.Messaging;

    public class SmartIrcClient : AbstractClient
    {
        private readonly IrcClient ircClient;
        private readonly IUserService userService;
        private INetwork network;
        private IServer server;

        public SmartIrcClient(IUserService userService) : this()
        {
            this.userService = userService;
        }

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

        public override string Description
        {
            get
            {
                return typeof(IrcClient).Assembly.GetName().FullName;
            }
        }

        public override bool IsConnected
        {
            get
            {
                return this.ircClient != null && this.ircClient.IsConnected;
            }
        }

        public async override Task Connect(IServer server)
        {
            this.ircClient.Connect(server.Address, server.Port);
            this.ircClient.OnConnected -= this.OnConnected;
            this.ircClient.OnConnected += this.OnConnected;
        }

        public async override Task Connect(INetwork network)
        {
            this.network = network;
            this.server = network.Servers.First();
        }

        public async override Task Disconnect()
        {
            await Task.Run(() => this.ircClient.Disconnect());
        }

        public async override Task SendResponseAsync(IResponse response)
        {
            await Task.Run(() =>
            {
                var sendType = SendType.Message;

                switch (response.Format)
                {
                    case MessageFormat.Message:
                        sendType = SendType.Message; 
                        break;
                    case MessageFormat.Action:
                        sendType = SendType.Action; 
                        break;
                    case MessageFormat.Notice:
                        sendType = SendType.Notice; 
                        break;
                }

                foreach (var target in response.Targets)
                {
                    this.ircClient.SendMessage(sendType, target, response.Message);
                }
            });
        }

        private void OnAway(object sender, AwayEventArgs e)  { }

        private void OnChannelMessageReceived(object sender, IrcEventArgs e)
        {
            // this.OnPublicMessageReceived(new PublicMessageReceivedEventArgs());
        }

        private void OnConnected(object sender, EventArgs eventArgs)
        {
            this.ircClient.Login(this.network.Identity.NickName, this.network.Identity.RealName);

            this.WireEvents();
        }

        private void OnQueryMessageReceived(object sender, IrcEventArgs e)
        {
            this.OnPrivateMessageReceived(new PrivateMessageReceivedEventArgs());
        }

        private void OnRegistered(object sender, EventArgs e) { }

        private void OnUnAway(object sender, IrcEventArgs e) { }

        private void WireEvents()
        {
            this.ircClient.OnRegistered -= this.OnRegistered;
            this.ircClient.OnRegistered += this.OnRegistered;

            this.ircClient.OnQueryMessage -= this.OnQueryMessageReceived;
            this.ircClient.OnQueryMessage += this.OnQueryMessageReceived;

            this.ircClient.OnChannelMessage -= this.OnChannelMessageReceived;
            this.ircClient.OnChannelMessage += this.OnChannelMessageReceived;

            this.ircClient.OnAway -= this.OnAway;
            this.ircClient.OnAway += this.OnAway;

            this.ircClient.OnUnAway -= this.OnUnAway;
            this.ircClient.OnUnAway += this.OnUnAway;
        }
    }
}
