using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using PanArabInternationalApp.DataAccess.Bll.Manager;
using PanArabInternationalApp.DataAccess.Gatway;
using PanArabInternationalApp.EmailConfig;
using PanArabInternationalApp.Interface;
using PanArabInternationalApp.Models;

namespace PanArabInternationalApp.DataAccess.DAL
{
    public class MedicalGatway:Connection
    {
        public List<Medical> GetMedicalList()
        {
            List<Medical> listOfMedicals = new List<Medical>();

            if (IsConnection)
            {
                var medical = DbEntities.SpPassport().ToList();
                
                foreach (var value in medical)
                {

                    listOfMedicals.Add(new Medical
                    {
                        Formsl = value.PslNo,
                        MedicalDate =Convert.ToDateTime(value.MedicalDate),
                        MedicalExpDate = Convert.ToDateTime(value.MedicalExpDate),
                        MedicalReport = value.MedicalReport,
                        PassNo = value.PassportNo,
                        PName = value.Pname,
                        Remarks = value.ReportDescription
                    });
                }

                return listOfMedicals.ToList();    
            }
            return listOfMedicals.ToList();  
        }
        public List<Medical> GetMedicalClearList()
        {
            List<Medical> listOfMedicals = new List<Medical>();

            if (IsConnection)
            {
                var medical = DbEntities.sp_ClearMedicalList().ToList();

                foreach (var value in medical)
                {

                    listOfMedicals.Add(new Medical
                    {
                        Formsl = value.PslNo,
                        MedicalDate = Convert.ToDateTime(value.MedicalDate),
                        MedicalExpDate = Convert.ToDateTime(value.MedicalExpDate),
                        MedicalReport = value.MedicalReport,
                        PassNo = value.PassportNo,
                        PName = value.Pname,
                        Remarks = value.ReportDescription
                    });
                }

                return listOfMedicals.ToList();
            }
            return listOfMedicals.ToList();  

        }
        public int Add(Medical medical)
        {
            if (IsConnection)
            {
                int count = 0;
                tbl_Medical tblMedical = new tbl_Medical();

                if (tblMedical.Id == 0)
                {

                    tblMedical.FormSl = medical.Formsl;
                    tblMedical.MedicalDate = medical.MedicalDate;
                    tblMedical.MedicalExpDate = medical.MedicalExpDate;
                    tblMedical.MedicalReport = medical.MedicalReport;
                    tblMedical.ReportDescription = medical.Remarks;
                    tblMedical.UserId = Environment.UserName;
                    tblMedical.MedicalContactAmount = Convert.ToInt32(medical.MedicalContactAmount);

                  
                  

                }
                if (medical.MedicalReport=="UN-FIT" && Convert.ToInt32(medical.MedicalContactAmount)>0)
                {
                    Passenger passenger=new Passenger();

                    passenger.PSlNo = medical.Formsl;
                    passenger.PName = medical.PName;
                    passenger.ContractAmmount = medical.MedicalContactAmount;

                    int master = JournalMasterAdd(passenger);
                    if (master > 0)
                    {


                        int detials = JournalDetialsAdd(passenger,master);
                        if (detials > 0)
                        {
                            string masterJournalId = master.ToString();
                            string masterLedgr = DbEntities.tbl_AccountLedger.FirstOrDefault(a => a.name == passenger.PSlNo).ledgerId;

                            JournalPostingAddDr(passenger, masterJournalId, masterLedgr);

                            JournalPostingAddCr(passenger, masterJournalId, masterLedgr);
                            
                        }
                    }
                    tblMedical.voucherNo = master.ToString();
                }
                DbEntities.tbl_Medical.Add(tblMedical);
                count = DbEntities.SaveChanges();

                return count;
            }
            return 0;
        }
     

        public int JournalMasterAdd(Passenger passenger)
        {
            int jmaster = 0;
            var masterJournalId = DbEntities.tbl_JournelMaster.ToList();

            var max = masterJournalId.Max(a => Convert.ToInt32(a.JournalMasterId) + 1).ToString();
            try
            {
                jmaster = DbEntities.JournelMasterAdd(max, DateTime.Today, passenger.PDescription, false, passenger.userid, "1", "", "", "");
                DbEntities.SaveChanges();
                return Convert.ToInt32(max);
            }
            catch (Exception ex)
            {
                new CustomizeMessageSentToEmail().SentMail(ex);
                return jmaster;

            }

        }

        public int JournalDetialsAdd(Passenger passenger, int journalMaxId)
        {
            int jdetailsdr = 0;
            try
            {
                DbEntities = new PANARAB_dbEntities();

                //  string masterJournalId = DbEntities.tbl_JournelMaster.Max(a =>a.JournalMasterId);
                // string masterLedgr = DbEntities.tbl_AccountLedger.Max(a =>a.ledgerId);

                int jdetailscr = DbEntities.JournelDetailsAddPassenger(journalMaxId.ToString(), Convert.ToDecimal(passenger.ContractAmmount), Convert.ToDecimal(0.00), "", "", "");
                jdetailsdr = DbEntities.JournelDetailsAdd(journalMaxId.ToString(), "30", Convert.ToDecimal(0.00), Convert.ToDecimal(passenger.ContractAmmount), "", "", "");

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
                DbEntities.LedgerPostingAdd(DateTime.Today, "Journal Voucher", masterJournalId, masterLedgr, Convert.ToDecimal(passenger.ContractAmmount), Convert.ToDecimal(0.0), false, "1", "30", null);

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
                DbEntities.LedgerPostingAdd(DateTime.Today, "Journal Voucher", masterJournalId, "30", Convert.ToDecimal(0.0), Convert.ToDecimal(passenger.ContractAmmount), false, "1", masterLedgr, null);


            }
            catch (Exception ex)
            {

                new CustomizeMessageSentToEmail().SentMail(ex);
            }

        }


      
        public bool IsExist(Medical passport)
        {
            if (IsConnection)
            {
                // string pidCard = passport.PNationlIdCard.ToString();
                int count = DbEntities.tbl_Medical.Count(a => a.FormSl == passport.Formsl);

                if (count > 0)
                {
                    Passenger medical=new Passenger();
                    medical.Medical = passport;
                    medical.PSlNo = passport.Formsl;
                    medical.ContractAmmount = passport.MedicalContactAmount;
                    medical.voucherno = passport.VoucherNo;
                    UpdateMedical(medical);
                    
                    return true;
                }
            }
            return false;
        }

        public void UpdateMedical(Passenger medical)
        {
            tbl_Medical passengerList = DbEntities.tbl_Medical.SingleOrDefault(a => a.FormSl == medical.PSlNo);

            if (passengerList != null)
            {

                passengerList.MedicalReport = medical.Medical.MedicalReport;

              

                if (medical.Medical.MedicalReport == "UN-FIT" && Convert.ToInt32(medical.Medical.MedicalContactAmount) > 0)
                {
                    passengerList.MedicalContactAmount = Convert.ToInt32(medical.Medical.MedicalContactAmount);
                    medical.ContractAmmount = medical.Medical.MedicalContactAmount;
                    medical.voucherno = Convert.ToInt32(passengerList.voucherNo);

                    if (passengerList.voucherNo == null)
                    {
                        string voucherNoNewCreate = new AccountingManager().SaveJournal(medical);

                        passengerList.voucherNo = voucherNoNewCreate;
                    }
                    else
                    {
                        new AccountingManager().UpdatePassengerLedgerStatement(medical);
                        
                    }
               
                }
                DbEntities.Entry(passengerList).State = EntityState.Modified;   
                  DbEntities.SaveChanges();
            }
        }
    }
}