// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBot.cs" company="Patrick Magee">
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
//   The Bot interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Core
{
    #region Using directives

    using System;

    using NazureBot.Core.Events;

    #endregion

    /// <summary>
    /// The Bot interface.
    /// </summary>
    public interface IBot
    {
        #region Public Events

        /// <summary>
        /// Occurs when [status changed].
        /// </summary>
        event EventHandler<BotStatusChangedEventArgs> StatusChanged;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        BotStatus Status { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Starts this instance. Called during activation.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops this instance. Called during deactivation.
        /// </summary>
        void Stop();

        #endregion
    }
}