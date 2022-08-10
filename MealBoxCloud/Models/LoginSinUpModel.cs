using System.ComponentModel.DataAnnotations;

namespace MealBoxCloud.Models
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

        public string EmployeeName { get; set; }

        public bool IsActive { get; set; }

        public int Module { get; set; }

        public int pages { get; set; }

    }


}