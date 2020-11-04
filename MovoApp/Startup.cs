using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MovoApp.Startup))]
namespace MovoApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
