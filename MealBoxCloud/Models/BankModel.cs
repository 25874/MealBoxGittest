using System;

namespace MealBoxCloud.Models
{
    public class BankModel
    {
        public int CashBnk_id { get; set; }
        public string CashBnk_nam { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}