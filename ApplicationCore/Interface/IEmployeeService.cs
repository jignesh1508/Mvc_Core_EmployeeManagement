using MVC_EmployeeManagement.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interface
{
    public interface IEmployeeService
    {
        Employee Add(Employee employee);
        Employee GetByid(Guid id);
        List<Employee> GetAll();
        void Update(Employee employee);
        void Delete(Employee employee);
    }
}
