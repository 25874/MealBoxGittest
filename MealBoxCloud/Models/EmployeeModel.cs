using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MealBoxCloud.Models
{
    public class EmployeeModel
    {
        public int employeeID { get; set; }
        [Display(Name ="Name")]
        public string employeeName { get; set; }
        public string employeeLastName { get; set; }
        public string employeeFatherName { get; set; }
        public string CategoryID { get; set; }
        public Nullable<int> EmployeeType { get; set; }
        public string Qualification { get; set; }

        [RegularExpression("^[0-9]\\d*$", ErrorMessage = "Contact No Must Be a Number")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "Contact Number Must Contain 13 digits")]

        public string CNIC { get; set; }
        public string CityID { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        [Display(Name = "Tele Phone No")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Contact Number Must Contain 11 digits")]
        public string PhoneNum { get; set; }
        [Display(Name = "Mobile No")]
        
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Contact Number Must Contain 11 digits")]
        public string CellNum { get; set; }
        [Display(Name = "Fax NO")]
        
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Passport { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        [EmailAddress]
        public string Email { get; set; }
        public string PostalCode { get; set; }
        public string MarkID { get; set; }
        public string Gender { get; set; }
        public string Disability { get; set; }
        public string NTN { get; set; }
        public string Religion { get; set; }
        public string ConvcenceAllowance_Day { get; set; }
        public string ConvcenceAllowance_Km { get; set; }
        public string SalesPercentage { get; set; }

        [Display(Name = "Salary")]
        
        public int booker { get; set; }

        public int Salesman { get; set; }

        public int Assign { get; set; }


        public string NetSalary { get; set; }
        public string RMSID { get; set; }
        public string RSMPercentage { get; set; }
        public Nullable<bool> EmployeeCardExits { get; set; }
        public Nullable<bool> EmployeeCardPrinted { get; set; }
        public string COAID { get; set; }
        public Nullable<bool> IsSalman { get; set; }
        public Nullable<System.DateTime> Created_At { get; set; }
        public string Created_By { get; set; }
        [Display(Name = "Designation")]
        public string designation { get; set; }
        
        public string Address { get; set; }
        public Nullable<double> income_tax { get; set; }
        public string others { get; set; }
        public string incentive { get; set; }
        public string Entertainment { get; set; }
        public Nullable<int> Depart_id { get; set; }
        public string emp_acc { get; set; }

        public bool IsActive { get; set; }
    }
}