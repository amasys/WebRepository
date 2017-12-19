using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PanArabInternationalApp.DataAccess.Gatway;
using PanArabInternationalApp.Models;

namespace PanArabInternationalApp.DataAccess.DAL
{
    public class MofaGatway:Connection
    {
        public List<Mofa> GetMofaList()
        {
            List<Mofa> listOfMedicals = new List<Mofa>();

            if (IsConnection)
            {
                var mofa = DbEntities.sp_Mofa().ToList();

                foreach (var value in mofa)
                {

                    listOfMedicals.Add(new Mofa
                    {
                        Formsl = value.PslNo,
                        MofaDate = Convert.ToDateTime(value.MofaEnrollDate),
                        MofaNumber = value.MofaNo,
                      
                        PassNo = value.PassportNo,
                        PName = value.Pname,
                       
                    });
                }

                return listOfMedicals.ToList();
            }
            return listOfMedicals.ToList();
        }
        public List<Mofa> GetMofaClearList()
        {
            List<Mofa> listOfMedicals = new List<Mofa>();

            if (IsConnection)
            {
                var mofa = DbEntities.sp_ClearMofa().ToList();

                foreach (var value in mofa)
                {

                    listOfMedicals.Add(new Mofa
                    {
                        Formsl = value.PslNo,
                        MofaDate = Convert.ToDateTime(value.MofaEnrollDate),
                        MofaNumber = value.MofaNo,

                        PassNo = value.PassportNo,
                        PName = value.Pname,

                    });
                }

                return listOfMedicals.ToList();
            }
            return listOfMedicals.ToList();
        }
        public int Add(Mofa mofa)
        {
            if (IsConnection)
            {
                tbl_Mofa tblMofa = new tbl_Mofa();

                if (tblMofa.Id == 0)
                {

                    tblMofa.FormSl = mofa.Formsl;
                    tblMofa.MofaEnrollDate = mofa.MofaDate;
                    tblMofa.MofaNo = mofa.MofaNumber;
                 
                    tblMofa.UserId = Environment.UserName;
                    DbEntities.tbl_Mofa.Add(tblMofa);
                    int count = DbEntities.SaveChanges();
                    return count;

                }
            }
            return 0;
        }



        public bool IsExist(Mofa mofa)
        {
            if (IsConnection)
            {
                // string pidCard = passport.PNationlIdCard.ToString();
                int count = DbEntities.tbl_Mofa.Count(a => a.FormSl == mofa.Formsl);

                if (count > 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}