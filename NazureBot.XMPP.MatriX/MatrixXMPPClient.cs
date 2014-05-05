// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MatrixXMPPClient.cs" company="Patrick Magee">
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
//   Defines the MatrixXMPPClient type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.XMPP.MatriX
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using Matrix;
    using Matrix.Xmpp;
    using Matrix.Xmpp.Client;
    using Matrix.Xmpp.Register;
    using Matrix.Xmpp.Roster;

    using NazureBot.Core.Infrastructure.Entities;
    using NazureBot.Core.Messaging;
    using NazureBot.Core.Services.User;
    using NazureBot.Modules.Events;
    using NazureBot.Modules.Messages;
    using NazureBot.Modules.Messaging;

    using EventArgs = Matrix.EventArgs;

    /// <summary>
    /// The matrix xmpp client.
    /// </summary>
    public class MatrixXMPPClient : AbstractClient
    {
        /// <summary>
        /// Gets or sets the xmpp client.
        /// </summary>
        private XmppClient XmppClient { get; set; }

        /// <summary>
        /// Gets or sets the server.
        /// </summary>
        private IServer Server { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        private string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        private string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether connected.
        /// </summary>
        private bool Connected { get; set; }

        /// <summary>
        /// The user service
        /// </summary>
        private readonly IUserService userService;

        public override string Description
        {
            get
            {
                return Assembly.GetAssembly(typeof(XmppClient)).GetName().FullName;
            }
        }

        public override bool IsConnected
        {
            get
            {
                return this.XmppClient != null && this.Connected;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixXMPPClient"/> class.
        /// </summary>
        /// <param name="userService">
        /// The user service.
        /// </param>
        public MatrixXMPPClient(IUserService userService) : this()
        {
            Contract.Requires<ArgumentNullException>(userService != null, "userService");
            this.userService = userService;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="MatrixXMPPClient"/> class from being created.
        /// </summary>
        private MatrixXMPPClient()
        {
            
        }

        public override async Task Connect(INetwork network)
        {
            this.Username = network.Identity.UserName;
            this.Password = network.Identity.Password;
            this.Connect(network.Servers.First());
        }

        public override async Task Connect(IServer server)
        {
            this.XmppClient.SetUsername(this.Username);
            this.XmppClient.Password = this.Password;
            this.XmppClient.SetXmppDomain(server.Address);
            this.XmppClient.Port = server.Port;
            this.XmppClient.Show = Show.chat;
            this.XmppClient.AutoRoster = true;

            WireEvents();
            
            this.XmppClient.Open();
        }

        private void WireEvents()
        {
            this.WireRegisterEvents();
            this.WireRosterEvents();
            this.WireConnectionEvents();

            this.XmppClient.OnMessage += OnMessage;
        }

        private void WireConnectionEvents()
        {
            this.XmppClient.OnLogin += this.OnLogin;
            this.XmppClient.OnClose += this.OnClose;
            this.XmppClient.OnError += this.OnError;
        }

        private void WireRegisterEvents()
        {
            this.XmppClient.OnRegister += this.OnRegister;
            this.XmppClient.OnRegisterError += this.OnRegisterError;
            this.XmppClient.OnRegisterInformation += this.OnRegisterInformation;
        }

        private void WireRosterEvents()
        {
            this.XmppClient.OnRosterStart += this.OnRosterStart;
            this.XmppClient.OnRosterItem += this.OnRosterItem;
            this.XmppClient.OnRosterEnd += this.OnRosterEnd;
        }

        private void OnError(object sender, ExceptionEventArgs args)
        {
            
        }

        private void OnClose(object sender, EventArgs args)
        {
            this.Connected = false;
        }

        private void OnLogin(object sender, EventArgs args)
        {
            this.Connected = true;
        }

        private void OnMessage(object sender, MessageEventArgs args)
        {
            if (args.Message.Type == MessageType.groupchat)
            {
                this.userService.GetOrCreateByHostmaskAsync(args.Message.From.Bare) // USER ?
                    .ContinueWith(task =>
                            {
                                var user = task.Result;
                                var channel = new Channel() { Name = args.Message.From.ToString() } as IChannel; // CHANNEL ?
                                var message = args.Message.Body;
                                var eventArgs = new PublicMessageReceivedEventArgs(user, Server, channel, MessageFormat.Message, MessageBroadcast.Public, message);
                                this.OnPublicMessageReceived(eventArgs);
                            });
            }
            else if (args.Message.Type == MessageType.chat)
            {
                this.userService.GetOrCreateByHostmaskAsync(args.Message.From.Bare)
                    .ContinueWith(task =>
                    {
                        var user = task.Result;
                        var message = args.Message.Body;
                        var eventArgs = new PrivateMessageReceivedEventArgs(user, Server, MessageFormat.Message, MessageBroadcast.Public, message);
                        this.OnPrivateMessageReceived(eventArgs);
                    });
            }
        }

        #region Roster

        private void OnRosterEnd(object sender, EventArgs eventArgs)
        {
            
        }

        private void OnRosterItem(object sender, RosterEventArgs rosterEventArgs)
        {
            // Roster contact
        }

        private void OnRosterStart(object sender, EventArgs eventArgs)
        {
            // Roster contact list begin
        }

        #endregion

        #region Registration

        private void OnRegisterError(object sender, IqEventArgs iqEventArgs)
        {
            
        }

        private void OnRegister(object sender, EventArgs eventArgs)
        {
            
        }

        private void OnRegisterInformation(object sender, RegisterEventArgs registerEventArgs)
        {

        }

        #endregion

        public override async Task Disconnect()
        {
            this.XmppClient.SendUnavailablePresence("Bot disconnected.");
            this.XmppClient.Close();
        }

        public override async Task SendResponseAsync(IResponse response)
        {
            if (response.Broadcast == MessageBroadcast.Private)
            {
                var message = new Message { Type = MessageType.chat, To = response.Request.User.ToString(), Body = response.Message };
                this.XmppClient.Send(message);
            }
            else if (response.Broadcast == MessageBroadcast.Public)
            {
                var message = new Message { Type = MessageType.groupchat, To = response.Request.Channel.ToString(), Body = response.Message };
                this.XmppClient.Send(message);
            }
        }
    }
}
