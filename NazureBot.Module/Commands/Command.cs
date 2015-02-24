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
    using System;
    using System.Threading.Tasks;

    using NazureBot.Modules.Messages;
    using NazureBot.Modules.Security;

    public class Command : ICommand
    {
        public Command() { }

        public Command(AccessLevel requiredLevel, string trigger, string description, string usage, Func<IRequest, Task> handler)
        {
            this.RequiredLevel = requiredLevel;
            this.Trigger = trigger;
            this.Description = description;
            this.Usage = usage;
            this.Handler = handler;
        }

        public string Description { get; set; }
        public Func<IRequest, Task> Handler { get; set; }
        public AccessLevel RequiredLevel { get; private set; }
        public string Trigger { get; set; }
        public string Usage { get; private set; }
    }
}