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

        [HttpPatch("{id}/benefits-costs")]
        public Employee ApplyCostsAndDeductions(Employee employee)
        {
            employee = _benefitsCalculatorService.SetEmployeeCostsAndDeductions(employee);
            return employee;
        }

        [HttpPost]
        public async Task<Employee> Create(Employee employee)
        {
            var existingEmployee = await _employeeRepository.GetByEmployeeIdentifier(employee.EmployeeIdentifier);
            if(existingEmployee != null) throw new Exception("Employee Identifier is already taken.");

            employee = _benefitsCalculatorService.SetEmployeeCostsAndDeductions(employee);
            var createdEmployee = await _employeeRepository.Create(employee);
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
            await _employeeRepository.DeleteById(id);
        }
    }
}
