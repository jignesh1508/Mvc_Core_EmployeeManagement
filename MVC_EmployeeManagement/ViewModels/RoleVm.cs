using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_EmployeeManagement.ViewModels
{
    public class RoleVm
    {
        [Required]
        public string Name { get; set; }
    }
}
