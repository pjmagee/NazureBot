// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserService.cs" company="Patrick Magee">
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
//   The user service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Core.Services.User
{
    #region Using directives

    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Runtime.Caching;
    using System.Threading.Tasks;

    using NazureBot.Core.Infrastructure.EF;
    using NazureBot.Core.Infrastructure.Entities;
    using NazureBot.Core.Services.Host;
    using NazureBot.Modules.Irc;

    using Ninject;

    #endregion

    /// <summary>
    ///     The user service.
    /// </summary>
    public sealed class UserService : IUserService
    {
        #region Fields

        /// <summary>
        /// The cache
        /// </summary>
        private readonly ObjectCache cache;

        /// <summary>
        /// The database context
        /// </summary>
        private readonly DatabaseContext databaseContext;

        /// <summary>
        /// The host matcher
        /// </summary>
        private readonly IHostMatcher hostMatcher;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="databaseContext">
        /// The database context.
        /// </param>
        /// <param name="hostMatcher">
        /// The host matcher.
        /// </param>
        [Inject]
        public UserService(DatabaseContext databaseContext, IHostMatcher hostMatcher)
            : this()
        {
            Contract.Requires<ArgumentNullException>(databaseContext != null, "databaseContext");
            Contract.Requires<ArgumentNullException>(hostMatcher != null, "hostMatcher");

            this.hostMatcher = hostMatcher;
            this.databaseContext = databaseContext;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="UserService" /> class from being created.
        /// </summary>
        private UserService()
        {
            this.cache = MemoryCache.Default;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Gets the or create by hostmask asynchronous.
        /// </summary>
        /// <param name="hostmask">
        /// The hostmask.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<IUser> GetOrCreateByHostmaskAsync(string hostmask)
        {
            Contract.Requires<ArgumentNullException>(hostmask != null, "hostmask");

            Task<User> user =
                this.databaseContext.Users.FirstOrDefaultAsync(
                    u => u.KnownHosts.Any(kh => this.hostMatcher.IsMatch(kh, hostmask)));

            if (user == null)
            {
                var guestUser = new User(hostmask);
                var item = new CacheItem(hostmask, guestUser);

                this.cache.Add(item, 
                    new CacheItemPolicy
                        {
                            SlidingExpiration = TimeSpan.FromMinutes(30), 
                            Priority = CacheItemPriority.Default
                        });
            }

            return this.cache[hostmask] as IUser;
        }

        /// <summary>
        /// Gets the users asynchronous.
        /// </summary>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        public async Task<IEnumerable<IUser>> GetUsersAsync()
        {
            return await this.databaseContext.Users.ToListAsync();
        }

        #endregion
    }
}