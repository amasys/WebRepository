using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PanArabInternationalApp.DataAccess.DAL;

namespace PanArabInternationalApp.DataAccess.Bll.Manager
{
    public class ReportManger:ReportGatway
    {
        public Sp_PassengerReportStatus_Result ReportStatus(string pslno)
        {
            return PassengerReport(pslno);
        }

        public Dictionary<string,string> PassengerList()
        {
            return GetPassengerList();
        }

        
    }
}