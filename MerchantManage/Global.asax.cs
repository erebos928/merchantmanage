using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Configuration;
using System.Data.SqlClient;

namespace MerchantManage
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static string ConnString1 { get; set; }
        public static string Erreur { get; set; }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
           try
            {
                ConnString1 = ConfigurationManager.ConnectionStrings["MerchantConnection"].ConnectionString;
               
            }
            catch (Exception ex)
            {
                Erreur = string.Format("Erreur de configuration : {0}", ex.Message);
               
            }
            // System.Web.HttpContext.Current.Application["merchantManagerFactory"] = new test.MockMerchantManagerFactory();
            //  System.Web.HttpContext.Current.Application["serviceClientFactory"] = new test.MockServiceClientFactory();
              System.Web.HttpContext.Current.Application["merchantManagerFactory"] = new DAO.DBMerchantManagerFactory();
             // System.Web.HttpContext.Current.Application["serviceClientFactory"] = new DAO.DBServiceClientFactory();
        }
    }
}
