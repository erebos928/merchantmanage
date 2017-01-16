using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MerchantManage.Models
{
    public class Basket
    {
        public int Count { get { return content.Count; } }
        List<Item> content = new List<Item>();
        public void AddItem(Item item)
        {
            if (content.Contains(item))
                return;
            content.Add(item);
            
        }
    }
}