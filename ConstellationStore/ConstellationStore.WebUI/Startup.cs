using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ConstellationStore.WebUI.Startup))]
namespace ConstellationStore.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
