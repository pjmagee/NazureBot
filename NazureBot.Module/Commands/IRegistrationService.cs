// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRegistrationService.cs" company="Patrick Magee">
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
//   The RegistrationService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Modules.Commands
{
    #region Using directives

    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// The RegistrationService interface.
    /// </summary>
    public interface IRegistrationService
    {
        #region Public Properties

        /// <summary>
        /// Gets the registered commands.
        /// </summary>
        IEnumerable<ICommand> RegisteredCommands { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The clear.
        /// </summary>
        void Clear();

        /// <summary>
        /// The register.
        /// </summary>
        /// <param name="command">
        /// The command.
        /// </param>
        void Register(ICommand command);

        /// <summary>
        /// The register.
        /// </summary>
        /// <param name="commands">
        /// The commands.
        /// </param>
        void Register(IEnumerable<ICommand> commands);

        /// <summary>
        /// The register.
        /// </summary>
        /// <param name="commands">
        /// The commands.
        /// </param>
        void Register(params ICommand[] commands);

        #endregion
    }
}