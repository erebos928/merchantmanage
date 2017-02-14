using MerchantManage.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MerchantManage.Controllers
{
    public class HeadController : Controller
    {
        [Route("Head/Explore/{merchant}")]
        public async Task<ActionResult> Explore(String merchant)
        {
            MerchantManagerFactory MManagerFact = (MerchantManagerFactory)System.Web.HttpContext.Current.Application["merchantManagerFactory"];
            MerchantManager manager = MManagerFact.CreateMerchantManager();
            if (merchant.Equals("all"))
                return Intro();
            manager.SetMerchantId(merchant);
            Merchant mer = manager.ResolveMerchant(Request);
            ContentResult cont = new ContentResult();
            if (mer != null)
            {
                ServiceClientFactory serviceClientFact = (ServiceClientFactory)System.Web.HttpContext.Current.Application["serviceClientFactory"];
                ServiceClient serviceClient = serviceClientFact.GetServiceClient();
                String curnode = Request.QueryString["currentnode"];
                if (curnode == null)
                    curnode = "-1";
                String response = await serviceClient.SendRequest(mer, curnode);
                XslTransformer trans = new XslTransformer();
                response = response.Substring(1, response.Length - 2);
                response = response.Replace("\\","");
                response = Encoding.UTF8.GetString(Convert.FromBase64String(response));
                String html = trans.Transform(response,mer);
                cont.Content = html;
                return cont;
            }
            cont.Content = "Merchant does not exist.";
            return cont;
        }
        private ActionResult Intro()
        {
            SqlConnection conn = new SqlConnection(MvcApplication.ConnString1);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "select MERCHANTID,MERCHANTNAME,LOGO from TBMERCHANT";
            conn.Open();
            SqlDataReader reader = command.ExecuteReader();
            String s = "";
            while (reader.Read())
            {
                s += "<Division id=\"" + reader.GetString(0) + "\"" + " preferredName=\"" + reader.GetString(1) +
                    "\">" + "<Logo>" + reader.GetString(2) + "</Logo></Division>";
            }
            reader.Close();
            command = new SqlCommand();
            command.CommandText = "select ID,NAME,LOGO,TEMPLATE from Zone";
            command.Connection = conn;
            reader = command.ExecuteReader();
            String w = "<Catalogue><Zone id=";
            String xsl = "";
            while (reader.Read())
            {
                w += "\"" + reader.GetString(0) + "\"" + " preferredName=\"" + reader.GetString(1) +
                    "\"" + "><Logo>" + reader.GetString(2) + "</Logo>" + s + "</Zone></Catalogue>";
                xsl = reader.GetString(3);
            }
            Merchant dummyMerchant = new Merchant();
            dummyMerchant.XsltTemplate = xsl;
            ContentResult listContent = new ContentResult();
            String src = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + w ;
            reader.Close();
            conn.Close();
            XslTransformer transformer = new XslTransformer();
            listContent.Content = transformer.Transform(src, dummyMerchant);
            return listContent;
        }
        public ActionResult ArticleCount()
        {
            Basket basket = null;
            int cnt = 0;
            if (Session["Basket"] != null)
            {
                basket = (Basket)Session["Basket"];
                cnt = basket.Count;
            }
            ContentResult content = new ContentResult();
            content.Content = cnt.ToString();
            return content;
        }
        [Route("Head/AddToCart/{itemId}")]
        public ActionResult AddToCart(String itemId)
        {
            Object obj = Session["Basket"];
            Basket basket = new Basket();
            if (obj != null)
                basket = (Basket)obj;
            else
                basket = new Basket();
            Item item = new Item(itemId);
            basket.AddItem(item);
            Session["Basket"] = basket;
            ContentResult cr = new ContentResult();
            cr.Content = "The item added successfully.";
            return cr;


        }
    }
}