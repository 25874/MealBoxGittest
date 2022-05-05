using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MealBox.Models
{
    public class AreaModel
    {
        public int areaid { get; set; }
        public string area_ { get; set; }
        public string CompanyId { get; set; }
        public string BranchId { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int PrivinceId { get; set; }
        public string ProvinceName { get; set;}
        public int PrivinceIdFk { get; set; }


    }
}