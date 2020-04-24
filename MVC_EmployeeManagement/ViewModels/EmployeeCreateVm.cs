using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_EmployeeManagement.ViewModels
{
    public class EmployeeCreateVm
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IFormFile Photo { get; set; }
    }
}
