using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PanArabInternationalApp.DataAccess.DAL;
using PanArabInternationalApp.Models;

namespace PanArabInternationalApp.DataAccess.Bll.Manager
{
    public class PC_ClearenceManager:PoliceClearenceGateway
    {
        public string Save(PC_ConsoleLetter pcConsoleLetter)
        {
            try
            {
                if (IsExist(pcConsoleLetter))
                {
                    return "This Passenger is Updated";
                }
                else
                {
                   
                        if (Add(pcConsoleLetter) > 0)
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


        public List<PC_ConsoleLetter> GetlistOfPC_Clearence()
        {
            return GetpcClearlist().ToList();
        }

     
    }
}