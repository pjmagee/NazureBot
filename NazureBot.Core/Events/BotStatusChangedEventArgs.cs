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
    #region Using directives

    using System;

    #endregion

    /// <summary>
    /// The bot status changed event args.
    /// </summary>
    public class BotStatusChangedEventArgs : EventArgs
    {
        #region Fields

        /// <summary>
        /// The new bot status
        /// </summary>
        private readonly BotStatus newBotStatus;

        /// <summary>
        /// The old bot status
        /// </summary>
        private readonly BotStatus oldBotStatus;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BotStatusChangedEventArgs"/> class.
        /// </summary>
        /// <param name="newBotStatus">
        /// The new bot status.
        /// </param>
        /// <param name="oldBotStatus">
        /// The old bot status.
        /// </param>
        public BotStatusChangedEventArgs(BotStatus newBotStatus, BotStatus oldBotStatus)
        {
            this.oldBotStatus = oldBotStatus;
            this.newBotStatus = newBotStatus;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the new bot status.
        /// </summary>
        /// <value>
        /// The new bot status.
        /// </value>
        public BotStatus NewBotStatus
        {
            get
            {
                return this.newBotStatus;
            }
        }

        /// <summary>
        /// Gets the old bot status.
        /// </summary>
        /// <value>
        /// The old bot status.
        /// </value>
        public BotStatus OldBotStatus
        {
            get
            {
                return this.oldBotStatus;
            }
        }

        #endregion
    }
}