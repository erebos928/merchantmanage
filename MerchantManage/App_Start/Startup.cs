using MerchantManage.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MerchantManage.App_Start
{
    public class Startup
    {
        static void Setup()
        {
            HomeController.merchantManagerFactory = new test.MockMerchantManagerFactory();
        }
    }
}