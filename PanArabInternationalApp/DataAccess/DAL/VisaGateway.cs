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
                        string masterJournalMaxId = new AccountingManager().SaveJournal(passenger);
                        tblVisaProcessing.ContractAmount = Convert.ToInt32(visa.ContractAmount);
                        tblVisaProcessing.voucherno = masterJournalMaxId;

                    }

                }

                tblVisaProcessing.VisaStapmingDate = visa.VisaStapDate;
                DbEntities.tbl_VisaProcessing.Add(tblVisaProcessing);

                int count = DbEntities.SaveChanges();

                return count;
            }
            return 0;
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

                if (visaPassenger.visa.Category== "Driver" && Convert.ToInt32(visaPassenger.ContractAmmount) > 0)
                {
                    passengerList.ContractAmount = Convert.ToInt32(visaPassenger.ContractAmmount);

                    passengerList.ContractAmount = Convert.ToInt32(visaPassenger.visa.ContractAmount);
                    visaPassenger.ContractAmmount = visaPassenger.visa.ContractAmount;

                    var existingVoucher = passengerList.voucherno;
                    if (existingVoucher == null)
                    {

                        string voucherNoNewCreate = new AccountingManager().SaveJournal(visaPassenger);

                        passengerList.voucherno = voucherNoNewCreate;

                    }
                    else
                    {
                        visaPassenger.voucherno = Convert.ToInt32(passengerList.voucherno);

                        new AccountingManager().UpdatePassengerLedgerStatement(visaPassenger);

                    }

                }
                else
                {
                    passengerList.ContractAmount = 0;
                    passengerList.voucherno = null;
                    passengerList.Cateogry = visaPassenger.visa.Category;
                }
                DbEntities.Entry(passengerList).State=EntityState.Modified;   
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