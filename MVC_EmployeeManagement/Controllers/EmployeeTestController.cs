using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interface;
using Microsoft.AspNetCore.Mvc;
using MVC_EmployeeManagement.Model;

namespace MVC_EmployeeManagement.Controllers
{
    public class EmployeeTestController : Controller
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeTestController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }


        public IActionResult Index()
        {
            var employees = _employeeService.GetAll();
            return View(employees);
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {

            _employeeService.Add(employee);

            return View("Index");
        }
        public JsonResult Details()
        {
            var employee = _employeeService.GetByid(new Guid("35f19cf0-4170-4f13-8cd8-528ccde2b46e"));
            // return employee.FirstName + " " + employee.LastName;
            return Json(employee);
        }
    }
}