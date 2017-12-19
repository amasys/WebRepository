using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PanArabInternationalApp.DataAccess.DAL;
using PanArabInternationalApp.Models;

namespace PanArabInternationalApp.DataAccess.Bll.Manager
{
    public class PassengerManger:PassengerGatway

    {
        public string Save(Passenger aPassenger)
        {
            try
            {
                if (IsExist(aPassenger))
                {
                    return "Passenger Updated";
                }
                else
                {
                    if (Add(aPassenger) > 0)
                    {
                        return "Save";
                    }
                    return "Save Failed";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;

            }
           

        }

        public List<Passenger> GetAllPassengersList()
        {
            return GetAllPassengers().ToList();
        }
        public Passenger EditPassengers(string pid)
        {
            return GetAllPassengers().FirstOrDefault(a=>a.PSlNo==pid);
        } 
        public string SerialNo()
        {
            return Serial();
        }   
        public List<SelectListItem> IdistrictList()
        {
            List<SelectListItem> IDistrict = GetAllDistrict();
            
            return IDistrict;
        }
    }
}