// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DatabaseContext.cs" company="Patrick Magee">
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
//   The database context.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Core.Infrastructure.EF
{
    #region Using directives

    using System.Data.Entity;

    using NazureBot.Core.Infrastructure.Entities;

    #endregion

    /// <summary>
    /// The database context.
    /// </summary>
    public class DatabaseContext : DbContext
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseContext" /> class.
        /// </summary>
        public DatabaseContext() : base("Default")
        {
            this.Configuration.AutoDetectChangesEnabled = true;
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.ValidateOnSaveEnabled = true;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the channels.
        /// </summary>
        /// <value>
        /// The channels.
        /// </value>
        public DbSet<Channel> Channels { get; set; }

        /// <summary>
        /// Gets or sets the identities.
        /// </summary>
        /// <value>
        /// The identities.
        /// </value>
        public DbSet<Identity> Identities { get; set; }

        /// <summary>
        /// Gets or sets the known hosts.
        /// </summary>
        /// <value>
        /// The known hosts.
        /// </value>
        public DbSet<KnownHost> KnownHosts { get; set; }

        /// <summary>
        /// Gets or sets the networks.
        /// </summary>
        /// <value>
        /// The networks.
        /// </value>
        public DbSet<Network> Networks { get; set; }

        /// <summary>
        /// Gets or sets the servers.
        /// </summary>
        /// <value>
        /// The servers.
        /// </value>
        public DbSet<Server> Servers { get; set; }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public DbSet<User> Users { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">
        /// The builder that defines the model for the context being created.
        /// </param>
        /// <remarks>
        /// Typically, this method is called only once when the first instance of a derived context
        /// is created.  The model for that context is then cached and is for all further instances of
        /// the context in the app domain.  This caching can be disabled by setting the ModelCaching
        /// property on the given ModelBuidler, but note that this can seriously degrade performance.
        /// More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        /// classes directly.
        /// </remarks>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}
