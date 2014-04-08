// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHostMatcher.cs" company="Patrick Magee">
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
//   The ConnectionService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Core.Services.Host
{
    #region Using directives

    using NazureBot.Core.Infrastructure.Entities;

    #endregion

    /// <summary>
    /// The ConnectionService interface.
    /// </summary>
    public interface IConnectionService
    {
    }

    /// <summary>
    ///     The HostMatcher interface.
    /// </summary>
    public interface IHostMatcher
    {
        #region Public Methods and Operators

        /// <summary>
        /// Determines whether the specified known host is match.
        /// </summary>
        /// <param name="knownHost">
        /// The known host.
        /// </param>
        /// <param name="hostmask">
        /// The hostmask.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool IsMatch(KnownHost knownHost, string hostmask);

        #endregion
    }
}