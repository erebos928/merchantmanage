using MerchantManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MerchantManage.DAO
{
    public class DBMerchantManagerFactory: MerchantManagerFactory
    {
        static MerchantManager manager;
        public MerchantManager CreateMerchantManager()
        {
            if (manager == null)
                manager = new DBMerchantManager();
            return manager;
        }
    }
}