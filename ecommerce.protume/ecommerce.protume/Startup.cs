using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ecommerce.protume.Startup))]
namespace ecommerce.protume
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
