using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PanArabInternationalApp.DataAccess.DAL;
using PanArabInternationalApp.Models;

namespace PanArabInternationalApp.DataAccess.Bll.Manager
{
    public class PassportManager:PassportGateway
    {
        public List<string> GetAllSerialNo()
        {
            return GetList();
        }

        public string Save(Passport aPassport)
        {
            try
            {
                if (IsExist(aPassport))
                {
                    return "This Passport Already Exist";
                }
                else
                {
                    if (Add(aPassport) > 0)
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


        public List<Passport> GetPassport()
        {
            return GetAllPassports().ToList();
        } 
    }
}