// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IIdentity.cs" company="Patrick Magee">
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
//   The Identity interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Modules.Messaging
{
    using System.Collections.Generic;

    /// <summary>
    /// The Identity interface.
    /// </summary>
    /// <remarks>
    /// Used as a way for the bot to give an identity when registering with external messaging services. 
    /// This enables users to easily identify the bot.
    /// </remarks>
    public interface IIdentity
    {
        string Description { get; }        
        string NickName { get; }
        string Password { get; }
        string RealName { get; }
        string UserName { get; }
        IEnumerable<INetwork> Networks { get; }
    }
}