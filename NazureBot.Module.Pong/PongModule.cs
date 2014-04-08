// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PongModule.cs" company="Patrick Magee">
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
//   The pong module.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Module.Pong
{
    #region Using directives

    using System.Threading.Tasks;

    using NazureBot.Modules;
    using NazureBot.Modules.Commands;
    using NazureBot.Modules.Events;
    using NazureBot.Modules.Messages;
    using NazureBot.Modules.Security;

    #endregion

    /// <summary>
    /// The pong module.
    /// </summary>
    [Module(Author = "Patrick Magee", Category = "Utils", Description = "Replys to a ping", Version = "0.1", Name = "Pong", LevelRequired = AccessLevel.None)]
    public class PongModule : Module
    {
        #region Public Methods and Operators

        /// <summary>
        /// The on user joined.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        public override async void OnUserJoined(object sender, UserJoinedEventArgs e)
        {
            var request = sender as IRequest;
            IResponse response = request.CreateResponse();
            await request.SendResponseAsync(response);
        }

        /// <summary>
        /// The register commands.
        /// </summary>
        /// <param name="registrationService">
        /// The registration service.
        /// </param>
        public override void RegisterCommands(IRegistrationService registrationService)
        {
            Command command = new Command(AccessLevel.None, "!ping", string.Empty, string.Empty, this.Handler);
            command.Handler = this.Handler;
            command.Trigger = "ping";
            command.Description = "Returns a pong from a ping";

            registrationService.Register(command);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The handler.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        private async Task Handler(IRequest request)
        {
            IResponse response = request.CreateResponse();

            if (request.Message.Equals("ping"))
            {
                response.Broadcast = MessageBroadcast.Public;
                response.Format = MessageFormat.Message;
                response.Message = "pong";

                if (request.Broadcast == MessageBroadcast.Public)
                {
                    response.Targets = new[] { request.Channel.Name };
                }

                if (request.Broadcast == MessageBroadcast.Private)
                {
                    response.Targets = new[] { request.User.Host.Nick };
                }
                
                await request.SendResponseAsync(response);
            }
        }

        #endregion
    }
}
