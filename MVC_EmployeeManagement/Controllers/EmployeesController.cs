using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Infrastructure;
using MVC_EmployeeManagement.Model;
using ApplicationCore.Interface;
using MVC_EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace MVC_EmployeeManagement.Controllers
{
    [Route("[controller]/[action]")]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IHostingEnvironment _hostingEnvironment;
        public EmployeesController(IEmployeeService employeeService,IHostingEnvironment hostingEnvironement)
        {
            _employeeService = employeeService;
            _hostingEnvironment = hostingEnvironement;
        }


        // GET: Employees
        [Route("")]
        
        // GET: Employees
        [Route("~/")]
        public IActionResult Index()
        {
            var employees = _employeeService.GetAll();
            return View(employees);
        }

        // GET: Employees/Details/5
        [Route("{id}")]
        public IActionResult Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _employeeService.GetByid(id);
                
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FirstName,LastName,Id,Photo,CreatedDate,UpdatedDate")] EmployeeCreateVm employeeVm)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;

                if(employeeVm.Photo!=null)
                {
                    var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath + "\\images"); //Web root image folder
                    uniqueFileName =  Guid.NewGuid().ToString() + "_" + employeeVm.Photo.FileName; //File Name

                    var filePath=Path.Combine(uploadsFolder, uniqueFileName); //Combine above two to create one path

                    employeeVm.Photo.CopyTo(new FileStream(filePath, FileMode.Create)); // here we can code to copy(upload) on azure storage
                }

                var employee = new Employee();
                employee.FirstName = employeeVm.FirstName;
                employee.LastName=  employeeVm.LastName;
                employee.PhotoPath = uniqueFileName;
                employee.Id = Guid.NewGuid();

                _employeeService.Add(employee);
                return RedirectToAction("Details",new { id = employee.Id });
            }
            return View(employeeVm);
        }

        // GET: Employees/Edit/5
        public IActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _employeeService.GetByid(id);
            if (employee == null)
            {
                return NotFound();
            }

            var employeeEdit = new EmployeeEditVm();
            employeeEdit.Id = employee.Id;
            employeeEdit.FirstName = employee.FirstName;
            employeeEdit.LastName = employee.LastName;
            employeeEdit.ExistingPhotoPath = employee.PhotoPath;

            return View(employeeEdit);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("FirstName,LastName,Id,Photo,CreatedDate,UpdatedDate")] EmployeeEditVm employeeVm)
        {
            if (id != employeeVm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var employee = _employeeService.GetByid(id);
                    employee.FirstName = employeeVm.FirstName;
                    employee.LastName = employeeVm.LastName;


                    string uniqueFileName = null;

                    if (employeeVm.Photo != null)
                    {
                        if(employeeVm.ExistingPhotoPath!=null)
                        {
                           var filePathToDelete= Path.Combine(_hostingEnvironment.WebRootPath + "\\images" + employeeVm.ExistingPhotoPath);
                            System.IO.File.Delete(filePathToDelete);
                        }
                        var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath + "\\images"); //Web root image folder
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + employeeVm.Photo.FileName; //File Name

                        var filePath = Path.Combine(uploadsFolder, uniqueFileName); //Combine above two to create one path
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            employeeVm.Photo.CopyTo(fileStream); // here we can code to copy(upload) on azure storage
                        }
                           
                    }

               
                  
                    employee.PhotoPath = uniqueFileName;



                    _employeeService.Update(employee);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employeeVm.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employeeVm);
        }

        // GET: Employees/Delete/5
        public IActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _employeeService.GetByid(id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var employee =  _employeeService.GetByid(id);

            _employeeService.Delete(employee);
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(Guid id)
        {
            var employees = _employeeService.GetAll();
            return employees.Any(e => e.Id == id);
        }
    }
}
