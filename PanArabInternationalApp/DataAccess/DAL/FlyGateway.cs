using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PanArabInternationalApp.DataAccess.Gatway;
using PanArabInternationalApp.Models;

namespace PanArabInternationalApp.DataAccess.DAL
{
    public class FlyGateway:Connection
    {
        public List<Fly> GetFlyList()
        {
            List<Fly> listOfFlyList= new List<Fly>();

            if (IsConnection)
            {
                var fly = DbEntities.sp_Fly().ToList();

                foreach (var value in fly)
                {

                    listOfFlyList.Add(new Fly
                    {
                        FormSl = value.PslNo,

                        FlyDate = Convert.ToDateTime(value.FlyDate),
                        Destination = Convert.ToString(value.FlyDestination),
                        TraAgName = Convert.ToString(value.TraAgName),
                        Status = Convert.ToString(value.Status),
                        PassNo = value.PassportNo,
                        Pname = value.Pname,

                    });
                }

                return listOfFlyList.ToList();
            }
            return listOfFlyList.ToList();
        }

        public List<Fly> GetFlyClearenceList()
        {
            List<Fly> listOfFlyList = new List<Fly>();

            if (IsConnection)
            {
                var fly = DbEntities.sp_ClearFly().ToList();

                foreach (var value in fly)
                {

                    listOfFlyList.Add(new Fly
                    {
                        FormSl = value.PslNo,

                        FlyDate = Convert.ToDateTime(value.FlyDate),
                        Destination = Convert.ToString(value.FlyDestination),
                        TraAgName = Convert.ToString(value.TraAgName),
                        Status = Convert.ToString(value.Status),
                        PassNo = value.PassportNo,
                        Pname = value.Pname,

                    });
                }

                return listOfFlyList.ToList();
            }
            return listOfFlyList.ToList();
        }
        public int Add(Fly fly)
        {
            if (IsConnection)
            {
                tbl_FlyDetails tblFyDetails = new tbl_FlyDetails();

                if (tblFyDetails.Id == 0)
                {

                    tblFyDetails.FormSlNo = fly.FormSl;
                    tblFyDetails.FlyDate = Convert.ToDateTime(fly.FlyDate);
                    tblFyDetails.FlyDestination = Convert.ToString(fly.Destination);
                    tblFyDetails.TraAgName = Convert.ToString(fly.TraAgName);
                    tblFyDetails.Status = "Clear";
                    tblFyDetails.FileDeleveryDate = fly.FileDeleveryDate;
                    tblFyDetails.FlighNo = fly.FlighNo;

                    tblFyDetails.UserID = Environment.UserName;
                    DbEntities.tbl_FlyDetails.Add(tblFyDetails);
                    int count = DbEntities.SaveChanges();
                    return count;

                }
            }
            return 0;
        }

        public bool IsExist(Fly fly)
        {
            if (IsConnection)
            {
                // string pidCard = passport.PNationlIdCard.ToString();
                int count = DbEntities.tbl_FlyDetails.Count(a => a.FormSlNo == fly.FormSl);

                if (count > 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}