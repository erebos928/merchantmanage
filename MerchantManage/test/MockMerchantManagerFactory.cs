using MerchantManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MerchantManage.test
{
    public class MockMerchantManagerFactory: MerchantManagerFactory
    {
        static MerchantManager manager;
        public MerchantManager CreateMerchantManager()
        {
            if (manager == null)
                manager = new MockMerchantManager();
            return manager;
        }
    }
}