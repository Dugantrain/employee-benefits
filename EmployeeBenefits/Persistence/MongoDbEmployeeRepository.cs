using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeBenefits.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EmployeeBenefits.Persistence
{
    public class MongoDbEmployeeRepository  : IEmployeeRepository
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

        public async Task<IEnumerable<Employee>> GetAll()
        {
            var employees = await _employeeCollection.Find(new BsonDocument())
                .ToListAsync();
            return employees;
        }

        public async Task<Employee> GetById(string id)
        {
            var filter = Builders<Employee>.Filter.Eq(x => x.Id, id);
            var employee = await _employeeCollection.Find(filter)
                .FirstOrDefaultAsync();
            return employee;
        }

        public async Task<Employee> GetByEmployeeIdentifier(string employeeIdentifier)
        {
            var filter = Builders<Employee>.Filter.Eq(x => x.EmployeeIdentifier, employeeIdentifier);
            var employee = await _employeeCollection.Find(filter)
                .FirstOrDefaultAsync();
            return employee;
        }

        public async Task DeleteById(string id)
        {
            var filter = Builders<Employee>.Filter.Eq(x => x.Id, id);
            var employee = await _employeeCollection.DeleteOneAsync(filter);
            if (employee.DeletedCount == 0)
                throw new Exception($"Unable to delete {id}.");
        }
    }
}