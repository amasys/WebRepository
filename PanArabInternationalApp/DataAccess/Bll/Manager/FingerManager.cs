using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PanArabInternationalApp.DataAccess.DAL;
using PanArabInternationalApp.Models;

namespace PanArabInternationalApp.DataAccess.Bll.Manager
{
    public class FingerManager:FinngerGateway
    {
        
        public string Save(Finger finger)
        {
            try
            {
                if (IsExist(finger))
                {
                    return "This Finger Already Ready";
                }
                else
                {
                    if (Add(finger) > 0)
                    {
                        return "Save";
                    }
                    return "Visa Successfully Submited";
                }
            }
            catch (Exception ex)
            {

                return "Visa Not Submitted Submited";
            }

            
        }


        public List<Finger> GetFingersList()
        {
            return GetFingerList().ToList();
        }


        public List<Finger> GetClearFingersList()
        {
            return GetFingerClearList().ToList();
        } 
    }
}