// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserJoinedEventArgs.cs" company="Patrick Magee">
//   Copyright � 2013 Patrick Magee
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
//   The user joined event args.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Modules.Events
{
    using System;
    using NazureBot.Modules.Messaging;

    public class UserJoinedEventArgs : EventArgs
    {
        public UserJoinedEventArgs(IUser user, IChannel channel, IServer server)
        {
            this.User = user;
            this.Channel = channel;
            this.Server = server;
        }

        public IChannel Channel { get; private set; }
        public IServer Server { get; private set; }
        public IUser User { get; private set; }
    }
}