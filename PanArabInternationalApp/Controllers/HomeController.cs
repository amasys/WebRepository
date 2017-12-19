using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PanArabInternationalApp.DataAccess.Bll.Manager;

namespace PanArabInternationalApp.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        PassengerManger manger=new PassengerManger();
        public ActionResult Index()
        {
           int pcount= manger.GetAllPassengersList().Count;
           int clerenceMedical= new MedicalManager().GetClearMedicals().Count;
           int clearVisa= new VisaManager().GetClearvisa().Count;
           int flyCount= new FlyManager().GetClearListFly().Count;

            ViewBag.PCount = pcount;
            ViewBag.MCount = clerenceMedical;
            ViewBag.VCount = clearVisa;
            ViewBag.FCount = flyCount;
            return View();
        }

      

    }
}
