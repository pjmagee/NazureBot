// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WeatherModule.cs" company="Patrick Magee">
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
//   The weather module.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Module.Weather
{
    using System.Threading.Tasks;

    using NazureBot.Modules;
    using NazureBot.Modules.Commands;
    using NazureBot.Modules.Messages;
    using NazureBot.Modules.Security;

    [Module(Author = "Patrick Magee", Category = "Utils", Description = "Provides weather data", LevelRequired = AccessLevel.None, Version = "0.0.1", Name = "Weather")]
    public class WeatherModule : Module
    {
        public override void RegisterCommands(IRegistrationService registrationService)
        {
            registrationService.Register(new Command(AccessLevel.None, "!weather", "Weather data", "!weather [post code]", this.WeatherRequestAsync));
        }

        private async Task WeatherRequestAsync(IRequest request)
        {
            if (request.Message == "Hello")
            {
                IResponse response = request.CreateResponse();

                response.Message = "Hello";
                response.Targets = new[] { request.User.Host.Nick };
                response.Format = request.Format;
                response.Broadcast = request.Broadcast;

                await request.SendResponseAsync(response);
            }
        }
    }
}
