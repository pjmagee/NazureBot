// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IServer.cs" company="Patrick Magee">
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
//   The Server interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Modules.Irc
{
    /// <summary>
    /// The Server interface.
    /// </summary>
    public interface IServer
    {
        #region Public Properties

        /// <summary>
        /// Gets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        string Address { get; }

        /// <summary>
        /// Gets the port.
        /// </summary>
        /// <value>
        /// The port.
        /// </value>
        int Port { get; }

        /// <summary>
        /// Gets a value indicating whether ssl.
        /// </summary>
        bool Ssl { get; }

        #endregion
    }
}