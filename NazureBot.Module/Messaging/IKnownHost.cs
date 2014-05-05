// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IKnownHost.cs" company="Patrick Magee">
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
//   The KnownHost interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Modules.Messaging
{
    /// <summary>
    /// The KnownHost interface.
    /// </summary>
    /// <remarks>
    /// The known host is a set of properties that makes up an individual users identity that can be
    /// recognised by the bot. Generally made up of three parts, but depends on the chat service. 
    /// In irc, a users ident can be found made up of their host name, an ident/username value and their given nickname.
    /// <example>
    ///     IRC:
    ///     The nick is the first part of the string split on !: nick
    ///     The username is the second part of the string: user.name 
    ///     The host is the last part of the string: address.com
    ///     ~nick!user.name@address.com
    /// </example>
    /// <example>
    ///     XMPP:
    ///     The nick/ident/host can be put together to give the full username of the users known host.
    /// </example> 
    /// </remarks>
    public interface IKnownHost
    {
        #region Public Properties

        /// <summary>
        /// Gets the host.
        /// </summary>
        /// <value>
        /// The host.
        /// </value>
        string Host { get; }

        /// <summary>
        /// Gets the ident.
        /// </summary>
        /// <value>
        /// The ident.
        /// </value>
        string Ident { get; }

        /// <summary>
        /// Gets the nick.
        /// </summary>
        /// <value>
        /// The nick.
        /// </value>
        string Nick { get; }

        #endregion
    }
}