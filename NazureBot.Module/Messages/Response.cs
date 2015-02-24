// --------------------------------------------------------------------------------------------------------------------
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
    using System.Collections.Generic;

    public class Response : IResponse
    {
        public MessageBroadcast Broadcast { get; set; }
        public MessageFormat Format { get; set; }
        public string Message { get; set; }
        public IRequest Request { get; set; }
        public IEnumerable<string> Targets { get; set; }
    }
}