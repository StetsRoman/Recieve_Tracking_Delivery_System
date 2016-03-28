using RTDS.Domain.Concrete;
using RTDS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RTDS.WebUI.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }
       [HttpGet]
        public ViewResult Search(string searchString)
        {
            Package searchPkg;
            if (!String.IsNullOrEmpty(searchString))
            {
                UnitOfWork uow = new UnitOfWork();
                searchPkg = uow.GetRepository<Package>().Find(p=>p.Package_Number==searchString).FirstOrDefault();
            }
            else
            {
                searchPkg = new Package();
            }
            return View(searchPkg);
        }

     
    }
}
