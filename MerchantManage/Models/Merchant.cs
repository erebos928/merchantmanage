using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace MerchantManage.Models
{
    public class Merchant
    {

        public String uri { get; set; }
        public String merid { get; set; }
        public String mername { get; set; }
        public String description { get; set; }
        public String username { get; set; }
        public String password { get; set; }
        public String logo { get; set; }
       
        public String XsltTemplate { get; set; }
        String path = "";
        public String Path { get { return path; } set { path = value; } }
        public Merchant() {   }
        public Merchant(string merid, string uri)
        {
           
            this.merid = merid;
            this.uri = uri;
        }
        public override string ToString()
        {
            return merid + ", " + uri;
        }
   

    }
    //This context class will define the database and table
    //MerchantContext is the name of connectionstring
    // Merchants represents database and Merchant(given in DbSet<Merchant>) represents table.
   // public class MerchantContext : DbContext
    //{
    //    public DbSet<Merchant> Merchants { get; set; }
    //}
}