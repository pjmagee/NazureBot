// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModuleAttribute.cs" company="Patrick Magee">
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
//   The module attribute.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Modules
{
    #region Using directives

    using System;
    using System.ComponentModel;
    using System.ComponentModel.Composition;

    using NazureBot.Modules.Security;

    #endregion

    /// <summary>
    /// The module attribute.
    /// </summary>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ModuleAttribute : ExportAttribute
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleAttribute"/> class.
        /// </summary>
        public ModuleAttribute()
        {
            this.Name = this.ContractType.Name;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        /// <value>
        /// The author.
        /// </value>
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        [DefaultValue("Modules")]
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the level required.
        /// </summary>
        /// <value>
        /// The level required.
        /// </value>
        [DefaultValue(typeof(AccessLevel), "None")]
        public AccessLevel LevelRequired { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        [DefaultValue("0.0.0.1")]
        public string Version { get; set; }

        #endregion
    }
}