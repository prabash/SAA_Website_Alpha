using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TEST_ASP_ALPHA_1.Startup))]
namespace TEST_ASP_ALPHA_1
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
