// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BotStatusChangedEventArgs.cs" company="Patrick Magee">
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
//   The bot status changed event args.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Core.Events
{
    using System;

    /// <summary>
    /// The bot status changed event args.
    /// </summary>
    public class BotStatusChangedEventArgs : EventArgs
    {
        private readonly BotStatus newBotStatus;
        private readonly BotStatus oldBotStatus;

        public BotStatusChangedEventArgs(BotStatus newBotStatus, BotStatus oldBotStatus)
        {
            this.oldBotStatus = oldBotStatus;
            this.newBotStatus = newBotStatus;
        }

        public BotStatus NewBotStatus
        {
            get { return this.newBotStatus; }
        }

        public BotStatus OldBotStatus
        {
            get { return this.oldBotStatus; }
        }
    }
}