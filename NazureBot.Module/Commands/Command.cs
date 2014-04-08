// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Command.cs" company="Patrick Magee">
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
//   The command.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Modules.Commands
{
    #region Using directives

    using System;
    using System.Threading.Tasks;

    using NazureBot.Modules.Messages;
    using NazureBot.Modules.Security;

    #endregion

    /// <summary>
    /// The command.
    /// </summary>
    public class Command : ICommand
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class.
        /// </summary>
        public Command()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class.
        /// </summary>
        /// <param name="requiredLevel">
        /// The required level.
        /// </param>
        /// <param name="trigger">
        /// The trigger.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <param name="usage">
        /// The usage.
        /// </param>
        /// <param name="handler">
        /// The handler.
        /// </param>
        public Command(AccessLevel requiredLevel, string trigger, string description, string usage, Func<IRequest, Task> handler)
        {
            this.RequiredLevel = requiredLevel;
            this.Trigger = trigger;
            this.Description = description;
            this.Usage = usage;
            this.Handler = handler;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the handler.
        /// </summary>
        /// <value>
        /// The handler.
        /// </value>
        public Func<IRequest, Task> Handler { get; set; }

        /// <summary>
        /// Gets the required level.
        /// </summary>
        /// <value>
        /// The required level.
        /// </value>
        public AccessLevel RequiredLevel { get; private set; }

        /// <summary>
        /// Gets or sets the trigger.
        /// </summary>
        /// <value>
        /// The trigger.
        /// </value>
        public string Trigger { get; set; }

        /// <summary>
        /// Gets the usage.
        /// </summary>
        /// <value>
        /// The usage.
        /// </value>
        public string Usage { get; private set; }

        #endregion
    }
}