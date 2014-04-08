﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Response.cs" company="Patrick Magee">
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
//   The response.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Modules.Messages
{
    #region Using directives

    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// The response.
    /// </summary>
    public class Response : IResponse
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the broadcast.
        /// </summary>
        public MessageBroadcast Broadcast { get; set; }

        /// <summary>
        /// Gets or sets the format.
        /// </summary>
        public MessageFormat Format { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the request.
        /// </summary>
        public IRequest Request { get; set; }

        /// <summary>
        /// Gets or sets the targets.
        /// </summary>
        public IEnumerable<string> Targets { get; set; }

        #endregion
    }
}