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
    
    public partial class tbl_PayTyp
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_PayTyp()
        {
            this.tbl_vchtyp = new HashSet<tbl_vchtyp>();
            this.tbl_vchtyp1 = new HashSet<tbl_vchtyp>();
        }
    
        public int PayTyp_id { get; set; }
        public string PayTyp_nam { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> vchtyp_id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_vchtyp> tbl_vchtyp { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_vchtyp> tbl_vchtyp1 { get; set; }
    }
}
