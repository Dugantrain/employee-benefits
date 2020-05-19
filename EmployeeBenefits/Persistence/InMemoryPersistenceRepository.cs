using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeBenefits.Models;

namespace EmployeeBenefits.Persistence
{
    public class InMemoryPersistenceRepository : IEmployeeRepository
    {
        private IEnumerable<Employee> _employees;

        public InMemoryPersistenceRepository()
        {
            _employees = new List<Employee>();
        }

        public Task<IEnumerable<Employee>> GetAll()
        {
            return Task.FromResult((IEnumerable<Employee>)_employees);
        }

        public Task<Employee> GetById(string id)
        {
            var employee = _employees.FirstOrDefault(e => e.Id == id);
            return Task.FromResult(employee);
        }

        public Task<Employee> GetByEmployeeIdentifier(string employeeIdentifier)
        {
            var employee = _employees.FirstOrDefault(e => e.EmployeeIdentifier == employeeIdentifier);
            return Task.FromResult(employee);
        }

        public Task<Employee> Upsert(Employee employee)
        {
            var employeeList = _employees.ToList();
            if (!string.IsNullOrEmpty(employee.Id))
            {
                employeeList.RemoveAll(e => e.Id == employee.Id);
            }
            employeeList.Add(employee);
            _employees = employeeList;
            return Task.FromResult(employee);
        }

        public Task DeleteById(string id)
        {
            _employees = _employees.Where(e => e.Id != id);
            return Task.CompletedTask;
        }
    }
}
