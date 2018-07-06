using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DoAn1.Startup))]
namespace DoAn1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
