using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MerchantManage.Models
{
    public class Item
    {
        public String Identifier { get; set; }
        public Item(String id)
        {
            Identifier = id;
        }
    }
}