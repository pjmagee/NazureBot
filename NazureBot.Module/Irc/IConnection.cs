// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IConnection.cs" company="Patrick Magee">
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
//   The Connection interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Modules.Irc
{
    #region Using directives

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Threading.Tasks;

    using NazureBot.Modules.Events;
    using NazureBot.Modules.Messages;

    #endregion

    /// <summary>
    /// The Connection interface.
    /// </summary>
    public interface IConnection : IPartImportsSatisfiedNotification
    {
        #region Public Events

        /// <summary>
        /// Occurs when [modules changed].
        /// </summary>
        event EventHandler<ConnectionModulesChangedEventArgs> ModulesChanged;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the client.
        /// </summary>
        /// <value>
        /// The client.
        /// </value>
        IIrcClient Client { get; }

        /// <summary>
        /// Gets or sets the modules.
        /// </summary>
        /// <value>
        /// The modules.
        /// </value>
        [ImportMany(typeof(Module), AllowRecomposition = true, RequiredCreationPolicy = CreationPolicy.Any, Source = ImportSource.Any)]
        IEnumerable<Module> Modules { get; set; }

        /// <summary>
        /// Gets the network.
        /// </summary>
        /// <value>
        /// The network.
        /// </value>
        INetwork Network { get; }

        /// <summary>
        /// Gets the server.
        /// </summary>
        /// <value>
        /// The server.
        /// </value>
        IServer Server { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Connects the asynchronous.
        /// </summary>
        /// <param name="network">
        /// The network.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task ConnectAsync(INetwork network);

        /// <summary>
        /// Connects the asynchronous.
        /// </summary>
        /// <param name="server">
        /// The server.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task ConnectAsync(IServer server);

        /// <summary>
        /// Sends the response.
        /// </summary>
        /// <param name="response">
        /// The response.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task SendResponseAsync(IResponse response);

        #endregion
    }
}