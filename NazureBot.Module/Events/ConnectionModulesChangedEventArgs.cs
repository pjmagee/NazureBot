// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConnectionModulesChangedEventArgs.cs" company="Patrick Magee">
//   Copyright � 2013 Patrick Magee
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
//   The connection modules changed event args.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Modules.Events
{
    using System;
    using System.Collections.Generic;

    public class ConnectionModulesChangedEventArgs : EventArgs
    {
        public ConnectionModulesChangedEventArgs(IEnumerable<Module> modules)
        {
            this.Modules = modules;
        }

        private ConnectionModulesChangedEventArgs()
        {

        }

        public IEnumerable<Module> Modules { get; private set; }
    }
}