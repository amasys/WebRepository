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
    
    public partial class tbl_TailoringMaster
    {
        public string ServiceMasterId { get; set; }
        public string voucherNo { get; set; }
        public string invoiceNo { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public string narration { get; set; }
        public string userId { get; set; }
        public string branchId { get; set; }
        public string suffixPrefixId { get; set; }
        public string customerName { get; set; }
        public string customerAdress { get; set; }
        public string phone { get; set; }
        public Nullable<System.DateTime> deliveryDate { get; set; }
        public Nullable<bool> delivered { get; set; }
        public Nullable<decimal> discount { get; set; }
    }
}
