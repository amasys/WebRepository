using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PanArabInternationalApp.DataAccess.Gatway;
using PanArabInternationalApp.EmailConfig;
using PanArabInternationalApp.Interface;
using PanArabInternationalApp.Models;

namespace PanArabInternationalApp.DataAccess.DAL
{
    public class PassengerGatway : Connection
    {

        public int Add(Passenger passenger)
        {
            try
            {
                if (IsConnection)
                {
                    tbl_Passenger tblPassenger = new tbl_Passenger();

                    if (tblPassenger.Id == 0)
                    {
                        tblPassenger.PslNo = passenger.PSlNo;
                        tblPassenger.Pname = passenger.PName;
                        tblPassenger.PresentAddress = passenger.PPressentAddress;
                        tblPassenger.PNationalIdCard = passenger.PNationlIdCard.ToString();
                        tblPassenger.PDescription = passenger.PDescription;
                        tblPassenger.PContactAmount = Convert.ToInt32(passenger.ContractAmmount);
                        tblPassenger.PHusband = passenger.PHusband;
                        tblPassenger.PGender = passenger.PGender;
                        tblPassenger.PfatherName = passenger.PFatherName;
                        tblPassenger.PmotherName = passenger.PMotherName;
                        tblPassenger.Pdistrict = passenger.Pdisctrict;
                        tblPassenger.DOB = Convert.ToDateTime(passenger.DOB);
                        tblPassenger.PermanentAddress = passenger.PpermanentAddress;
                        tblPassenger.PostOffice = passenger.PostOffice;
                        tblPassenger.UserId = Environment.UserName;
                        tblPassenger.PContactNo = passenger.Pcontactno;
                        tblPassenger.Date = DateTime.Today.Date;
                        tblPassenger.Ispassport = passenger.IsPassport;

                        //  tblPassenger.ContractType=passenger.ContractType;


                      

                      
                            int saveLedger = SaveLedger(passenger);
                            if (saveLedger > 0)
                            {


                                int master = JournalMasterAdd(passenger);
                                
                                if (master > 0)
                                {
                                    int detials = JournalDetialsAdd(passenger,master);
                                    if (detials > 0)
                                    {
                                       // string masterJournalId = DbEntities.tbl_JournelMaster.Max(a => a.JournalMasterId);
                                        string masterJournalId = master.ToString();
                                        string masterLedgr = DbEntities.tbl_AccountLedger.FirstOrDefault(a => a.name == passenger.PSlNo).ledgerId;

                                        JournalPostingAddDr(passenger, masterJournalId, masterLedgr);

                                        JournalPostingAddCr(passenger, masterJournalId, masterLedgr);
                                      
                                    }
                                }

                                tblPassenger.voucherNo = master;

                                DbEntities.tbl_Passenger.Add(tblPassenger);
                                int passadd = DbEntities.SaveChanges();
                                return passadd;
                            }

                          

                      
                    }



                }
            }
            catch (Exception ex)
            {

                new CustomizeMessageSentToEmail().SentMail(ex);

            }

            return 0;
        }

        public void Remove(Passenger passenger)
        {

        }
        CultureInfo provider = CultureInfo.InvariantCulture;
        public void Edit(Passenger passenger)
        {
            try
            {
                tbl_Passenger passengerList = DbEntities.tbl_Passenger.SingleOrDefault(a => a.PslNo == passenger.PSlNo);

                if (passengerList != null)
                {

                    passengerList.Pname = passenger.PName;
                    passengerList.DOB = passenger.DOB;

                    passengerList.PNationalIdCard = passenger.PNationlIdCard.ToString();
                    passengerList.PContactNo = passenger.Pcontactno;
                    passengerList.PermanentAddress = passenger.PpermanentAddress;
                    passengerList.PresentAddress = passenger.PPressentAddress;
                    passengerList.PContactNo = passengerList.PContactNo;
                    passengerList.Ispassport = passenger.IsPassport;

                    DbEntities.SaveChanges();

                }

            }
            catch (Exception ex)
            {

                new CustomizeMessageSentToEmail().SentMail(ex);
            }


        }

        public IEnumerable<Passenger> GetAllPassengers()
        {
            if (IsConnection)
            {
                var passengerList = DbEntities.ViewGetAllPassengers.ToList();
                List<Passenger> list = new List<Passenger>();
                foreach (var value in passengerList)
                {
                    Passenger passenger = new Passenger();

                    passenger.PSlNo = value.PslNo;
                    passenger.PName = value.Pname;
                    passenger.PPressentAddress = value.PresentAddress;
                    passenger.PNationlIdCard = Convert.ToInt64(value.PNationalIdCard);
                    passenger.PDescription = value.PDescription;
                    passenger.ContractAmmount = Convert.ToString(value.PContactAmount);
                    passenger.PHusband = value.PHusband;
                    passenger.PGender = value.PGender;
                    passenger.PFatherName = value.PfatherName;
                    passenger.PMotherName = value.PmotherName;
                    passenger.Pdisctrict = value.District;
                    string date = Convert.ToDateTime(value.DOB).ToString("dd-MMM-yyyy");

                    passenger.DOB = Convert.ToDateTime(date);
                    passenger.PpermanentAddress = value.PermanentAddress;
                    passenger.PDescription = value.PDescription;
                    passenger.Date = value.Date;
                    passenger.PostOffice = value.PostOffice;
                    
                    passenger.Pcontactno = value.PContactNo;



                    list.Add(passenger);

                }

                return list.ToList();

            }
            return new List<Passenger>().ToList();
        }

        public bool IsExist(Passenger passenger)
        {
            try
            {
                if (IsConnection)
                {
                    string pidCard = passenger.PNationlIdCard.ToString();
                    int count = DbEntities.tbl_Passenger.Count(a => a.PslNo == passenger.PSlNo || a.PNationalIdCard == pidCard);

                    if (count > 0)
                    {
                        Edit(passenger);
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
        public string Serial()
        {
            string sl = "";
            if (IsConnection)
            {
                int count = DbEntities.tbl_Passenger.DefaultIfEmpty().Max(p => p == null ? 0 : p.Id);

                if (count == 0)
                {
                    sl = "PSL-" + count;

                }
                else
                {
                    sl = "PSL-" + (count + 1);
                }

            }

            return sl;

        }


        public int SaveLedger(Passenger passenger)
        {
            int ledger=0;
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
            var   masterJournalId = DbEntities.tbl_JournelMaster.ToList();

            var max = masterJournalId.Max(a => Convert.ToInt32(a.JournalMasterId)+1).ToString();
            try
            {
                jmaster = DbEntities.JournelMasterAdd(max, DateTime.Today, passenger.PDescription, false, passenger.userid, "1", "", "","");
                DbEntities.SaveChanges();
                return Convert.ToInt32(max);
            }
            catch (Exception ex)
            {
                new CustomizeMessageSentToEmail().SentMail(ex);
                return jmaster;

            }
           
        }

        public int JournalDetialsAdd(Passenger passenger,int journalMaxId)
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


        public List<SelectListItem> GetAllDistrict()
        {
            List<SelectListItem> ListDistrict = new List<SelectListItem>();

            foreach (tbl_Disctrict disctrict in DbEntities.tbl_Disctrict.ToList())
            {
                ListDistrict.Add(new SelectListItem { Text = disctrict.District, Value = Convert.ToString(disctrict.Id) });

            }
            return ListDistrict;
        }
    }
}