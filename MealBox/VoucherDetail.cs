//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MealBox
{
    using System;
    using System.Collections.Generic;
    
    public partial class VoucherDetail
    {
        public int VoucherDetailId { get; set; }
        public Nullable<int> VoucherId { get; set; }
        public string AccountCode { get; set; }
        public Nullable<double> DebitAmount { get; set; }
        public Nullable<double> CreditAmount { get; set; }
    }
}