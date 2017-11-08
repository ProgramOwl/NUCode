using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NUCode.Startup))]
namespace NUCode
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
