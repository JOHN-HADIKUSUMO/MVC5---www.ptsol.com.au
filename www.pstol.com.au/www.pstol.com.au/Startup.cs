using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(www.pstol.com.au.Startup))]
namespace www.pstol.com.au
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
