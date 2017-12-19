using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using PanArabInternationalApp.Models;

namespace PanArabInternationalApp.DataAccess.Bll.Manager
{

    public class EditManger:Controller
    {
        public ActionResult MedicalEdit(string formId, string actionView)
        {
            MedicalManager medicalManager = new MedicalManager();
          
            ViewBag.MedicalList = medicalManager.GetClearMedicals().Where(a => a.Formsl == formId.ToString()).ToList();
            ViewBag.MedicalListClear = medicalManager.GetClearMedicals();

            return View(actionView);
        }

        public ActionResult PassportEdit(string formId, string actionView)
        {
            PassportManager passportManager = new PassportManager();
           
            ViewBag.ListPassport = passportManager.GetAllSerialNo();
            ViewBag.PList = passportManager.GetAllPassports().Where(a => a.Formsl == formId.ToString()).ToList();

            return View(actionView);
        }

        public ActionResult VisaEdit(string formId, string actionView)
        {
            VisaManager visaManager = new VisaManager();
            ViewBag.VisaList = visaManager.GetClearvisa().Where(a=>a.FormSl==formId).ToList();

            //ViewBag.VisaList = visaManager.GetListVisa();
            ViewBag.ClearenceList = visaManager.GetClearvisa();


            Models.Visa visaModel = new Visa();

            visaModel.VisaCategory = visaManager.GetVisaCategory().ToList();


            return View(actionView,visaModel);
        }
        public ActionResult PC_ConsoleLetter(string formId, string actionView)
        {
          MofaManager  mofaManager = new MofaManager();
            //var list = mofaManager.GetMofaClearence();
            var list = new PANARAB_dbEntities().Sp_ExistPcClearence().ToList();


            ViewBag.ListOfPassenger = list;


            var clearData = new PC_ClearenceManager().GetlistOfPC_Clearence().ToList();
            ViewBag.ListOfPC = clearData;

            return View(actionView);
        }



        public ActionResult EditMangerRoute(string formId, string pageName)
        {




            return GetRoute(formId,pageName);
        }

        public ActionResult GetRoute(string formId,string pageName)
        {
            switch (pageName)
            {
                case "Medical":

                return  MedicalEdit(formId, pageName);
               
                case "Visa":

                return VisaEdit(formId, pageName);
            }

            return View("Error");
        }
    }

   
}