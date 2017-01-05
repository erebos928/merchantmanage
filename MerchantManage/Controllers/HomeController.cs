using MerchantManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MerchantManage.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult AddMerchant()
        {
            String merid = Request.Form["merid"];
            String uri = Request.Form["uri"];
            Merchant merchant = new Merchant(merid, uri);
            Save(merchant);
            return View();
        }
        public ActionResult SearchMerchant()
        {
            Merchant mer1 = new Merchant("id325", "http://exemple.com/service");
            Merchant mer2 = new Merchant("id893", "http://somehost.com/point");
            List<Merchant> lst = new List<Merchant>();
            lst.Add(mer1);
            lst.Add(mer2);
            ViewBag.Results = lst;
            return View("SearchResult");
        }
        private void Save(Merchant merchant)
        {
            //throw new NotImplementedException();
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
    }
}