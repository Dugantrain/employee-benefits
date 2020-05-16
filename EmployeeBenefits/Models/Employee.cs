using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EmployeeBenefits.Models
{
    public interface IModel
    {
    }

    public interface IBeneficiary : IModel
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string Ssn { get; set; }
    }

    public class Dependent : IBeneficiary
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Ssn { get; set; }
    }
    public class Employee : IBeneficiary
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Ssn { get; set; }
        public double BaseRate { get; set; }

        public Dependent Spouse { get; set; }
        public IEnumerable<Dependent> Dependents { get; set; }
    }
}
