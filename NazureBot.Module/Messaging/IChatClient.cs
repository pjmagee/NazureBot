// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IChatClient.cs" company="Patrick Magee">
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
//   The IrcClient interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Modules.Messaging
{
    using System;
    using System.Threading.Tasks;

    using NazureBot.Modules.Events;
    using NazureBot.Modules.Messages;

    public interface IChatClient
    {
        event EventHandler<PrivateMessageReceivedEventArgs> PrivateMessageReceived;
        event EventHandler<PublicMessageReceivedEventArgs> PublicMessageReceived;
        event EventHandler<TopicChangedEventArgs> TopicChanged;
        event EventHandler<UserJoinedEventArgs> UserJoined;
        event EventHandler<UserKickedEventArgs> UserKicked;
        event EventHandler<UserQuitEventArgs> UserQuit;

        string Description { get; }
        bool IsConnected { get; }

        Task Connect(INetwork network);
        Task Connect(IServer server);
        Task Disconnect();
        Task SendResponseAsync(IResponse response);
    }
}
