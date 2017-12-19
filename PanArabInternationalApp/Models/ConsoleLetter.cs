using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PanArabInternationalApp.Models
{
    public class ConsoleLetter
    {
      
        [Required]
        public DateTime ConsoleLetterDateTime { get; set; }

        [Required]
        public string Available { get; set; }


        public HttpPostedFileBase UpoadFileName { get; set; }
       
        public string ConsoleRemarks { get; set; }
        
        public string ConsoleStatus { get; set; }
        


    }
}