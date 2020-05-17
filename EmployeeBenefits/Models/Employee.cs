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
    }

    public interface IBeneficiary : IEntity
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        double YearlyBenefitsCost { get; set; }
        bool DiscountApplied { get; set; }
    }

    public class Dependent : IBeneficiary
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double YearlyBenefitsCost { get; set; }
        public bool DiscountApplied { get; set; }
    }
    public class Employee : IBeneficiary
    {
        [BsonId]
        public string Id { get; set; }
        public string EmployeeIdentifier { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double WeeklyPayRate { get; set; }
        public double YearlyPayRate { get; set; }
        public double YearlyBenefitsCost { get; set; }
        public bool DiscountApplied { get; set; }
        public double YearlyNetBenefitsCost { get; set; }
        public double PayPeriodNetBenefitsCost { get; set; }

        public Dependent Spouse { get; set; }
        public IEnumerable<Dependent> Dependents { get; set; }
    }
}
