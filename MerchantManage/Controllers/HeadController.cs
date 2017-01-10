using MerchantManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MerchantManage.Controllers
{
    public class HeadController : Controller
    {
        [Route("Head/Explore/{merchant}")]
        public ActionResult Explore(String merchant)
        {
            MerchantManagerFactory MManagerFact = (MerchantManagerFactory)System.Web.HttpContext.Current.Application["merchantManagerFactory"];
            MerchantManager manager = MManagerFact.CreateMerchantManager();
            Merchant mer = manager.ResolveMerchant(Request);
            ContentResult cont = new ContentResult();
            if (mer != null)
            {
              ServiceClientFactory serviceClientFact = (ServiceClientFactory)System.Web.HttpContext.Current.Application["serviceClientFactory"];
              ServiceClient serviceClient = serviceClientFact.GetServiceClient();
                String curnode = Request.QueryString["currentnode"];
                if (curnode == null)
                    curnode = "0";
                String response = serviceClient.SendRequest(mer,curnode);
                XslTransformer trans = new XslTransformer();
                String html = trans.Transform(response);
                cont.Content = html;
                return cont;
            }
            cont.Content = "Merchant does not exist.";
            return cont;
        }
    }
}