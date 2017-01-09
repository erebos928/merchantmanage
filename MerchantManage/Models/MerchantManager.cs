using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantManage.Models
{
    public interface MerchantManager
    {
        void Add(Merchant mer);
        void Remove(String id);
        Merchant FindById(String id);
        Merchant FindByName(String name);
        List<Merchant> GetAll();
        Merchant ResolveMerchant(System.Web.HttpRequestBase req);
    }
}
