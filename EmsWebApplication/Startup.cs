using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EmsWebApplication.Startup))]
namespace EmsWebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
