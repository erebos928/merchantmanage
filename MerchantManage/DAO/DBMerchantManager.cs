using MerchantManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MerchantManage.DAO
{
    public class DBMerchantManager:MerchantManager
    {
        Dictionary<String, Merchant> repository = new Dictionary<string, Merchant>();

        public void Add(Merchant mer)
        {
            if (repository.ContainsKey(mer.merid))
                repository.Remove(mer.merid);
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

        public Merchant ResolveMerchant(HttpRequestBase req)
        {
            String url = req.Url.ToString();
            String[] parts = url.Split(new char[] { '/' });
            if (parts.Length < 4)
                return null;
            String method = parts[3];
            Merchant merchant = null;
            String zone = null;
            String division = null;
            if (parts.Length > 4)
                zone = parts[4];
            if (parts.Length > 5)
                division = parts[5];
            if (division != null)
                 merchant = FindById(division);
            if (merchant != null)
            {
                for (int i = 6; i < parts.Length; i++)
                    merchant.AddPart(parts[i]);
                return merchant;
            }
            return null;
        }
    }
}