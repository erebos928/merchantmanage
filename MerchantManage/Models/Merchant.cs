using System;
using System.Collections.Generic;

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
        public List<String> parts { get; set; }
        public String XsltTemplate { get; set; }
        String path = "";
        public String Path { get { return path; } set { path = value; } }
        public Merchant(string merid, string uri)
        {
            parts = new List<String>();
            this.merid = merid;
            this.uri = uri;
        }
        public override string ToString()
        {
            return merid + ", " + uri;
        }
        public void AddPart(String part)
        {
            parts.Add(part);
        }
        
    }
}