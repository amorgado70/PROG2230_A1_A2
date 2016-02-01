using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ConstellationStore.Startup))]
namespace ConstellationStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
