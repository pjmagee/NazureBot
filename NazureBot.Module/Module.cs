// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Module.cs" company="Patrick Magee">
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
//   The module.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Modules
{
    using System.ComponentModel.Composition;

    using NazureBot.Modules.Commands;
    using NazureBot.Modules.Events;

    [InheritedExport(typeof(Module))]
    public abstract class Module : IModule
    {
        public virtual void OnChannelMessageReceived(object sender, PublicMessageReceivedEventArgs e)
        {
        }

        public virtual void OnQueryMessageReceived(object sender, PrivateMessageReceivedEventArgs e)
        {
        }

        public virtual void OnTopicChanged(object sender, TopicChangedEventArgs e)
        {
        }

        public virtual void OnUserJoined(object sender, UserJoinedEventArgs e)
        {
        }

        public virtual void OnUserKicked(object sender, UserKickedEventArgs e)
        {
        }

        public virtual void OnUserQuit(object sender, UserQuitEventArgs e)
        {
        }

        public abstract void RegisterCommands(IRegistrationService registrationService);
    }
}