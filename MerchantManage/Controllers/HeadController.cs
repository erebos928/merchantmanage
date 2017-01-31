using MerchantManage.Models;
using System;
using System.Collections.Generic;
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
                
                
              //  DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(String));
               // MemoryStream stream = new MemoryStream();
                //StreamWriter tw = new StreamWriter(stream);
                //tw.Write(response);
                //tw.Flush();
                //stream.Position = 0;
                //String src = (String)ser.ReadObject(stream);
                String html = trans.Transform(response,mer);
                cont.Content = html;
                return cont;
            }
            cont.Content = "Merchant does not exist.";
            return cont;
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