//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MealBoxCloud
{
    using System;
    using System.Collections.Generic;
    
    public partial class stockOut
    {
        public int StockOutID { get; set; }
        public string ItmNme { get; set; }
        public Nullable<int> StockQty { get; set; }
        public string Units { get; set; }
        public Nullable<double> weight { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<double> unit_price { get; set; }
        public Nullable<double> amount { get; set; }
        public string created_by { get; set; }
        public Nullable<System.DateTime> create_at { get; set; }
        public Nullable<int> StockRef { get; set; }
        public string Type { get; set; }
    }
}
