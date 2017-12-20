using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PanArabInternationalApp.Models
{
    public class PC_ConsoleLetter:ConsoleLetter
    {
        public string FormSlNo { get; set; }
        public string PName { get; set; }
        public string PCtype { get; set; }
        
        
        [Required]
        public DateTime ClearenceDate { get; set; }

        public string PassportNo { get; set; }
        [Required]
        public string PcCategory { get; set; }
        [Required]
        public decimal? PcContactAmmount { get; set; }
        [Required]
        public string PcStatus { get; set; }

        [Required]
        public string Remarks { get; set; }


        public int VoucherNo { get; set; }

       



    }
}