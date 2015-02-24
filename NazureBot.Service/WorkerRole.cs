// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorkerRole.cs" company="Patrick Magee">
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
//   The worker role.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NazureBot.Service
{
    using System;
    using System.Diagnostics;
    using System.Net;
    using System.Threading;

    using Microsoft.ServiceBus;
    using Microsoft.ServiceBus.Messaging;
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.ServiceRuntime;

    using NazureBot.Core;
    using NazureBot.Core.Factories;
    using NazureBot.Core.Infrastructure.EF;
    using NazureBot.Core.Infrastructure.Entities;
    using NazureBot.Core.Messaging;
    using NazureBot.Core.Services.Module;
    using NazureBot.Core.Services.Network;
    using NazureBot.Core.Services.User;
    using NazureBot.Irc.IrcDotNet;
    using NazureBot.Modules.Commands;
    using NazureBot.Modules.Messaging;

    using Ninject;
    using Ninject.Extensions.Azure;
    using Ninject.Extensions.Factory;

    public class WorkerRole : NinjectRoleEntryPoint
    {
        private const string QueueName = "ProcessingQueue";

        private readonly ManualResetEvent CompletedEvent = new ManualResetEvent(false);
        private IBot bot;
        private QueueClient client;
        
        [Inject]
        public void CreateBot(IBot bot)
        {
            this.bot = bot;
        }

        public override void Run()
        {
            Trace.WriteLine("Starting processing of messages");

            this.client.OnMessage(receivedMessage =>
            {
                try
                {
                    Trace.WriteLine("Processing Service Bus message: " + receivedMessage.SequenceNumber.ToString());

                    if (receivedMessage.Properties.ContainsKey("start"))
                    {
                        this.bot.Start();
                    }

                    if (receivedMessage.Properties.ContainsKey("stop"))
                    {
                        this.bot.Stop();
                    }
                }
                catch(Exception e)
                {
                    Trace.TraceError(e.Message);
                }
            });

            this.CompletedEvent.WaitOne();
        }

        protected override IKernel CreateKernel()
        {
            var kernal = new StandardKernel();
            RegisterServices(kernal);
            return kernal;
        }

        protected override bool OnRoleStarted()
        {
            ServicePointManager.DefaultConnectionLimit = 12;

            string connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");

            var namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);

            if (!namespaceManager.QueueExists(QueueName))
            {
                namespaceManager.CreateQueue(QueueName);
            }

            this.client = QueueClient.CreateFromConnectionString(connectionString, QueueName);

            return base.OnRoleStarted();
        }

        protected override void OnRoleStopped()
        {
            this.client.Close();
            this.CompletedEvent.Set();
            base.OnRoleStopped();
        }

        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IBot>().To<Bot>().InSingletonScope();

            if (RoleEnvironment.IsEmulated)
            {
                kernel.Bind<INetworkService>().To<FakeNetworkService>().InThreadScope();
                kernel.Bind<IUserService>().To<FakeIrcUserService>().InThreadScope();
                kernel.Bind<IChatClient>().To<FakeIrcClient>().InTransientScope();
            }
            else
            {
                kernel.Bind<INetworkService>().To<NetworkService>().InThreadScope();
                kernel.Bind<IUserService>().To<UserService>().InThreadScope();
                kernel.Bind<IChatClient>().To<DotNetIrcClient>().InTransientScope();
            }

            kernel.Bind<ICommand>().To<Command>().InTransientScope();
            kernel.Bind<IConnection>().To<Connection>().InTransientScope();
            kernel.Bind<IRegistrationService>().To<RegistrationService>().InTransientScope();

            kernel.Bind<IResponseFactory>().ToFactory();
            kernel.Bind<IRequestFactory>().ToFactory();
            kernel.Bind<IIrcClientFactory>().ToFactory();
            kernel.Bind<IConnectionFactory>().ToFactory();

            kernel.Bind<IUser>().To<User>();
            kernel.Bind<INetwork>().To<Network>();
            kernel.Bind<IChannel>().To<Channel>();
            kernel.Bind<IIdentity>().To<Identity>();
            kernel.Bind<IServer>().To<Server>();

            kernel.Bind<DatabaseContext>().ToSelf().InThreadScope();
        }
    }
}
