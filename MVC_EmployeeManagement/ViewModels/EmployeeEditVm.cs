using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_EmployeeManagement.ViewModels
{
    public class EmployeeEditVm:EmployeeCreateVm
    {
        public Guid Id { get; set; }
        public string ExistingPhotoPath { get; set; }
    }
}
