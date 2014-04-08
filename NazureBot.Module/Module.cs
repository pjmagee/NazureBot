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
    #region Using directives

    using System.ComponentModel.Composition;

    using NazureBot.Modules.Commands;
    using NazureBot.Modules.Events;
    using NazureBot.Modules.Irc;

    #endregion

    /// <summary>
    /// The module.
    /// </summary>
    [InheritedExport(typeof(Module))]
    public abstract class Module : IModule
    {
        #region Public Methods and Operators

        /// <summary>
        /// Called when [channel message received].
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="ChannelMessageReceivedEventArgs"/> instance containing the event data.
        /// </param>
        public virtual void OnChannelMessageReceived(object sender, ChannelMessageReceivedEventArgs e)
        {
        }

        /// <summary>
        /// Called when [query message received].
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="QueryMessageReceivedEventArgs"/> instance containing the event data.
        /// </param>
        public virtual void OnQueryMessageReceived(object sender, QueryMessageReceivedEventArgs e)
        {
        }

        /// <summary>
        /// Called when [topic changed].
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="TopicChangedEventArgs"/> instance containing the event data.
        /// </param>
        public virtual void OnTopicChanged(object sender, TopicChangedEventArgs e)
        {
        }

        /// <summary>
        /// Called when [user joined].
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="UserJoinedEventArgs"/> instance containing the event data.
        /// </param>
        public virtual void OnUserJoined(object sender, UserJoinedEventArgs e)
        {
        }

        /// <summary>
        /// Called when [user kicked].
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="UserKickedEventArgs"/> instance containing the event data.
        /// </param>
        public virtual void OnUserKicked(object sender, UserKickedEventArgs e)
        {
        }

        /// <summary>
        /// Called when [user quit].
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="UserQuitEventArgs"/> instance containing the event data.
        /// </param>
        public virtual void OnUserQuit(object sender, UserQuitEventArgs e)
        {
        }

        /// <summary>
        /// Registers the commands.
        /// </summary>
        /// <param name="registrationService">
        /// The command registration service.
        /// </param>
        public abstract void RegisterCommands(IRegistrationService registrationService);

        #endregion
    }
}