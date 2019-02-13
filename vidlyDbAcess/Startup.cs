using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(vidlyDbAcess.Startup))]
namespace vidlyDbAcess
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
