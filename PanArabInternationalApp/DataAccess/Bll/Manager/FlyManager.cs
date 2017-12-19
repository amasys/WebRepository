using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PanArabInternationalApp.DataAccess.DAL;
using PanArabInternationalApp.DataAccess.Gatway;
using PanArabInternationalApp.Models;

namespace PanArabInternationalApp.DataAccess.Bll.Manager
{
    public class FlyManager:FlyGateway
    {
        public string Save(Fly fly)
        {
            try
            {
                if (IsExist(fly))
                {
                    return "This Passenger Flight   Ready Entry";
                }
                else
                {
                    if (Add(fly) > 0)
                    {
                        return "Fly is Ready ";
                    }
                    return "Failed";
                }
            }
            catch (Exception ex)
            {

                return "Failed";
            }
           

        }


        public List<Fly> GetListFly()
        {
            return GetFlyList().ToList();
        }


        public List<Fly> GetClearListFly()
        {
            return GetFlyClearenceList().ToList();
        } 
    }
}