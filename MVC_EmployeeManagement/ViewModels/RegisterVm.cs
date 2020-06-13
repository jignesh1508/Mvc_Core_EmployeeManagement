using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_EmployeeManagement.ViewModels
{
    public class RegisterVm
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Confirm Password is not match with password ")]
        [Display(Name ="Confirm Passsword")]
        public string ConfirmPassword { get; set; }
    }
}
