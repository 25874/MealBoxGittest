using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MealBoxCloud.Models
{
    public class WareHouseModel
    {
        public Nullable<int> WarHouseId { get; set; }
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

        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public Nullable<int> AreaLenght { get; set; }
        public int EditId { get; set; }
        public int WareHouseId { get; set; }
        public List<WareHouse> WareHousesList { get; set;}
        public int Type { get; set; }
        public int WareHouseInventoryId { get; set; }

        public List<WareHouseInv> WareHousesInvList { get; set; }
    }

    public class WareHouse 
    {
        public string WarHouseName { get; set; }
        public string CityName { get; set; }
        public int WarHouseId { get; set; }

        public string AreaLength { get; set; }
    }

    public class WareHouseInv
    {
        public string WarHouseName { get; set; }
        public int ProductId { get; set; }
        public int WarHouseId { get; set; }
        public string ProductName { get; set; }
        public int Qty { get; set; }

        
    }

}