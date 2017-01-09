using MerchantManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MerchantManage.test
{
    public class MockServiceClientFactory : ServiceClientFactory
    {
        public ServiceClient GetServiceClient()
        {
            return new MockServiceClient();
        }
    }
}