//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PanArabInternationalApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_TaxMaster
    {
        public string taxId { get; set; }
        public string taxName { get; set; }
        public Nullable<decimal> rate { get; set; }
        public string calculatingMode { get; set; }
        public Nullable<bool> active { get; set; }
        public string narration { get; set; }
        public string branchId { get; set; }
        public Nullable<System.DateTime> extraDate { get; set; }
        public string extra1 { get; set; }
        public string extra2 { get; set; }
    }
}
