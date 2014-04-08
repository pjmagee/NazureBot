// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICommand.cs" company="Patrick Magee">
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
//   The Command interface.
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
    /// The Command interface.
    /// </summary>
    public interface ICommand
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Gets the handler.
        /// </summary>
        Func<IRequest, Task> Handler { get; }

        /// <summary>
        /// Gets the required level.
        /// </summary>
        AccessLevel RequiredLevel { get; }

        /// <summary>
        /// Gets the trigger.
        /// </summary>
        string Trigger { get; }

        /// <summary>
        /// Gets the usage.
        /// </summary>
        string Usage { get; }

        #endregion
    }
}