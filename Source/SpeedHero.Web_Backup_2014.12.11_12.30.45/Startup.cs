using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SpeedHero.Web.Startup))]

namespace SpeedHero.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}
