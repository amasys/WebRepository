using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PanArabInternationalApp.DataAccess.DAL;
using PanArabInternationalApp.Models;

namespace PanArabInternationalApp.DataAccess.Bll.Manager
{
    public class VisaManager:VisaGateway 
    {
        public string Save(Visa visa)
        {
            try
            {
                if (IsExist(visa))
                {
                    return "This Visa is Updated ";
                }
                else
                {
                    if (Add(visa) > 0)
                    {
                        return "Save";
                    }
                    return "Visa Successfully Submited";
                }
            }
            catch (Exception ex)
            {

               return  ex.Message;

            }
          

        }

        public tbl_VisaProcessing GetExistVisaProcessList(string PslNo)
        {
            if (IsConnection)
            {
                var tblVisaInformation = DbEntities.tbl_VisaProcessing.FirstOrDefault(a => a.FormSl == PslNo);
                if (tblVisaInformation!=null)
                {
                    return tblVisaInformation;
                }
            }
            return null;
        }



        public List<Visa> GetListVisa()
        {
            return GetAllVisaProcessing().ToList();
        }



        public List<Visa> GetClearvisa()
        {
            return GetAllVisaClearencelist().ToList();
        }


        public List<SelectListItem> GetVisaCategory()
        {
            List<SelectListItem> IDistrict = GetAllVisaCate();

            return IDistrict;
        }
    }
}