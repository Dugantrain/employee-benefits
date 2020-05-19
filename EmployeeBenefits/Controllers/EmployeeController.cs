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
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IBenefitsCalculatorService _benefitsCalculatorService;
        public EmployeeController(IEmployeeRepository employeeRepository, IBenefitsCalculatorService benefitsCalculatorService)
        {
            _employeeRepository = employeeRepository;
            _benefitsCalculatorService = benefitsCalculatorService;
        }

        [HttpGet]
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            var employees = await _employeeRepository.GetAll();
            return employees;
        }

        [HttpGet("{id}")]
        public async Task<Employee> Get(string id)
        {
            return await _employeeRepository.GetById(id);
        }

        [HttpGet("employeeIdentifier/{employeeIdentifier}")]
        public async Task<Employee> GetByEmployeeIdentifier(string employeeIdentifier)
        {
            var existingEmployee = await _employeeRepository.GetByEmployeeIdentifier(employeeIdentifier);
            return existingEmployee;
        }

        [HttpPatch("benefits")]
        public Employee ApplyCostsAndDeductions(Employee employee)
        {
            employee = _benefitsCalculatorService.SetEmployeeCostsAndDeductions(employee);
            return employee;
        }

        [HttpPost]
        public async Task<Employee> Upsert(Employee employee)
        {
            if (string.IsNullOrEmpty(employee.FirstName) || string.IsNullOrEmpty(employee.LastName) ||
                (employee.Spouse != null &&
                 (string.IsNullOrEmpty(employee.Spouse.FirstName) || string.IsNullOrEmpty(employee.Spouse.LastName))) ||
                (employee.Dependents != null &&
                 (employee.Dependents.Any(d => string.IsNullOrEmpty(d.FirstName) || employee.Dependents.Any(d => string.IsNullOrEmpty(d.LastName))))
                )
            )
            {
                throw new Exception("Employee Info incomplete.");
            }

            if (string.IsNullOrEmpty(employee.Id))
            {
                var existingEmployee = await _employeeRepository.GetByEmployeeIdentifier(employee.EmployeeIdentifier);
                if (existingEmployee != null) throw new Exception("Employee Identifier is already taken.");
            }

            employee = _benefitsCalculatorService.SetEmployeeCostsAndDeductions(employee);
            var createdEmployee = await _employeeRepository.Upsert(employee);
            return createdEmployee;
        }

        [HttpDelete]
        public async Task Remove(string id)
        {
            await _employeeRepository.DeleteById(id);
        }
    }
}
