using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PanArabInternationalApp.DataAccess.Bll.Manager;
using PanArabInternationalApp.DataAccess.Gatway;
using PanArabInternationalApp.EmailConfig;
using PanArabInternationalApp.Models;

namespace PanArabInternationalApp.DataAccess.DAL
{
    public class VisaGateway : Connection
    {
        public int Add(Visa visa)
        {
            if (IsConnection)
            {
                tbl_VisaProcessing tblVisaProcessing = new tbl_VisaProcessing();

                if (tblVisaProcessing.Id == 0)
                {
                    tblVisaProcessing.FormSl = visa.FormSl;
                    tblVisaProcessing.Cateogry = visa.Category;
                    tblVisaProcessing.VisaNo = visa.VisaNo;
                    tblVisaProcessing.IssueDate = Convert.ToDateTime(visa.IssueDate);

                    tblVisaProcessing.UserId = Environment.UserName;
                    tblVisaProcessing.IssuePlace = visa.IssuePlace;
                    tblVisaProcessing.VisaReffNo = visa.VisaReff;

                    tblVisaProcessing.OkeylaNo = visa.OkelaNo;
                    tblVisaProcessing.OkeylaDate = visa.OkalyeeDate;
                    tblVisaProcessing.OkeylaRemarks = visa.OkalyeeRemarks;

                    tblVisaProcessing.MusaniDate = visa.MusaniDate;
                    tblVisaProcessing.MusaniRemarks = visa.MusaniRemarks;

                    tblVisaProcessing.SponcerName = visa.SponsorName;
                    tblVisaProcessing.SponserId = visa.SponcerId;

                    tblVisaProcessing.DrivingLicnc = visa.DrivingLicenceNo;
                    tblVisaProcessing.ContractAmount = Convert.ToInt32(visa.ContractAmount);


              

                    if (visa.Category == "Driver" && Convert.ToInt32(visa.ContractAmount) > 0)
                    {
                        Passenger passenger = new Passenger();

                        passenger.PSlNo = visa.FormSl;
                        passenger.PName = visa.Pname;
                        passenger.ContractAmmount = visa.ContractAmount;

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
                        tblVisaProcessing.voucherno = master.ToString();
                    }

                }

                tblVisaProcessing.VisaStapmingDate = visa.VisaStapDate;
                DbEntities.tbl_VisaProcessing.Add(tblVisaProcessing);

                int count = DbEntities.SaveChanges();

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

        public bool IsExist(Visa visa)
        {
            if (IsConnection)
            {
                // string pidCard = passport.PNationlIdCard.ToString();
                int count = DbEntities.tbl_VisaProcessing.Count(a => a.FormSl == visa.FormSl);


                if (count > 0)
                {
                    VisaUpdate(visa);

                    return true;
                }
            }
            return false;
        }
        public void Remove(Passport passport)
        {

        }

        public IEnumerable<Visa> GetAllVisaProcessing()
        {
            if (IsConnection)
            {
                var visallist = DbEntities.sp_VisaProcessing().ToList();
                List<Visa> list = new List<Visa>();
                foreach (var value in visallist)
                {

                    Visa visa = new Visa();
                    visa.FormSl = value.PslNo;
                    visa.PassNo = value.PassportNo;
                    visa.Pname = value.Pname;
                    visa.Category = value.Cateogry;
                    visa.VisaNo = value.VisaNo;
                    visa.IssueDate = Convert.ToDateTime(value.IssueDate);
                    visa.IssuePlace = Convert.ToString(value.IssuePlace);
                    visa.Status = Convert.ToString(value.Status);
                    visa.Okalyee = Convert.ToBoolean(value.Okela);


                    list.Add(visa);


                }

                return list.ToList();

            }
            return new List<Visa>().ToList();
        }
        public IEnumerable<Visa> GetAllVisaClearencelist()
        {
            if (IsConnection)
            {
                var visallist = DbEntities.sp_ClearVisaProcessing().ToList();
                List<Visa> list = new List<Visa>();
                foreach (var value in visallist)
                {

                    Visa visa = new Visa();
                    visa.FormSl = value.PslNo;
                    visa.PassNo = value.PassportNo;
                    visa.Pname = value.Pname;
                    visa.Category = value.Cateogry;
                    visa.VisaNo = value.VisaNo;
                    visa.IssueDate = Convert.ToDateTime(value.IssueDate);
                    visa.IssuePlace = Convert.ToString(value.IssuePlace);
                    visa.Status = Convert.ToString(value.Status);
                    visa.Okalyee = Convert.ToBoolean(value.Okela);

                    list.Add(visa);


                }

                return list.ToList();

            }
            return new List<Visa>().ToList();
        }

        public void VisaUpdate(Visa visa)
        {
            Passenger medical = new Passenger();
            medical.visa = visa;
            medical.PSlNo = visa.FormSl;
            medical.ContractAmmount = visa.ContractAmount;
           
            UpdateVisa(medical);
        }

        private void UpdateVisa(Passenger visaPassenger)
        {
            tbl_VisaProcessing passengerList = DbEntities.tbl_VisaProcessing.SingleOrDefault(a => a.FormSl == visaPassenger.PSlNo);

            if (passengerList != null)
            {





                if (passengerList.Cateogry == "Driver" && Convert.ToInt32(visaPassenger.ContractAmmount) > 0)
                {
                    passengerList.ContractAmount = Convert.ToInt32(visaPassenger.ContractAmmount);

                    passengerList.ContractAmount = Convert.ToInt32(visaPassenger.visa.ContractAmount);
                    visaPassenger.ContractAmmount = visaPassenger.visa.ContractAmount;
                    visaPassenger.voucherno = Convert.ToInt32(passengerList.voucherno);

                    new AccountingManager().UpdatePassengerLedgerStatement(visaPassenger);
                }

                DbEntities.SaveChanges();
            }
        }


        public List<SelectListItem> GetAllVisaCate()
        {
            List<SelectListItem> ListOfVisa = new List<SelectListItem>();

            Dictionary<int, string> visaList = new Dictionary<int, string>();
            visaList.Add(0, "House Worker");
            visaList.Add(1, "House Driver");
            visaList.Add(2, "Driver");



            foreach (KeyValuePair<int, string> disctrict in visaList.ToList())
            {
                ListOfVisa.Add(new SelectListItem { Text = disctrict.Value, Value = Convert.ToString(disctrict.Key) });

            }
            return ListOfVisa;
        }






        public void Edit(Passport passport)
        {

        }
    }
}