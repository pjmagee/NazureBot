// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PrivateMessageReceivedEventArgs.cs" company="Patrick Magee">
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
//   The query message received event args.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Modules.Events
{
    using System;

    using NazureBot.Modules.Messages;
    using NazureBot.Modules.Messaging;

    public class PrivateMessageReceivedEventArgs : EventArgs
    {
        public PrivateMessageReceivedEventArgs()
        {
            
        }

        public PrivateMessageReceivedEventArgs(IUser user, IServer server, MessageFormat format, MessageBroadcast broadcast, string message)
        {
            this.User = user;
            this.Server = server;
            this.Format = format;
            this.Broadcast = broadcast;
            this.Message = message;
        }

        public MessageBroadcast Broadcast { get; private set; }
        public MessageFormat Format { get; private set; }
        public string Message { get; private set; }
        public IServer Server { get; private set; }
        public IUser User { get; private set; }
    }
}