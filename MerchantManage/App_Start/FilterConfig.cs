﻿using MerchantManage.Controllers;
using System.Web;
using System.Web.Mvc;

namespace MerchantManage
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            
        }
    }
}
