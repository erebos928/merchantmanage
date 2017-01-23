using MerchantManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MerchantManage.DAO
{
    public class DBServiceClientFactory : ServiceClientFactory
    {
        public ServiceClient GetServiceClient()
        {
            return new DBServiceClient();
        }
    }
}