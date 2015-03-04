using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(www.ptsol.com.au.Startup))]
namespace www.ptsol.com.au
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
