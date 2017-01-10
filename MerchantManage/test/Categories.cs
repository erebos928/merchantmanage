using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MerchantManage.test
{
    public class Categories
    {
        static Dictionary<String, String> dic;
        static Categories()
        {
            dic = new Dictionary<string, string>();
            dic.Add("Appliances", "1");
            dic.Add("Home-Appliance", "1.1");
            dic.Add("Dishwashers", "1.1.1");
            dic.Add("Freezers", "1.1.2");
            dic.Add("Small-Appliance", "1.2");
            dic.Add("Coffee-Makers", "1.2.1");
            dic.Add("Microwaves", "1.2.2");
            dic.Add("Clothings", "2");
            dic.Add("Men", "2.1");
            dic.Add("Women", "2.2");
            dic.Add("Coats", "2.2.1");
            dic.Add("Boys", "2.3");
            dic.Add("Boys-Bottom", "2.3.1");
            dic.Add("Boys-Pants", "2.3.1.1");
            dic.Add("Boys-Denim", "2.3.1.2");
            dic.Add("Girls", "2.4");
            dic.Add("Girls-Bottom", "2.4.1");
            dic.Add("Girls-Pants", "2.4.1.1");
            dic.Add("Girls-Denim", "2.4.1.2");
            dic.Add("Shoes", "2.5");
            dic.Add("Girls-Shoes", "2.5.1");
            dic.Add("Boys-Shoes", "2.5.2");
            dic.Add("Men-Shoes", "2.5.3");
            dic.Add("Women-Shoes", "2.5.4");
        }
        public static String GetCode(String word)
        {
            String val = null;
            Boolean res = dic.TryGetValue(word, out val);
            if (res == false)
                val = "0";
            return val;
        }
    }
}