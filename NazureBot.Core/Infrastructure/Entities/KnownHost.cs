// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KnownHost.cs" company="Patrick Magee">
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
//   The known host.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Core.Infrastructure.Entities
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    using NazureBot.Modules.Messaging;

    public class KnownHost : IKnownHost, IEquatable<string>
    {
        public Guid Id { get; set; }

        public string Host { get; private set; }
        public string HostMask { get; set; }
        public string Ident { get; private set; }
        public string Nick { get; private set; }

        public virtual User User { get; set; }
        public Guid? UserId { get; set; }

        public KnownHost()
        {
            this.Id = Guid.NewGuid();
        }

        public KnownHost(string hostmask) : this()
        {
            Contract.Requires<ArgumentNullException>(hostmask != null, "hostmask");

            this.HostMask = hostmask;
            this.TryParseIrcSegments();
        }

        /// <summary>
        /// Tries to parse the hostmask as an IRC hostmask.1
        /// </summary>
        private void TryParseIrcSegments()
        {
            try
            {
                Nick = this.HostMask.Split('!')[0];
            }
            catch (Exception)
            {
                Trace.TraceInformation("Failed to grab nick segment from {0}", HostMask);
            }

            try
            {
                this.Ident = this.HostMask.Split('!', '@')[1];
            }
            catch (Exception)
            {
                Trace.TraceInformation("Failed to grab ident segment from {0}", HostMask);
            }

            try
            {
                this.Host = HostMask.Split('!', '@')[2];
            }
            catch (Exception)
            {
                Trace.TraceInformation("Failed to grab host segment from {0}", HostMask);
            }
        }

        public KnownHost(string hostmask, User user) : this(hostmask)
        {
            Contract.Requires<ArgumentNullException>(user != null, "user");

            this.User = user;
        }

        public bool Equals(string hostmask)
        {
            return this.HostMask.Equals(hostmask, StringComparison.OrdinalIgnoreCase);
        }

        public override string ToString()
        {
            return this.HostMask;
        }
    }
}