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
        MerchantManagerFactory merchantManagerFactory;

        //public ActionResult Index()
        //{
        //    return View("Addition");
        //}
       // [Authorize]
        public ActionResult GestionMerchant()
        {
            merchantManagerFactory = (MerchantManagerFactory)System.Web.HttpContext.Current.Application["merchantManagerFactory"];

            //Read data from table tbmerchant
            ViewBag.Results = merchantManagerFactory.CreateMerchantManager().GetAll();
            return View();
        }
        //Edit a merchant 
        public ActionResult EditMerchant(int id)
        {
            merchantManagerFactory = (MerchantManagerFactory)System.Web.HttpContext.Current.Application["merchantManagerFactory"];

            String str= id.ToString();
            if (str != null)
            {
                Merchant mer = merchantManagerFactory.CreateMerchantManager().FindById(str);
                if(mer != null)
                {
                    ViewBag.merid = mer.merid;
                    ViewBag.mername = mer.mername;
                    ViewBag.username = mer.username;
                    ViewBag.password = mer.password;
                    ViewBag.logo = mer.logo;
                    ViewBag.description = mer.description;
                    ViewBag.XsltTemplate = mer.XsltTemplate;
                    ViewBag.uri = mer.uri;

                    return View();
                }
            }
            ViewBag.Results = merchantManagerFactory.CreateMerchantManager().GetAll();
            return View("GestionMerchant");
        }
        //update a merchant
        public ActionResult UpdateMerchant()
        {
            Merchant merchant = new Merchant();
            String merid = Request.Form["merid"];
            String mername = Request.Form["mername"];
            String uri = Request.Form["uri"];
            String username = Request.Form["username"];
            String password = Request.Form["password"];
            String logo = Request.Form["logo"];
            merchant.merid = merid;
            merchant.mername = mername;
            merchant.uri = uri;
            merchant.username = username;
            merchant.password = password;
            merchant.logo = logo;
            merchant.description = Request.Form["description"];
            merchant.XsltTemplate = Request.Unvalidated.Form["XsltTemplate"];
            MerchantManagerFactory merchantManagerFactory = (MerchantManagerFactory)System.Web.HttpContext.Current.Application["merchantManagerFactory"];
            MerchantManager manager = merchantManagerFactory.CreateMerchantManager();
            manager.Update(merchant);
            ViewBag.Results = merchantManagerFactory.CreateMerchantManager().GetAll();
            return View("GestionMerchant");
        }
        //add a merchant
        public ActionResult AddMerchant()
        {
            merchantManagerFactory = (MerchantManagerFactory)System.Web.HttpContext.Current.Application["merchantManagerFactory"];
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
            String mername = Request.Form["mername"];
            String username = Request.Form["username"];
            String password = Request.Form["password"];
            String logo = Request.Form["logo"];
            merchant.mername = mername;
            merchant.username = username;
            merchant.password = password;
            merchant.logo = logo;
            merchant.description = Request.Form["description"];
            merchant.XsltTemplate = Request.Unvalidated.Form["XsltTemplate"];
            Save(merchant);
            //Read data from table tbmerchant
            ViewBag.Results = merchantManagerFactory.CreateMerchantManager().GetAll();
           
            return View("GestionMerchant");
        }
        
        //call Remove for delete a merchant
        public ActionResult DeleteMerchant(int id)
        {
            merchantManagerFactory = (MerchantManagerFactory)System.Web.HttpContext.Current.Application["merchantManagerFactory"];

            String str = id.ToString();
            if (str != null)
            {
                merchantManagerFactory.CreateMerchantManager().Remove(str);
            }
            ViewBag.Results = merchantManagerFactory.CreateMerchantManager().GetAll();
            return View("GestionMerchant");
        }
        public ActionResult SearchMerchant()
        {
            merchantManagerFactory = (MerchantManagerFactory)System.Web.HttpContext.Current.Application["merchantManagerFactory"];
            String str = "100";//Request.Form["merid"];
            if (str == null)
                return View("SearchPage");
            Merchant mer = merchantManagerFactory.CreateMerchantManager().FindById(str);
            List<Merchant> lst = new List<Merchant>();
            if (mer != null)
                lst.Add(mer);
            ViewBag.Results = lst; 
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