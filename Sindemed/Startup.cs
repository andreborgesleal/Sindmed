using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sindemed.Startup))]
namespace Sindemed
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
