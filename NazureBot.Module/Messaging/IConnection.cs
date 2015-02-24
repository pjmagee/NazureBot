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

namespace NazureBot.Modules.Messaging
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Threading.Tasks;

    using NazureBot.Modules.Events;
    using NazureBot.Modules.Messages;

    public interface IConnection : IPartImportsSatisfiedNotification
    {
        event EventHandler<ConnectionModulesChangedEventArgs> ModulesChanged;

        IChatClient Client { get; }

        [ImportMany(typeof(Module), AllowRecomposition = true, RequiredCreationPolicy = CreationPolicy.Any, Source = ImportSource.Any)]
        IEnumerable<Module> Modules { get; set; }

        INetwork Network { get; }
        IServer Server { get; }

        Task ConnectAsync(INetwork network);
        Task ConnectAsync(IServer server);
        Task SendResponseAsync(IResponse response);
    }
}