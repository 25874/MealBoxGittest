using System;
using System.ComponentModel.DataAnnotations;
namespace MealBoxCloud.Models
{
    public class ProductModel
    {
        public int ProductID { get; set; }
        [Display(Name = "Item Type")]
        public Nullable<int> ProductTypeID { get; set; }
        [Display(Name = "Name")]
        public string ProductName { get; set; }

        [Display(Name = "Packing Size")]
        public int PckSize { get; set; }
        //public double Cost { get; set; }
        //public string ProductDiscriptions { get; set; }
        //public string Supplier_Customer { get; set; }

        public string Unit { get; set; }
        [Display(Name = "Purchase Price")]
        public Nullable<double> PurchasePrice { get; set; }
        [Display(Name = "Sale Price")]
        public Nullable<double> SalePrice { get; set; }
        [Display(Name = "Packets in Cartons")]
        public Nullable<double> stk_carton { get; set; }
        //public string ProductType { get; set; }
        //public string CreatedBy { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }

        public int Limit { get; set; }


        public string ProductTypeName { get; set; }
        public string CreateBy { get; set; }

        public Nullable<bool> IsActive { get; set; }
        public string CompanyId { get; set; }
        public string BranchId { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<System.DateTime> UpdateAt { get; set; }



        //public string Pro_Code { get; set; }
        //public Nullable<bool> IsActive { get; set; }
        //public string CompanyId { get; set; }
        //public string BranchId { get; set; }

    }

}