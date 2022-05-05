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
    
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.DPurchases = new HashSet<DPurchase>();
            this.DPurchases1 = new HashSet<DPurchase>();
            this.tbl_DSal = new HashSet<tbl_DSal>();
            this.tbl_ddsrbk = new HashSet<tbl_ddsrbk>();
        }
    
        public int ProductID { get; set; }
        public Nullable<int> ProductTypeID { get; set; }
        public string ProductName { get; set; }
        public Nullable<int> PckSize { get; set; }
        public double Cost { get; set; }
        public string ProductDiscriptions { get; set; }
        public string Supplier_Customer { get; set; }
        public string Unit { get; set; }
        public Nullable<double> PurchasePrice { get; set; }
        public double SalePrice { get; set; }
        public Nullable<double> stk_carton { get; set; }
        public string ProductType { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public string Pro_Code { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CompanyId { get; set; }
        public string BranchId { get; set; }
        public Nullable<bool> IsPerchase { get; set; }
        public Nullable<int> Limit { get; set; }
        public string ProductCode { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DPurchase> DPurchases { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DPurchase> DPurchases1 { get; set; }
        public virtual tbl_producttype tbl_producttype { get; set; }
        public virtual tbl_producttype tbl_producttype1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DSal> tbl_DSal { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_ddsrbk> tbl_ddsrbk { get; set; }
    }
}
