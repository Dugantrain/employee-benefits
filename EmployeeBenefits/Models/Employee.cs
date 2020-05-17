using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EmployeeBenefits.Models
{
    public interface IEntity
    {
        [BsonId]
        ObjectId Id { get; set; }
    }

    public interface IBeneficiary : IEntity
    {
        string FirstName { get; set; }
        string LastName { get; set; }
    }

    public class Dependent : IBeneficiary
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class Employee : IBeneficiary
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string EmployeeIdentifier { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double BaseRate { get; set; }

        public Dependent Spouse { get; set; }
        public IEnumerable<Dependent> Dependents { get; set; }
    }
}
