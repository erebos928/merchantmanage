using MerchantManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MerchantManage.test
{
    public class MockMerchantManager:MerchantManager
    {
        Dictionary<String, Merchant> repository = new Dictionary<string, Merchant>();

        public void Add(Merchant mer)
        {
            repository.Add(mer.merid, mer);
        }

        public Merchant FindById(string id)
        {
            Merchant res;
            repository.TryGetValue(id, out res);
            return res;
        }

        public Merchant FindByName(string name)
        {
            return null;
        }

        public List<Merchant> GetAll()
        {
            return repository.Values.ToList<Merchant>();
        }

        public void Remove(string id)
        {
            repository.Remove(id);
        }
    }
}