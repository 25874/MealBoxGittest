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
    
    public partial class tbl_WareHouse
    {
        public int WarHouseId { get; set; }
        public string WarHouseName { get; set; }
        public string WareHouseCode { get; set; }
        public Nullable<int> CityID { get; set; }
        public string Description { get; set; }
        public string AreaLength { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreateAt { get; set; }
        public Nullable<int> UpdatedBY { get; set; }
        public string UpdatedAt { get; set; }
    }
}
