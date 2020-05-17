using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeBenefits.Models;
using EmployeeBenefits.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeBenefits.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IMongoDbEmployeeRepository _mongoDbEmployeeRepository;
        public EmployeeController(IMongoDbEmployeeRepository mongoDbEmployeeRepository)
        {
            _mongoDbEmployeeRepository = mongoDbEmployeeRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            var employees = await _mongoDbEmployeeRepository.GetAll();
            return employees;
        }

        [HttpGet("{id}")]
        public async Task<Employee> Get(int id)
        {
            return new Employee();
        }

        [HttpPost]
        public async Task<Employee> Create(Employee employee)
        {
            var createdEmployee = await _mongoDbEmployeeRepository.Create(employee);
            return createdEmployee;
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
