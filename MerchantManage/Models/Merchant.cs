using System;

namespace MerchantManage.Models
{
    public class Merchant
    {

        public String uri { get; set; }
        public String merid { get; set; }
        
        public Merchant(string merid, string uri)
        {
            this.merid = merid;
            this.uri = uri;
        }
    }
}