using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PanArabInternationalApp.DataAccess.Bll.Manager;
using PanArabInternationalApp.DataAccess.Gatway;
using PanArabInternationalApp.EmailConfig;
using PanArabInternationalApp.Models;

namespace PanArabInternationalApp.DataAccess.DAL
{
    public class PoliceClearenceGateway:Connection
    {
        public int Add(PC_ConsoleLetter pcConsoleLetter)
        {
            if (IsConnection)
            {
                tbl_PC_ConsoleLetter tblPcConsoleLetter = new tbl_PC_ConsoleLetter();

                if (tblPcConsoleLetter.Id == 0)
                {

                    tblPcConsoleLetter.PslNo = pcConsoleLetter.FormSlNo;
                    tblPcConsoleLetter.ByOwnRemarks = pcConsoleLetter.Remarks;
                    tblPcConsoleLetter.CL_Status = pcConsoleLetter.ConsoleStatus;
                    tblPcConsoleLetter.ContactAmount = pcConsoleLetter.PcContactAmmount;
                    tblPcConsoleLetter.ControlLetterRemarks = pcConsoleLetter.Available;
                    tblPcConsoleLetter.PCDate = Convert.ToDateTime(pcConsoleLetter.ClearenceDate);
                    tblPcConsoleLetter.PC_Status = pcConsoleLetter.PcStatus;
                 
                    
                    if (pcConsoleLetter.UpoadFileName!=null)
                    {
                        tblPcConsoleLetter.UploadName = pcConsoleLetter.UpoadFileName.FileName;
                   
                    }
                   
               
                    if (pcConsoleLetter.PCtype == "by Agency" && pcConsoleLetter.PcContactAmmount>0)
                    {
                        Passenger passenger = new Passenger();

                        passenger.PSlNo = pcConsoleLetter.FormSlNo;
                        passenger.PName = pcConsoleLetter.PName;
                        passenger.ContractAmmount = pcConsoleLetter.PcContactAmmount.ToString();

                        int master = JournalMasterAdd(passenger);
                        if (master > 0)
                        {


                            int detials = JournalDetialsAdd(passenger, master);
                            if (detials > 0)
                            {
                                string masterJournalId = master.ToString();
                                string masterLedgr = DbEntities.tbl_AccountLedger.FirstOrDefault(a => a.name == passenger.PSlNo).ledgerId;

                                JournalPostingAddDr(passenger, masterJournalId, masterLedgr);

                                JournalPostingAddCr(passenger, masterJournalId, masterLedgr);
                              
                            }
                        }
                        tblPcConsoleLetter.voucherno = master.ToString();
                    }

                  

                }
            
                DbEntities.tbl_PC_ConsoleLetter.Add(tblPcConsoleLetter);
                int count = DbEntities.SaveChanges();
                return count;
            }
            return 0;
        }

        public void UpdatePoliceClearence(Passenger pcConsoleLetter)
        {
            tbl_PC_ConsoleLetter passengerList = DbEntities.tbl_PC_ConsoleLetter.SingleOrDefault(a => a.PslNo == pcConsoleLetter.PSlNo);

            if (passengerList != null)
            {

                if (pcConsoleLetter.pcConsoleLetter.PcContactAmmount>0)
                {
                    passengerList.ContactAmount = Convert.ToInt32(pcConsoleLetter.pcConsoleLetter.PcContactAmmount);
                    pcConsoleLetter.ContractAmmount = pcConsoleLetter.pcConsoleLetter.PcContactAmmount.ToString();

                    pcConsoleLetter.voucherno = Convert.ToInt32(passengerList.voucherno);

                    new AccountingManager().UpdatePassengerLedgerStatement(pcConsoleLetter);
                }

                DbEntities.SaveChanges();
            }
        }

        public bool IsExist(PC_ConsoleLetter pcConsoleLetter)
        {
            if (IsConnection)
            {
                // string pidCard = passport.PNationlIdCard.ToString();
                int count = DbEntities.tbl_PC_ConsoleLetter.Count(a => a.PslNo == pcConsoleLetter.FormSlNo);

                if (count > 0)
                {
                  

                        Passenger passenger = new Passenger();
                        passenger.pcConsoleLetter = pcConsoleLetter;
                        passenger.PSlNo = pcConsoleLetter.FormSlNo;
                        passenger.ContractAmmount = pcConsoleLetter.PcContactAmmount.ToString();

                        UpdatePoliceClearence(passenger);

                    
                    return true;
                }
            }
            return false;
        }

        public List<PC_ConsoleLetter> GetpcClearlist()
        {
            List<PC_ConsoleLetter> listOfCleardata = new List<PC_ConsoleLetter>();

            if (IsConnection)
            {
                var list = DbEntities.sp_ConsoleLetter().ToList();

                foreach (var value in list)
                {

                    listOfCleardata.Add(new PC_ConsoleLetter
                    {
                        FormSlNo = value.PslNo,
                        ClearenceDate = Convert.ToDateTime(Convert.ToDateTime(value.PCDate).ToString("dd-MMM-yy")),
                        PName = value.Pname,
                        PcStatus = value.PC_Status,
                        ConsoleStatus = value.CL_Status,
                        ConsoleRemarks = value.ControlLetterRemarks,
                        PassportNo = value.PassportNo,
                        PcContactAmmount = Convert.ToDecimal(value.ContactAmount)
                        
                    });
                }

                return listOfCleardata.ToList();
            }
            return listOfCleardata.ToList();
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
                jdetailsdr = DbEntities.JournelDetailsAdd(journalMaxId.ToString(), "45", Convert.ToDecimal(0.00), Convert.ToDecimal(passenger.ContractAmmount), "", "", "");

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