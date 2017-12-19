using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PanArabInternationalApp.DataAccess.Gatway;
using PanArabInternationalApp.EmailConfig;
using PanArabInternationalApp.Models;

namespace PanArabInternationalApp.DataAccess.DAL
{
    public class FinngerGateway:Connection
    {

        public List<Finger> GetFingerList()
        {
            List<Finger> listOfFingers = new List<Finger>();
            try
            {
              

                if (IsConnection)
                {
                    var finger = DbEntities.sp_Finger().ToList();

                    foreach (var value in finger)
                    {

                        listOfFingers.Add(new Finger
                        {
                            FormSl = value.PslNo,
                            FingerDate = value.FingerDate,
                            Status = value.Status,

                            PassNo = value.PassportNo,
                            Pname = value.Pname,

                        });
                    }

                    return listOfFingers.ToList();
                }
                return listOfFingers.ToList();
            }
            catch (Exception ex)
            {
                new CustomizeMessageSentToEmail().SentMail(ex);
                
            }
           return  listOfFingers.ToList();
        }


        public List<Finger> GetFingerClearList()
        {
            List<Finger> listOfFingers = new List<Finger>();

            if (IsConnection)
            {
                var finger = DbEntities.sp_ClearFinger().ToList();

                foreach (var value in finger)
                {

                    listOfFingers.Add(new Finger
                    {
                        FormSl = value.PslNo,
                        FingerDate = value.FingerDate,
                        Status = value.Status,

                        PassNo = value.PassportNo,
                        Pname = value.Pname,

                    });
                }

                return listOfFingers.ToList();
            }
            return listOfFingers.ToList();
        }
        public int Add(Finger Finger)
        {
            try
            {
                if (IsConnection)
                {
                    tbl_Finger tblFinger = new tbl_Finger();

                    if (tblFinger.Id == 0)
                    {

                        tblFinger.FormSl = Finger.FormSl;
                        tblFinger.FingerDate = Convert.ToString(Finger.FingerDate);
                        tblFinger.Status = "Clear";
                        tblFinger.UserId = Environment.UserName;

                        DbEntities.tbl_Finger.Add(tblFinger);
                        int count = DbEntities.SaveChanges();
                        return count;

                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                new CustomizeMessageSentToEmail().SentMail(ex);

                return 0;

            }
            
        }



        public bool IsExist(Finger finger)
        {
            try
            {
                if (IsConnection)
                {
                    // string pidCard = passport.PNationlIdCard.ToString();
                    int count = DbEntities.tbl_Finger.Count(a => a.FormSl == finger.FormSl);

                    if (count > 0)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                new CustomizeMessageSentToEmail().SentMail(ex);
                
            }
          
            return false;
        }
    }
}