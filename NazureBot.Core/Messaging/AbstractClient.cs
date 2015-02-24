// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AbstractClient.cs" company="Patrick Magee">
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
//   The abstract irc client.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Core.Messaging
{
    using System;
    using System.Threading.Tasks;

    using NazureBot.Modules.Events;
    using NazureBot.Modules.Messages;
    using NazureBot.Modules.Messaging;

    public abstract class AbstractClient : IChatClient
    {
        public event EventHandler<PrivateMessageReceivedEventArgs> PrivateMessageReceived;
        public event EventHandler<PublicMessageReceivedEventArgs> PublicMessageReceived;
        public event EventHandler<TopicChangedEventArgs> TopicChanged;
        public event EventHandler<UserJoinedEventArgs> UserJoined;
        public event EventHandler<UserKickedEventArgs> UserKicked;
        public event EventHandler<UserQuitEventArgs> UserQuit;

        public abstract string Description { get; }
        public abstract bool IsConnected { get; }

        public abstract Task Connect(INetwork network);
        public abstract Task Connect(IServer server);
        public abstract Task Disconnect();
        public abstract Task SendResponseAsync(IResponse response);

        protected virtual void OnPrivateMessageReceived(PrivateMessageReceivedEventArgs e)
        {
            var handler = this.PrivateMessageReceived;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnPublicMessageReceived(PublicMessageReceivedEventArgs e)
        {
            var handler = this.PublicMessageReceived;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnTopicChanged(TopicChangedEventArgs e)
        {
            var handler = this.TopicChanged;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnUserJoined(UserJoinedEventArgs e)
        {
            var handler = this.UserJoined;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnUserKicked(UserKickedEventArgs e)
        {
            var handler = this.UserKicked;

            if (handler != null)
            {
                handler(this, e);
            }
        }
        
        protected virtual void OnUserQuit(UserQuitEventArgs e)
        {
            var handler = this.UserQuit;

            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}