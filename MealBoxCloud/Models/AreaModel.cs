using System;

namespace MealBoxCloud.Models
{
    public class AreaModel
    {
        public int areaid { get; set; }
        public string area_ { get; set; }
        public string CompanyId { get; set; }
        public string BranchId { get; set; }
        public int CityId { get; set; }
        public int PrivinceId { get; set; }
        public string CityName { get; set; }
        public Nullable<int> ProvinceIdFk { get; set; }
        public string ProvinceName { get; set; }
        public Nullable<int> CityIdFk { get; set; }

    }
}