using System;
using System.ComponentModel.DataAnnotations;

namespace MealBoxCloud.Models
{
    public class SupplierModel
    {

        public int supplierId { get; set; }

        [Display(Name = "Name")]
        public string suppliername { get; set; }

        [Display(Name = "Contact Person")]
        public string contactperson { get; set; }

        [Display(Name = "BackUp Contact")]
        public string BackUpContact { get; set; }
        [Display(Name = "City")]
        public string City_ { get; set; }
        [Display(Name = "Phone No")]
        [RegularExpression("^[0-9]\\d*$", ErrorMessage = "Contact No Must Be a Number")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Contact Number Must Contain 11 digits")]
        public string phoneno { get; set; }
        [Display(Name = "Mobile No")]
        public string mobile { get; set; }
        [Display(Name = "Fax No")]
        public string faxno { get; set; }
        [Display(Name = "Postal Code")]
        public string postalcode { get; set; }
        [Display(Name = "Designation")]
        public string designation { get; set; }
        [Display(Name = "Address")]
        public string AddressOne { get; set; }
        [Display(Name = "Shop No")]
        public string AddressTwo { get; set; }
        [Display(Name = "NIC")]
        [RegularExpression("^[0-9]\\d*$", ErrorMessage = "Contact No Must Be a Number")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "Contact Number Must Contain 13 digits")]


        public string CNIC { get; set; }
        [Display(Name = "URL")]
        public string Url { get; set; }
        [Display(Name = "Business Nature")]
        public string BusinessNature { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        [EmailAddress]
        public string Email { get; set; }
        public string NTNNTRNo { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<bool> IsActive { get; set; }

        public Nullable<int> Limit { get; set; }
        public string Sup_Code { get; set; }
        public string CompanyId { get; set; }
        public string BranchId { get; set; }

        [Display(Name = "Previous Balance")]
        public string PreviousBalance { get; set; }
        public string sup_acc { get; set; }

        public string prebal { get; set; }
    }
}