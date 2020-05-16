using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeBenefits.Models;
using MongoDB.Driver;

namespace EmployeeBenefits.Persistence
{
    public interface IMongoDbEmployeeRepository
    {
        Task<Employee> Create(Employee employee);
        Task DeleteByEmployeeId(int employeeId);
        Task<IEnumerable<Employee>> Get();
    }

    public class MongoDbEmployeeRepository  : IMongoDbEmployeeRepository
    {
        private readonly IMongoCollection<Employee> _employeeCollection;

        public MongoDbEmployeeRepository(IMongoDatabase database)
        {
            if (database == null) throw new ArgumentNullException(nameof(database));
            _employeeCollection = database.GetCollection<Employee>("employees");
        }

        public async Task<Employee> Create(Employee employee)
        {
            try
            {
                if (employee == null) throw new ArgumentNullException(nameof(employee));
                await _employeeCollection.InsertOneAsync(employee, new InsertOneOptions());
                return employee;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<IEnumerable<Employee>> Get()
        {
            var filter = Builders<Employee>.Filter.Ne(x => x.FirstName, Guid.NewGuid().ToString());
            var employees = await _employeeCollection.Find(filter)
                .ToListAsync();
            return employees;
        }

        public async Task DeleteByEmployeeId(int employeeId)
        {
            var filter = Builders<Employee>.Filter.Eq(x => x.LastName, employeeId.ToString());
            var employee = await _employeeCollection.DeleteOneAsync(filter);
            if (employee.DeletedCount == 0)
                throw new Exception($"Unable to delete {employeeId}.");
        }
    }
}