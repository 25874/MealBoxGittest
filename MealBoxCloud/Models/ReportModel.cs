using System.Collections.Generic;

namespace MealBoxCloud.Models
{
    public class ReportModel
    {
        public int ReportId { get; set; }

        public List<Sp_SummaryReport_Result> SummaryReport_Results { get; set; }

        public string ProductId { get; set; }

        public string SupplierId { get; set; }

        public string CustomerId { get; set; }

        public string AccountId { get; set; }
        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public string PaymentType { get; set; }

        public string PaymentMethod { get; set; }

        public string WareHouseId { get; set; }

        public string CityId { get; set; }


    }
}