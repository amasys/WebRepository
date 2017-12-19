using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PanArabInternationalApp.Models
{
    public class Medical
    {
        [Required(AllowEmptyStrings = true)]
        public string Formsl { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string PassNo { get; set; }
        public string PName { get; set; }

        [Required(AllowEmptyStrings = true)]
        public DateTime MedicalDate { get; set; }
        [Required(AllowEmptyStrings = true)]
        public string MedicalReport { get; set; }

        [Required(AllowEmptyStrings = true)]
        public DateTime MedicalExpDate { get; set; }
       
        [Required(AllowEmptyStrings = true)]
        public string Remarks { get; set; }

        public string MedicalContactAmount { get; set; }
    }
}