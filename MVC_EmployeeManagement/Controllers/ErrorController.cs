using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MVC_EmployeeManagement.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statuscode}")]
        public IActionResult HttpSatusCodeHandler(int statuscode)
        {
            switch(statuscode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry the resource you requested is not found";
                    break;
            }

            return View("NotFound");
        }
    }
}