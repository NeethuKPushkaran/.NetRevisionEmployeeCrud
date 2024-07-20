using EmployeeCrudWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeCrudWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        List<Employee> _employee = new List<Employee>()
        {
            new Employee{ Id =  1, Name = "Jim", Designation = "Software Engineer", Salary = 50000},
            new Employee{ Id = 2, Name = "Mike", Designation = "Doctor", Salary = 100000},
            new Employee{ Id = 3, Name = "Linda", Designation = "Teacher", Salary = 30000}
        };

        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetEmployees()
        {
            return _employee;
        }

        //Get: api/Employee/id
        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployeeById(int id)
        {
            var employee = _employee.FirstOrDefault(e =>  e.Id == id);
            if(employee == null)
            {
                BadRequest("Employee not found.");
            }
            return (employee);
        }

        //Post: api/Employee
        [HttpPost]
        public ActionResult<Employee> AddEmployee(Employee employee)
        {
            employee.Id = _employee.Count + 1;
            _employee.Add(employee);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
        }

        //Put: api/Employee/{id}
        [HttpPut("{id}")]
        public ActionResult<Employee> UpdateEmployee(int id, Employee updatedEmployee)
        {
            var employee = _employee.FirstOrDefault(e => e.Id == id);
            if(employee == null)
            {
                return NotFound();
            }
            employee.Name = updatedEmployee.Name;
            employee.Designation = updatedEmployee.Designation;
            employee.Salary = updatedEmployee.Salary;
            return NoContent();
        }

        //Delete: api/Employee/{id}
        [HttpDelete("{id}")]
        public ActionResult<Employee> DeleteEmployee(int id)
        {
            var employee = _employee.FirstOrDefault(e => e.Id == id);
            if(employee == null)
            {
                return NotFound();
            }
            _employee.Remove(employee);
            return NoContent();
        }
    }
}
