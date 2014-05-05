// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTest1.cs" company="Patrick Magee">
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
//   The unit test 1.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Core.Tests
{
    #region Using directives

    using System.Security.Principal;
    using System.Threading;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NazureBot.Core.Infrastructure.Entities;
    using NazureBot.Modules.Security;

    #endregion

    /// <summary>
    /// The unit test 1.
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        #region Public Methods and Operators

        /// <summary>
        /// The test method 1.
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            var user = new User("Peej!patrick.magee@live.co.uk@192.168.0.10") { Description = "Adminstrator", AccessLevel = AccessLevel.Admin };

            GenericIdentity identity = new GenericIdentity(user.Host.Nick);
            GenericPrincipal principal = new GenericPrincipal(identity, new[] { user.AccessLevel.ToString() });
            Thread.CurrentPrincipal = principal;
        }

        #endregion
    }
}
