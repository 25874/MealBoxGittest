using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MealBox.Models
{
    public class LoginSinUpModel
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "User Name Must Required Here")]
        public string UserName { get; set; }

        public int UserTypeId { get; set; }
        [Required(ErrorMessage = "Please Insert Password here")]
        [DataType(DataType.Password)]

        public string Password { get; set; }

        public string Name { get; set; }

        public string ConfirmPassword { get; set; }

        public int EmployeeId { get; set; }

    }
}