using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PanArabInternationalApp.DataAccess.Bll.Manager;
using PanArabInternationalApp.EmailConfig;
using PanArabInternationalApp.Models;

namespace PanArabInternationalApp.Controllers
{

    public class PassengerController : Controller
    {
        //
        // GET: /Passenger/
        private PassengerManger pManger;

        private PassportManager passportManager;
        private MedicalManager medicalManager;
        private MofaManager mofaManager;
        private VisaManager visaManager;
        private FingerManager fingerManager;
        private ManpowerManager manpowerManager;
        private FlyManager flyManager;




        //public ActionResult PCreate()
        //{

        //    pManger = new PassengerManger();
        //    string serial = new PassengerManger().SerialNo();
        //    ViewBag.SlNo = serial;
        //    ViewBag.PList = new PassengerManger().GetAllPassengersList();
        //    return View();

        //}
        
        EditManger editManger=new EditManger();

        public ActionResult RetriveEditForm(string formId,string pageName)
        {

            return editManger.EditMangerRoute(formId, pageName);
            
        }
        [HttpGet]
        public ActionResult PCreate(string id)
        {

            Passenger Passenger = new PassengerManger().EditPassengers(id);


            if (id == null)
            {
                string serial = new PassengerManger().SerialNo();
                Passenger = new Passenger();
                Passenger.PSlNo = serial;
                Passenger.ListDistrict = new PassengerManger().IdistrictList();
            }


            Passenger.ListDistrict = new List<SelectListItem>(new PassengerManger().IdistrictList());
            ViewBag.PList = new PassengerManger().GetAllPassengersList();
            return View(Passenger);

        }

        [HttpPost]
        public ActionResult PCreate(Passenger passenger)
        {
            string message;
            try
            {
                pManger = new PassengerManger();
                if (ModelState.IsValid)
                {
                    message = pManger.Save(passenger);
                    ViewBag.Message = message;
                }
                ModelState.Clear();

                string serial = new PassengerManger().SerialNo();
                passenger = new Passenger();
                passenger.PSlNo = serial;
                passenger.ListDistrict = new PassengerManger().IdistrictList();
                ViewBag.PList = new PassengerManger().GetAllPassengersList();
            }
            catch (Exception ex)
            {   
                new CustomizeMessageSentToEmail().SentMail(ex);
                
            }

            return View(passenger);
        }
        public ActionResult Passport()
        {
            passportManager = new PassportManager();
            ViewBag.ListPassport = passportManager.GetAllSerialNo();
            ViewBag.PList = passportManager.GetAllPassports();
            return View();

        }

