using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PanArabInternationalApp.DataAccess.DAL;
using PanArabInternationalApp.EmailConfig;
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
                   
                    return "This Passenger Medical Updated";
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

        public void Update(Passenger passenger)
        {
            try
            {
                tbl_Medical passengerList = DbEntities.tbl_Medical.SingleOrDefault(a => a.FormSl == passenger.Medical.Formsl);

                if (passengerList != null)
                {

                    passengerList.MedicalContactAmount = Convert.ToInt32(passenger.Medical.MedicalContactAmount);
                    passengerList.MedicalReport = passenger.Medical.MedicalReport;
                    DbEntities.SaveChanges();

                }

            }
            catch (Exception ex)
            {

                new CustomizeMessageSentToEmail().SentMail(ex);
            }

        }
     
    }
}