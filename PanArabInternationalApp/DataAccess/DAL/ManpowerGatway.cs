using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PanArabInternationalApp.DataAccess.Gatway;
using PanArabInternationalApp.Models;

namespace PanArabInternationalApp.DataAccess.DAL
{
    public class ManpowerGatway : Connection
    {
        public List<Manpower> GetManpowers()
        {
            List<Manpower> listOfManpowers = new List<Manpower>();

            if (IsConnection)
            {
                var manpower = DbEntities.sp_ManPower().ToList();

                foreach (var value in manpower)
                {

                    listOfManpowers.Add(new Manpower
                    {
                        FormSl = value.PslNo,
                        ManpowerDate = Convert.ToDateTime(value.ManpowerDate),

                        PassNo = value.PassportNo,
                        Pname = value.Pname,
                        Status = value.Status

                    });
                }

                return listOfManpowers.ToList();
            }
            return listOfManpowers.ToList();
        }

        public List<Manpower> GetManpowersClearenceList()
        {
            List<Manpower> listOfManpowers = new List<Manpower>();

            if (IsConnection)
            {
                var manpower = DbEntities.sp_ClearManPower().ToList();

                foreach (var value in manpower)
                {

                    listOfManpowers.Add(new Manpower
                    {
                        FormSl = value.PslNo,
                        ManpowerDate = Convert.ToDateTime(value.ManpowerDate),

                        PassNo = value.PassportNo,
                        Pname = value.Pname,
                        Status = value.Status

                    });
                }

                return listOfManpowers.ToList();
            }
            return listOfManpowers.ToList();
        }
        public int Add(Manpower manpower)
        {
            if (IsConnection)
            {
                tbl_Manpower tblManpower = new tbl_Manpower();

                if (tblManpower.Id == 0)
                {

                    tblManpower.FormSl = manpower.FormSl;
                    tblManpower.ManpowerDate = Convert.ToString(manpower.ManpowerDate);
                    tblManpower.Status = "Clear";

                    tblManpower.UserID = Environment.UserName;
                    DbEntities.tbl_Manpower.Add(tblManpower);
                    int count = DbEntities.SaveChanges();
                    return count;

                }
            }
            return 0;
        }
        
        public bool IsExist(Manpower manpower)
        {
            if (IsConnection)
            {
                // string pidCard = passport.PNationlIdCard.ToString();
                int count = DbEntities.tbl_Manpower.Count(a => a.FormSl == manpower.FormSl);

                if (count > 0)
                {
                    return true;

                }
            }
            return false;
        }

        public int Edit(Manpower manpower)
        {
            if (IsConnection)
            {
                tbl_Manpower tblManpower = new tbl_Manpower();

                var retriveManpowe = DbEntities.tbl_Manpower.FirstOrDefault(a => a.FormSl == manpower.FormSl);

                if (retriveManpowe!=null)
                {
                    retriveManpowe.ManpowerDate = manpower.ManpowerDate.ToString("d");
                    

                    int count = DbEntities.SaveChanges();

                    return count;

                }
            }
            return 0;
        }
    }
}