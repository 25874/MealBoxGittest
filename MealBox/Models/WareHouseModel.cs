using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MealBox.Models
{
    public class WareHouseModel
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
        public int WarHouseIdFk { get; set;}
        public int ProductId { get; set; }
        public List<WareHouse> WareHousesList { get; set;}
        public int Type { get; set; }

        public int WareHouseInventoryId { get; set; }
    }

    public class WareHouse 
    {
        public string WarHouseName { get; set; }
        public string CityName { get; set; }
        public int WarHouseId { get; set; }
        public string AreaLength { get; set; }
    }

}