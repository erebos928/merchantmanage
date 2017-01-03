using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MerchantManage.Startup))]
namespace MerchantManage
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
