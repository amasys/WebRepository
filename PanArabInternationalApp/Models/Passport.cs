using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PanArabInternationalApp.Models
{
    public class Passport
    {   

        [Required(AllowEmptyStrings = true)]
        public string Formsl { get; set; }
        [Required(AllowEmptyStrings = true)]
        public string PassNo { get; set; }
        [Required(AllowEmptyStrings = true)]
        public string Pslipno { get; set; }
        [Required(AllowEmptyStrings = true)]
        public DateTime PsubmitDate { get; set; }
        [Required(AllowEmptyStrings = true)]
        public string PpMakeBy { get; set; }
        [Required(AllowEmptyStrings = true)]
        public string PpDeleveryDate { get; set; }

         [Required(AllowEmptyStrings = true)]
        public string Remarks { get; set; }



         [Required(AllowEmptyStrings = true)]
         public DateTime PassportExpireDate { get; set; }
    }
}