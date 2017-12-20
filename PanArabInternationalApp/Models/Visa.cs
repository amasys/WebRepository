using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PanArabInternationalApp.Models
{
    public class Visa
    {
        [Required]

        public string Category { get; set; }
        public string FormSl { get; set; }
        public string VisaNo { get; set; }
        public DateTime IssueDate { get; set; }
        public string IssuePlace { get; set; }
        public bool Okalyee { get; set; }
        public string SponsorName { get; set; }
        public string SponMobileNo { get; set; }
        public string VisaReff { get; set; }
        public string PassengerReff { get; set; }
        public DateTime VisaStapDate { get; set; }
        public string PassgaMobileNo { get; set; }
        public string PassNo { get; set; }
        public string Status { get; set; }
        public string Pname { get; set; }

        public List<SelectListItem> VisaCategory { get; set; }
        public string SponcerId { get; set; }
        public DateTime OkalyeeDate { get; set; }
        public string OkalyeeRemarks { get; set; }
        public DateTime MusaniDate { get; set; }
        public string MusaniRemarks { get; set; }
        public string ContractAmount { get; set; }
        public string DrivingLicenceNo { get; set; }
        public String OkelaNo { get; set; }

        public int VoucherNo { get; set; }
    }
}