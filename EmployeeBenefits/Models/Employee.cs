using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeBenefits.Models
{
    public interface IModel
    {
        int Id { get; set; }
    }

    public interface IBeneficiary : IModel
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string Ssn { get; set; }
    }
    public class Employee : IBeneficiary
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Ssn { get; set; }
        public double BaseRate { get; set; }

        public IBeneficiary Spouse { get; set; }
        public IEnumerable<IBeneficiary> Dependents { get; set; }
    }
}
