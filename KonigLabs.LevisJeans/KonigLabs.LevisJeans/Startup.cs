using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KonigLabs.LevisJeans.Startup))]
namespace KonigLabs.LevisJeans
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
