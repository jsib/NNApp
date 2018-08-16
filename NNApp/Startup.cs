using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NNApp.Startup))]
namespace NNApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
