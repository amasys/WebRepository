using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PanArabInternationalApp.DataAccess.DAL;
using PanArabInternationalApp.Models;

namespace PanArabInternationalApp.DataAccess.Bll.Manager
{
    public class ManpowerManager:ManpowerGatway
    {
        public string Save(Manpower manpower)
        {
            try
            {
                if (IsExist(manpower))
                {
                    if (Edit(manpower) > 0)
                    {
                        return "This Manpower is Up to Dated";
                    }
                    else
                    {
                        return "This Manpower is Already Added";
                    }

                }
                else
                {
                    if (Add(manpower) > 0)
                    {
                        return "Manpower Save";
                    }
                    return "Faield";
                }
            }
            catch (Exception ex)
            {

                return "Faield";
            }
           

        }


        public List<Manpower> GetListManpowers()
        {
            return GetManpowers().ToList();
        } 
    }
}