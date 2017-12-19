using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PanArabInternationalApp.DataAccess.Gatway;
using PanArabInternationalApp.EmailConfig;
using PanArabInternationalApp.Interface;
using PanArabInternationalApp.Models;

namespace PanArabInternationalApp.DataAccess.DAL
{
    public class PassportGateway:Connection

    {
        public int Add(Passport passport)
        {
            if (IsConnection)
            {
                tbl_Passport tblPassport = new tbl_Passport();

                if (tblPassport.Id == 0)
                {
                    tblPassport.PslNo = passport.Formsl;
                    tblPassport.PassportNo = passport.PassNo;
                    tblPassport.PassportMakeBy = passport.PpMakeBy;
                    tblPassport.PassportSubmitDate = passport.PsubmitDate;
                    tblPassport.PassportDeleveryDate = Convert.ToDateTime(passport.PpDeleveryDate);
                    tblPassport.UserId = Environment.UserName;
                    tblPassport.Remarks = passport.Remarks;
                    tblPassport.PSlipNo = passport.Pslipno;
                    tblPassport.PassportExpireDate= passport.PassportExpireDate;

                    DbEntities.tbl_Passport.Add(tblPassport);
                    
                    int count = DbEntities.SaveChanges();
                    
                    return count;
                }
            }
            return 0;
        }



        public bool IsExist(Passport passport)
        {
          
            if (IsConnection)
            {
               // string pidCard = passport.PNationlIdCard.ToString();
             int count = DbEntities.tbl_Passport.Count(a => a.PassportNo == passport.PassNo && a.PslNo==passport.Formsl);

                if (count > 0)
                {
                    return true;
                }
            }
            return false;
        }
        public void Remove(Passport passport)
        {
           
        }


        public List<string> GetList()
        {


            return DbEntities.viewExistPassportSlNoes.Select(a=>a.PslNo).ToList();
        }



        public IEnumerable<Passport> GetAllPassports()
        {
            if (IsConnection)
            {
                var passengerList = DbEntities.tbl_Passport.ToList();
                List<Passport> list = new List<Passport>();
                foreach (var value in passengerList)
                {

                    Passport passport = new Passport();
                    passport.Formsl = value.PslNo;
                    passport.PassNo = value.PassportNo;
                    passport.PpMakeBy = value.PassportMakeBy;
                    passport.PsubmitDate = Convert.ToDateTime(value.PassportSubmitDate);
                    passport.PpDeleveryDate = value.PassportDeleveryDate.ToString();
                    passport.Remarks = Convert.ToString(value.Remarks);
                    passport.Pslipno = Convert.ToString(value.PSlipNo);

                    list.Add(passport);


                }

                return list.ToList();

            }
            return new List<Passport>().ToList();
        }


        public void Edit(Passport passport)
        {
            
        }
        public int SaveLedger(Passenger passenger)
        {
            int ledger = 0;
            try
            {
                ledger = DbEntities.AccountLedgerAdd(passenger.PName, "49", false, Convert.ToDecimal(0.0), "Dr", null, passenger.PSlNo,
             null, null, null, null, null, 0, 0, "1", "14", false, "1", "passenger", "user1", "", false, "", "", "", "").Count();
                DbEntities.SaveChanges();

            }
            catch (Exception ex)
            {

                new CustomizeMessageSentToEmail().SentMail(ex);

            }
            return ledger;
        }

        public int JournalMasterAdd(Passenger passenger)
        {
            int jmaster = 0;
            //string  masterJournalId = DbEntities.tbl_JournelMaster.Max(a =>a.JournalMasterId);
            try
            {
                jmaster = DbEntities.JournelMasterAdd("", DateTime.Today, passenger.PDescription, false, passenger.userid, "1", "", "", "");
                DbEntities.SaveChanges();
                return jmaster;
            }
            catch (Exception ex)
            {
                new CustomizeMessageSentToEmail().SentMail(ex);
                return jmaster;

            }

        }

        public int JournalDetialsAdd(Passenger passenger)
        {
            int jdetailsdr = 0;
            try
            {
                DbEntities = new PANARAB_dbEntities();

                //  string masterJournalId = DbEntities.tbl_JournelMaster.Max(a =>a.JournalMasterId);
                // string masterLedgr = DbEntities.tbl_AccountLedger.Max(a =>a.ledgerId);

                int jdetailscr = DbEntities.JournelDetailsAddPassenger("", Convert.ToDecimal(passenger.ContractAmmount), Convert.ToDecimal(0.00), "", "", "");
                jdetailsdr = DbEntities.JournelDetailsAdd("", "45", Convert.ToDecimal(0.00), Convert.ToDecimal(passenger.ContractAmmount), "", "", "");

                DbEntities.SaveChanges();


            }
            catch (Exception ex)
            {

                new CustomizeMessageSentToEmail().SentMail(ex);
            }

            return jdetailsdr;
        }

        public void JournalPostingAddDr(Passenger passenger, string masterJournalId, string masterLedgr)
        {
            //changing 45 
            try
            {
                DbEntities.LedgerPostingAdd(DateTime.Today, "Journal Voucher", masterJournalId, masterLedgr, Convert.ToDecimal(passenger.ContractAmmount), Convert.ToDecimal(0.0), false, "1", "45", null);

            }
            catch (Exception ex)
            {

                new CustomizeMessageSentToEmail().SentMail(ex);
            }


        }
        public void JournalPostingAddCr(Passenger passenger, string masterJournalId, string masterLedgr)
        {
            //changing 45 
            try
            {
                DbEntities.LedgerPostingAdd(DateTime.Today, "Journal Voucher", masterJournalId, "45", Convert.ToDecimal(0.0), Convert.ToDecimal(passenger.ContractAmmount), false, "1", masterLedgr, null);


            }
            catch (Exception ex)
            {

                new CustomizeMessageSentToEmail().SentMail(ex);
            }

        }
    }
}