using MerchantManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MerchantManage.Controllers
{
    
    public class AdminController : Controller
    {
        
        public ActionResult Index()
        {
            return View("Addition");
        }
        [Authorize]
        public ActionResult AddMerchant()
        {
            MerchantManagerFactory merchantManagerFactory = (MerchantManagerFactory)System.Web.HttpContext.Current.Application["merchantManagerFactory"];
            String merid = Request.Form["merid"];
            if (merid == null)
                return View();
            Merchant oldMerchant = merchantManagerFactory.CreateMerchantManager().FindById(merid);
            String uri = Request.Form["uri"];
            if ((oldMerchant != null)&&(uri == null))
            {
                return View(oldMerchant);
            }
            
            Merchant merchant = new Merchant(merid, uri);
            String username = Request.Form["username"];
            String password = Request.Form["password"];
            String logo = Request.Form["logo"];
            merchant.username = username;
            merchant.password = password;
            merchant.logo = logo;
            merchant.description = Request.Form["description"];
            merchant.XsltTemplate = Request.Unvalidated.Form["XsltTemplate"];
            Save(merchant);
            ViewBag.Results = merchantManagerFactory.CreateMerchantManager().GetAll();
            return View("SearchResult");
        }
        public ActionResult SearchMerchant()
        {
            MerchantManagerFactory merchantManagerFactory = (MerchantManagerFactory)System.Web.HttpContext.Current.Application["merchantManagerFactory"];
            String str = Request.Form["merid"];
            if (str == null)
                return View("SearchPage");
            Merchant mer = merchantManagerFactory.CreateMerchantManager().FindById(str);
            List<Merchant> lst = new List<Merchant>();
            if (mer != null)
                lst.Add(mer);
            ViewBag.Results = lst; 
            return View("SearchResult");
        }
        public ActionResult DeleteMerchant()
        {
            MerchantManagerFactory merchantManagerFactory = (MerchantManagerFactory)System.Web.HttpContext.Current.Application["merchantManagerFactory"];
            String str = Request.Form["merid"];
            if (str == null)
                return View("SearchPage");
            merchantManagerFactory.CreateMerchantManager().Remove(str);
            ViewBag.Results = merchantManagerFactory.CreateMerchantManager().GetAll();
            return View("SearchResult");
        }
        private void Save(Merchant merchant)
        {
            //throw new NotImplementedException();
            MerchantManagerFactory merchantManagerFactory = (MerchantManagerFactory)System.Web.HttpContext.Current.Application["merchantManagerFactory"];
            MerchantManager manager = merchantManagerFactory.CreateMerchantManager();
            manager.Add(merchant);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Main()
        {
            return View();
        }
    }
}