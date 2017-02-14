using MerchantManage.DAO;
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

        // [Authorize]
        public ActionResult GestionZone()
        {  //Read data from table tbzone
            DBZoneManager zoneManager = new DBZoneManager();
            ViewBag.Results = zoneManager.GetAll();
            return View();
        }
        //ajouter zone
        public ActionResult AddZone()
        {
            String zoneid = Request.Form["ID"];
            if (zoneid == null)
                return View();
            DBZoneManager zoneManager = new DBZoneManager();
            Zone oldZone = zoneManager.FindById(zoneid);
            if ((oldZone != null))
            {
                return View(oldZone);
            }
            Zone zone = new Zone();
            zone.ID = zoneid;
            zone.Name = Request.Form["Name"];
            zone.Logo = Request.Form["Logo"];
            zone.Template = Request.Unvalidated.Form["Template"];
            zoneManager.Add(zone);
            //Read data from table tbzone
            ViewBag.Results = zoneManager.GetAll();
            return View("GestionZone");
        }
        //modifier zone
        public ActionResult EditZone(String id)
        {
            DBZoneManager zoneManager = new DBZoneManager();
            if (id != null)
            {
                Zone zone = zoneManager.FindById(id);
                if (zone != null)
                {
                    ViewBag.id = zone.ID;
                    ViewBag.name = zone.Name;
                    ViewBag.logo = zone.Logo;
                    ViewBag.template = zone.Template;
                    return View();
                }
            }
            ViewBag.Results = zoneManager.GetAll();
            return View("GestionZone");
        }
        //update a zne
        public ActionResult UpdateZone()
        {
            Zone zone = new Zone();
            String logo = 
            zone.ID = Request.Form["ID"];
            zone.Name= Request.Form["Name"];
            zone.Logo = Request.Form["Logo"];
            zone.Template = Request.Unvalidated.Form["Template"];
            DBZoneManager zoneManager = new DBZoneManager();
            zoneManager.Update(zone);
            ViewBag.Results = zoneManager.GetAll();
            return View("GestionZone");
        }
        //call Remove for delete a zone
        public ActionResult DeleteZone(String id)
        {
            DBZoneManager zoneManager = new DBZoneManager();
            if (id != null)
            {
                zoneManager.Remove(id);
            }
            ViewBag.Results = zoneManager.GetAll();
            return View("GestionZone");
        }

        public ActionResult GestionMerchant()
        {
            merchantManagerFactory = (MerchantManagerFactory)System.Web.HttpContext.Current.Application["merchantManagerFactory"];

            //Read data from table tbmerchant
            ViewBag.Results = merchantManagerFactory.CreateMerchantManager().GetAll();
            return View();
        }
        //Edit a merchant 
        public ActionResult EditMerchant(String id)
        {
            merchantManagerFactory = (MerchantManagerFactory)System.Web.HttpContext.Current.Application["merchantManagerFactory"];
            if (id != null)
            {
                Merchant mer = merchantManagerFactory.CreateMerchantManager().FindById(id);
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
        public ActionResult DeleteMerchant(String id)
        {
            merchantManagerFactory = (MerchantManagerFactory)System.Web.HttpContext.Current.Application["merchantManagerFactory"];

            if (id != null)
            {
                merchantManagerFactory.CreateMerchantManager().Remove(id);
            }
            ViewBag.Results = merchantManagerFactory.CreateMerchantManager().GetAll();
            return View("GestionMerchant");
        }
        public ActionResult SearchMerchant(String id)
        {
            merchantManagerFactory = (MerchantManagerFactory)System.Web.HttpContext.Current.Application["merchantManagerFactory"];
            if (id == null)
                return View("SearchPage");
            Merchant mer = merchantManagerFactory.CreateMerchantManager().FindById(id);
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
