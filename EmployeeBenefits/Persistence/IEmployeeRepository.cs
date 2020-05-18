using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeBenefits.Models;

namespace EmployeeBenefits.Persistence
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAll();
        Task<Employee> GetById(string id);
        Task<Employee> GetByEmployeeIdentifier(string employeeIdentifier);
        Task<Employee> Create(Employee employee);
        Task DeleteById(string id);
    }
}