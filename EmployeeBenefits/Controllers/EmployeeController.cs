using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeBenefits.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeBenefits.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {

        [HttpGet]
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return new List<Employee>();
        }

        [HttpGet("{id}")]
        public async Task<Employee> Get(int id)
        {
            return new Employee();
        }

        [HttpPost]
        public async Task<Employee> Create(Employee employee)
        {
            return new Employee();
        }

        [HttpPatch]
        public async Task<Employee> Change(Employee employee)
        {
            return new Employee();
        }

        [HttpDelete]
        public async Task Remove(int id)
        {
            
        }
    }
}
