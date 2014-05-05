namespace NazureBot.Core.Services.User
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using NazureBot.Core.Infrastructure.Entities;
    using NazureBot.Modules.Messaging;

    public class FakeXmppUserServer : IUserService
    {
        public async Task<IUser> GetOrCreateByHostmaskAsync(string hostmask)
        {
            return await Task.FromResult(new User("Peej@192.168.0.1"));
        }

        public async Task<IEnumerable<IUser>> GetUsersAsync()
        {
            var user1 = new User("user1@192.168.0.1");
            var user2 = new User("user2@192.168.0.1");

            var users = new[] { user1, user2 };

            return await Task.FromResult(users);
        }
    }
}