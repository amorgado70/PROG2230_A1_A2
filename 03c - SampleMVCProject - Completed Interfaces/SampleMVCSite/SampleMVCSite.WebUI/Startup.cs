using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SampleMVCSite.WebUI.Startup))]
namespace SampleMVCSite.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
