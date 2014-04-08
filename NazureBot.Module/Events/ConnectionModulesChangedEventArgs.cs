// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConnectionModulesChangedEventArgs.cs" company="Patrick Magee">
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
//   The connection modules changed event args.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Modules.Events
{
    #region Using directives

    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// The connection modules changed event args.
    /// </summary>
    public class ConnectionModulesChangedEventArgs : EventArgs
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionModulesChangedEventArgs"/> class.
        /// </summary>
        /// <param name="modules">
        /// The modules.
        /// </param>
        public ConnectionModulesChangedEventArgs(IEnumerable<Module> modules)
        {
            this.Modules = modules;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="ConnectionModulesChangedEventArgs"/> class from being created.
        /// </summary>
        private ConnectionModulesChangedEventArgs()
        {

        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the modules.
        /// </summary>
        /// <value>
        /// The modules.
        /// </value>
        public IEnumerable<Module> Modules { get; private set; }

        #endregion
    }
}