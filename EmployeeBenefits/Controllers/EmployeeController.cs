using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeBenefits.Models;
using EmployeeBenefits.Persistence;
using EmployeeBenefits.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace EmployeeBenefits.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IMongoDbEmployeeRepository _mongoDbEmployeeRepository;
        private readonly IBenefitsCalculatorService _benefitsCalculatorService;
        public EmployeeController(IMongoDbEmployeeRepository mongoDbEmployeeRepository, IBenefitsCalculatorService benefitsCalculatorService)
        {
            _mongoDbEmployeeRepository = mongoDbEmployeeRepository;
            _benefitsCalculatorService = benefitsCalculatorService;
        }

        [HttpGet]
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            var employees = await _mongoDbEmployeeRepository.GetAll();
            return employees;
        }

        [HttpGet("{id}")]
        public async Task<Employee> Get(string id)
        {
            return new Employee();
        }

        [HttpPost]
        public async Task<Employee> Create(Employee employee)
        {
            employee = _benefitsCalculatorService.SetEmployeeCostsAndDeductions(employee);
            var createdEmployee = await _mongoDbEmployeeRepository.Create(employee);
            return createdEmployee;
        }

        [HttpPatch]
        public async Task<Employee> Change(Employee employee)
        {
            return new Employee();
        }

        [HttpDelete]
        public async Task Remove(string id)
        {
            await _mongoDbEmployeeRepository.DeleteById(id);
        }
    }
}
