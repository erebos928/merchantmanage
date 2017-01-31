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
        void Update(Merchant mer);
        Merchant FindById(String id);
        Merchant FindByName(String name);
        List<Merchant> GetAll();
        void SetMerchantId(String id);
        Merchant ResolveMerchant(System.Web.HttpRequestBase req);
    }
}
