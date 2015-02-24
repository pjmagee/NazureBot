// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegistrationService.cs" company="Patrick Magee">
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
//   The command registration service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Core.Services.Module
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    using NazureBot.Modules.Commands;

    /// <summary>
    ///     The command registration service.
    /// </summary>
    public class RegistrationService : IRegistrationService
    {
        private readonly List<ICommand> commands;

        public RegistrationService()
        {
            this.commands = new List<ICommand>();
        }

        public IEnumerable<ICommand> RegisteredCommands
        {
            get { return this.commands; }
        }

        public void Clear()
        {
            this.commands.Clear();
        }

        public void Register(ICommand command)
        {
            Contract.Requires<ArgumentNullException>(command != null, "command");

            this.commands.Add(command);
        }

        public void Register(IEnumerable<ICommand> commands)
        {
            this.commands.AddRange(commands);
        }

        public void Register(params ICommand[] commands)
        {
            this.Register(commands.AsEnumerable());
        }
    }
}