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
    
    public partial class tbl_SalesPromotionMaster
    {
        public string salesPromotionMasterId { get; set; }
        public string salesPromotionName { get; set; }
        public Nullable<System.DateTime> fromDate { get; set; }
        public Nullable<System.DateTime> toDate { get; set; }
        public string narration { get; set; }
        public Nullable<bool> active { get; set; }
        public string branchId { get; set; }
        public Nullable<System.DateTime> extraDate { get; set; }
        public string extra1 { get; set; }
        public string extra2 { get; set; }
    }
}
