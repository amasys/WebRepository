using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PanArabInternationalApp.DataAccess.Gatway;

namespace PanArabInternationalApp.DataAccess.DAL
{
    public class ReportGatway:Connection
    {
        public Sp_PassengerReportStatus_Result PassengerReport(string pslno)
        {
            if (IsConnection)
            {
                var result = DbEntities.Sp_PassengerReportStatus().FirstOrDefault(a => a.PslNo == pslno);
                return result;
            }
            return null;
        }
        public Dictionary<string, string> GetPassengerList()
        {
            if (IsConnection)
            {
                var list=new Dictionary<string,string>();
                foreach (var source in DbEntities.tbl_Passenger.ToList())
                {
                    list.Add(source.PslNo,source.Pname);

                }
                return list;
            }
           return null;
        }
    }
}