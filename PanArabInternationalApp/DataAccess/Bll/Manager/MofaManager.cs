using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PanArabInternationalApp.DataAccess.DAL;
using PanArabInternationalApp.Models;

namespace PanArabInternationalApp.DataAccess.Bll.Manager
{
    public class MofaManager:MofaGatway
    {
        public string Save(Mofa mofa)
        {
            try
            {
                if (IsExist(mofa))
                {
                    return "This Passport Already Exist";
                }
                else
                {
                    if (Add(mofa) > 0)
                    {
                        return "Save";
                    }
                    return "Save Failed";
                }
            }
            catch (Exception ex)
            {
                return "Save Failed";
            }
            

        }


        public List<Mofa> GetlistOfMofa()
        {
            return GetMofaList().ToList();
        }
        public List<Mofa> GetMofaClearence()
        {
            return GetMofaClearList().ToList();
        }

        public List<Mofa> GetPCclearence()
        {
            return GetMofaClearList().ToList();
        } 
    }
}