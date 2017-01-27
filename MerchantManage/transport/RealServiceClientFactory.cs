using MerchantManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MerchantManage.transport
{
    public class RealServiceClientFactory : ServiceClientFactory
    {
        public ServiceClient GetServiceClient()
        {
            return new RealServiceClient();
        }
    }
}