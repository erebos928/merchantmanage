using System;

namespace MerchantManage.Models
{
    public class Merchant
    {

        public String uri { get; set; }
        public String merid { get; set; }
        public String description { get; set; }
        public String username { get; set; }
        public String password { get; set; }
        public String logo { get; set; }
        public Merchant(string merid, string uri)
        {
            this.merid = merid;
            this.uri = uri;
        }
    }
}