using BooksBackEnd.Models.Employees;
using BooksBackEnd.Models.Status;
using BooksBackEnd.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksBackEnd.Controllers
{
    public class StatusController : ControllerBase
    {

        private ISystemTime _systemTime;

        public StatusController(ISystemTime systemTime)
        {
            _systemTime = systemTime;
        }


        // GET /status
        [HttpGet("status")]
        public ActionResult GetTheStatus()
        {
            var response = new GetStatusResponse
            {
                CheckedBy = "Joe",
                Message = "Looks good, Boss!",
                LastChecked = _systemTime.GetCurrent()
            };

            return Ok(response);
        }

        // Url - "Route Parameters"
        [HttpGet("/products/{category}/{productid:int}")]
        public ActionResult GetProduct(string category, int productid)
        {
            return Ok($"That is in the category of {category} and product id { productid }");
        }

        [HttpGet("/employees")]
        public ActionResult GetEmployeesInDepartment([FromQuery] string department = "All")
        {
            return Ok($"Giving you all the employees in {department}");
        }

        [HttpGet("/whoami")]
        public ActionResult WhoAmI([FromHeader(Name ="User-Agent")] string userAgent)
        {
            return Ok($"I see you are running {userAgent}. Good choice.");
        }

        [HttpPost("/employees")]
        public ActionResult Hire([FromBody] PostEmployeeCreate employee)
        {
            return Ok($"Hiring {employee.Name} in {employee.Department} for {employee.StartingSalary:c}");
        }
    }
}
