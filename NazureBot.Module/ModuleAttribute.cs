// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModuleAttribute.cs" company="Patrick Magee">
//   Copyright � 2013 Patrick Magee
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
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Composition;

    using NazureBot.Modules.Security;

    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ModuleAttribute : ExportAttribute
    {
        public ModuleAttribute()
        {
            this.Name = this.ContractType.Name;
        }

        public string Author { get; set; }

        [DefaultValue("Modules")]
        public string Category { get; set; }

        public string Description { get; set; }

        [DefaultValue(typeof(AccessLevel), "None")]
        public AccessLevel LevelRequired { get; set; }

        public string Name { get; set; }

        [DefaultValue("0.0.0.1")]
        public string Version { get; set; }
    }
}