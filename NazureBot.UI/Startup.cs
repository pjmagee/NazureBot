
[assembly: Microsoft.Owin.OwinStartupAttribute(typeof(NazureBot.UI.Startup))]

namespace NazureBot.UI
{
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
