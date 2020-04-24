using ApplicationCore.Interface;
using MVC_EmployeeManagement.Interface;
using MVC_EmployeeManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationCore.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _repository;
        public EmployeeService(IRepository<Employee> repository)
        {
            _repository = repository;
        }
        public Employee Add(Employee employee)
        {
           return _repository.Add(employee);
        }

        public Employee GetByid(Guid id)
        {
            return _repository.GetById(id);
        }

        public List<Employee> GetAll()
        {
            return _repository.ListAll().ToList();
        }

        public void Update(Employee employee)
        {
            _repository.Update(employee);
        }
        public void Delete(Employee employee)
        {
            _repository.Delete(employee);
        }
    }
}
