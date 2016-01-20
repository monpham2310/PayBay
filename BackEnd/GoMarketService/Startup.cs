using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(GoMarketService.Startup))]

namespace GoMarketService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}