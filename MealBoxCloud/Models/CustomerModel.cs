using System;
using System.ComponentModel.DataAnnotations;

namespace MealBoxCloud.Models
{
    public class CustomerModel
    {
        public int CustomerID { get; set; }
        [Display(Name = "Name")]
        public string CustomerName { get; set; }
        public string GST { get; set; }
        public string category { get; set; }
        public string NTN { get; set; }
        public string customertype_ { get; set; }
        public Nullable<int> areaid { get; set; }
        public string Area { get; set; }
        public string RefNum { get; set; }
        public string District { get; set; }

        public string Discount { get; set; }

        [Display(Name = "Phone")]


        [RegularExpression("^[0-9]\\d*$", ErrorMessage = "Contact No Must Be a Number")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Contact Number Must Contain 11 digits")]
        public string PhoneNo { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        [EmailAddress]
        public string Email { get; set; }
        public string CellNo1 { get; set; }
        public string PostalCode { get; set; }
        public string CellNo2 { get; set; }
        public string PostalOfficeContact { get; set; }
        public string CellNo3 { get; set; }

        [RegularExpression("^[0-9]\\d*$", ErrorMessage = "Contact No Must Be a Number")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "Contact Number Must Contain 11 digits")]

        public string NIC { get; set; }
        public string CellNo4 { get; set; }
        [Display(Name = "City")]
        public string city_ { get; set; }
        public string Address { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<double> saleper { get; set; }
        public string Cus_Code { get; set; }
        public string CompanyId { get; set; }
        public string BranchId { get; set; }
        public Nullable<bool> left_eye { get; set; }
        public Nullable<bool> right_eye { get; set; }
        public string RSph { get; set; }
        public string RCyl { get; set; }
        public string RAxis { get; set; }
        public string RAdd { get; set; }
        public string LSph { get; set; }
        public string LCyl { get; set; }
        public string LAsix { get; set; }
        public string LAdd { get; set; }
        public Double PreviousBalance { get; set; }
        public string cust_acc { get; set; }
    }
}