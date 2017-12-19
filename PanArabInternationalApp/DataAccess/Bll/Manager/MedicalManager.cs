using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PanArabInternationalApp.DataAccess.DAL;
using PanArabInternationalApp.Models;

namespace PanArabInternationalApp.DataAccess.Bll.Manager
{
    public class MedicalManager:MedicalGatway

    {
        public List<Medical> GetAllPendingMedicals()
        {
            var list = GetMedicalList().ToList();
            return list;

        }
        public string Save(Medical medical)
        {
            try
            {
                if (IsExist(medical))
                {
                    return "This Passenger Medical Already Submitted";
                }
                else
                {
                    if (Add(medical) > 0)
                    {
                        return "Save Medical Report";
                    }
                    return "Save Failed";
                }
            }
            catch (Exception ex)
            {

                return "Save Failed";
            }
          

        }

        public List<Medical> GetClearMedicals()
        {
            var list = GetMedicalClearList().ToList();
            return list;
           
        }

     
    }
}