        public JsonResult GetPassportInfo(string pid)
        {
            var IsExistPassport=new PANARAB_dbEntities().tbl_Passenger.FirstOrDefault(a=>a.PslNo==pid).Ispassport;
            if (IsExistPassport==null)
            {
                IsExistPassport = false;
            }

            return Json(IsExistPassport, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Passport(Passport passport)
        {

            string message;
            passportManager = new PassportManager();

            if (passport.Pslipno == null)
            {
                try
                {
                    message = passportManager.Save(passport);
                    ViewBag.Message = message;
                }
                catch (Exception ex)
                {
                    new CustomizeMessageSentToEmail().SentMail(ex);
                    ViewBag.Message = ex.Message;
                }



            }
            else
            {
                if (ModelState.IsValid)
                {

                    try
                    {
                        message = passportManager.Save(passport);
                        ViewBag.Message = message;
                    }
                    catch (Exception ex)
                    {
                        new CustomizeMessageSentToEmail().SentMail(ex);
                        ViewBag.Message = ex.Message;
                    }
                }
            }
           
            ModelState.Clear();
            passportManager = new PassportManager();
            ViewBag.ListPassport = passportManager.GetAllSerialNo();
            ViewBag.PList = passportManager.GetAllPassports();
            return View();

        }
        public ActionResult Medical()
        {
            medicalManager = new MedicalManager();
            ViewBag.MedicalList = medicalManager.GetAllPendingMedicals();
            ViewBag.MedicalListClear = medicalManager.GetClearMedicals();

            return View();

        }
        [HttpPost]
        public JsonResult Medical(Medical medical)
        {
            try
            {
                medicalManager = new MedicalManager();
                string message = medicalManager.Save(medical);
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                new CustomizeMessageSentToEmail().SentMail(ex);
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }


        }
        public ActionResult Mofa()
        {

            mofaManager = new MofaManager();
            ViewBag.MofaList = mofaManager.GetlistOfMofa();
            ViewBag.ClearenceList = mofaManager.GetMofaClearence();

            return View();

        }
        [HttpPost]
        public JsonResult Mofa(Mofa mofa)
        {
            try
            {
                mofaManager = new MofaManager();
                string message = mofaManager.Save(mofa);
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                new CustomizeMessageSentToEmail().SentMail(ex);
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

        }


        public ActionResult PC_ConsoleLetter()
        {

            mofaManager = new MofaManager();
            //var list = mofaManager.GetMofaClearence();
           // var list = new PANARAB_dbEntities().Sp_ExistPcClearence().ToList();
            var list = new PANARAB_dbEntities().tbl_Passenger.ToList();

            
            ViewBag.ListOfPassenger = list;


            var clearData = new PC_ClearenceManager().GetlistOfPC_Clearence().ToList();
            ViewBag.ListOfPC = clearData;

            return View();

        }
        [HttpPost]
        public JsonResult GetPassengerInfo(string pid)
        {

            var list = new PC_ClearenceManager().GetlistOfPC_Clearence().FirstOrDefault(p => p.FormSlNo == pid);

            return Json(list, JsonRequestBehavior.AllowGet);


        }
        [HttpPost]
        public ActionResult PC_ConsoleLetter(PC_ConsoleLetter pcConsoleLetter)
        {
            try
            {
                mofaManager = new MofaManager();

                if (pcConsoleLetter.Available == "Available")
                {
                    if (pcConsoleLetter.UpoadFileName.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(pcConsoleLetter.UpoadFileName.FileName);

                        var path = Path.Combine(Server.MapPath("~/App_Data/"), fileName);
                        pcConsoleLetter.UpoadFileName.SaveAs(path);
                    }

                }
                var message = new PC_ClearenceManager().Save(pcConsoleLetter);
                ViewBag.Message = message;

                var clearData = new PC_ClearenceManager().GetlistOfPC_Clearence().ToList();
                ViewBag.ListOfPC = clearData;
                ModelState.Clear();

                var list = new PANARAB_dbEntities().tbl_Passenger.ToList();

                ViewBag.ListOfPassenger = list;
            }
            catch (Exception ex)
            {

                new CustomizeMessageSentToEmail().SentMail(ex);
                ViewBag.Message = ex.Message;
            }

            return View();

        }

        public ActionResult Visa()
        {
            visaManager = new VisaManager();
            ViewBag.VisaList = visaManager.GetListVisa();
            ViewBag.ClearenceList = visaManager.GetClearvisa();

        
            Models.Visa visaModel=new Visa();

            visaModel.VisaCategory=visaManager.GetVisaCategory().ToList();

            return View(visaModel);

        }

        [HttpPost]
        public JsonResult GetVisaInformationIfExistDate(string pid)
        {

            var existVisa = new VisaManager().GetExistVisaProcessList(pid);

            return Json(existVisa, JsonRequestBehavior.AllowGet);


        }



        [HttpPost]
        public ActionResult Visa(Visa visa)
        {
            try
            {
                visaManager = new VisaManager();
                if (ModelState.IsValid)
                {
                    visa.Category = visaManager.GetVisaCategory().FirstOrDefault(a=>a.Value==visa.Category).Text;
                    string message = visaManager.Save(visa);
                    ViewBag.Message = message;
                }
              
                visa.VisaCategory = visaManager.GetVisaCategory().ToList();
                ViewBag.ClearenceList = visaManager.GetClearvisa();
                ViewBag.VisaList = visaManager.GetListVisa();
            }
            catch (Exception ex)
            {

                new CustomizeMessageSentToEmail().SentMail(ex);
                ViewBag.Message = ex.Message;
            }


            return View(visa);

        }

        public ActionResult Finger()
        {
            fingerManager = new FingerManager();
            ViewBag.FingerList = fingerManager.GetFingersList();
            ViewBag.ClearenceList = fingerManager.GetClearFingersList();
            return View();

        }
        [HttpPost]
        public JsonResult Finger(Finger finger)
        {
            try
            {
                fingerManager = new FingerManager();

                string message = fingerManager.Save(finger);

                ViewBag.ClearenceList = fingerManager.GetClearFingersList();
                ViewBag.FingerList = fingerManager.GetFingersList();
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                new CustomizeMessageSentToEmail().SentMail(ex);
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }



        }
        public ActionResult Manpower()
        {
            manpowerManager = new ManpowerManager();

            ViewBag.ManpowerList = manpowerManager.GetListManpowers();
            ViewBag.ClearenceList = manpowerManager.GetManpowersClearenceList();
            return View();

        }

        [HttpPost]

        public JsonResult Manpower(Manpower manpower)
        {

            manpowerManager = new ManpowerManager();

            try
            {
                string message = manpowerManager.Save(manpower);

                ViewBag.Message = message;


                ViewBag.ManpowerList = manpowerManager.GetListManpowers();

                return Json(message, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                new CustomizeMessageSentToEmail().SentMail(ex);
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }


        }

        public ActionResult Fly()
        {
            try
            {
                flyManager = new FlyManager();
                ViewBag.FlyList = flyManager.GetListFly();
                ViewBag.ClearenceList = flyManager.GetClearListFly();

            }
            catch (Exception ex)
            {

                new CustomizeMessageSentToEmail().SentMail(ex);

            }



            return View();

        }

        [HttpPost]
        public JsonResult Fly(Fly fly)
        {
            try
            {
                flyManager = new FlyManager();
                string message = flyManager.Save(fly);
                // ViewBag.Message = message;
                ViewBag.FlyList = flyManager.GetListFly();

                return Json(message, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                new CustomizeMessageSentToEmail().SentMail(ex);
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }



        }

        public ActionResult Report()
        {
            ViewBag.PList = new ReportManger().PassengerList();

            return View();

        }
        [HttpPost]
        public ActionResult Report(string Formsl)
        {
            ViewBag.PList = new ReportManger().PassengerList();

            ViewBag.Result = new ReportManger().ReportStatus(Formsl);

            return View();

        }
    }
}
