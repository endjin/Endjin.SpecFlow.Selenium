using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Endjin.SpecFlow.Selenium.Sample.Web.Startup))]
namespace Endjin.SpecFlow.Selenium.Sample.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
