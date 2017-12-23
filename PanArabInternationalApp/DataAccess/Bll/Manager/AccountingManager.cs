using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PanArabInternationalApp.DataAccess.Gatway;
using PanArabInternationalApp.EmailConfig;
using PanArabInternationalApp.Models;

namespace PanArabInternationalApp.DataAccess.Bll.Manager
{
    public class AccountingManager:Connection
    {
        public void UpdatePassengerLedgerStatement(Passenger passenger)
        {
            if (IsConnection)
            {
                AccountLedgerPostingupdate(DbEntities,passenger);
                
            }
        }

        private void AccountLedgerPostingupdate(PANARAB_dbEntities dbEntities, Passenger passenger)
        {

            dbEntities.Sp_LedgerAndJournalPostingUpdateDrCr(passenger.voucherno.ToString(),passenger.ContractAmmount,passenger.ContractAmmount,"cr","");

            dbEntities.Sp_LedgerAndJournalPostingUpdateDrCr(passenger.voucherno.ToString(),passenger.ContractAmmount,passenger.ContractAmmount,"dr","");
        }

        public string SaveJournal(Passenger passenger)
        {
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
            return master.ToString();
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