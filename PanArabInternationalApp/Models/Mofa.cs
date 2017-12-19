using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PanArabInternationalApp.Models
{
    public class Mofa
    {
        [Required(AllowEmptyStrings = true)]
        public string Formsl { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string PassNo { get; set; }
        public string PName { get; set; }

        [Required(AllowEmptyStrings = true)]
        public DateTime MofaDate { get; set; }
        [Required(AllowEmptyStrings = true)]
        public string MofaNumber { get; set; }

        
    }
}