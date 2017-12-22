using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PanArabInternationalApp.Models
{
    public class Passenger
    {
        public string userid { get; set; }

        [Required(AllowEmptyStrings = true)]

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DOB { get; set; }

        [Required(AllowEmptyStrings = true)]

        public string PName { get; set; }
       [Required(AllowEmptyStrings = true)]

        public string PSlNo { get; set; }
         [Required(AllowEmptyStrings = true)]

        public string PFatherName { get; set; }
         [Required(AllowEmptyStrings = true)]

        public string PMotherName { get; set; }
         [Required(AllowEmptyStrings = true)]

        public string PHusband { get; set; }
         [Required(AllowEmptyStrings = true)]

        public string PGender { get; set; }
         [Required(AllowEmptyStrings = true)]
         [Range(14, Int64.MaxValue, ErrorMessage = "Maximum 14 Number ")]
        public Int64 PNationlIdCard { get; set; }

         [Required(AllowEmptyStrings = true)]

        public string PpermanentAddress { get; set; }

         [Required(AllowEmptyStrings = true)]
         public string PPressentAddress { get; set; }
         [Required(AllowEmptyStrings = true)]

        public string PostOffice { get; set; }
         [Required(AllowEmptyStrings = true)]

        public string Pdisctrict { get; set; }
         [Required(AllowEmptyStrings = true)]
         public string Pcontactno { get; set; }

         [Required(AllowEmptyStrings = true)]
         public string PDescription { get; set; }


         [Required(AllowEmptyStrings = true)]
         public string ContractAmmount { get; set; }

        public DateTime? Date { get; set; }


         [Required(AllowEmptyStrings = true)]
        public bool ContractType { get; set; }
         [Required(AllowEmptyStrings = true)]
         public bool IsPassport { get; set; }

         public List<SelectListItem> ListDistrict { get; set; }


         public Medical Medical { get; set; }
         public PC_ConsoleLetter pcConsoleLetter { get; set; }
         public Visa visa { get; set; }

         public int voucherno { get; set; }
    }
}