using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeBenefits.Models;

namespace EmployeeBenefits.Services
{
    public interface IBenefitsCalculatorService
    {
        //double GetBaseRate(Employee employee);
        //double GetBaseBenefitsCost(Employee employee);
        //double GetDiscount(Employee employee);

    }
    public class BenefitsCalculatorService: IBenefitsCalculatorService
    {
        public double GetEmployeeBaseRate(Employee employee)
        {
            return BenefitCosts.BaseRate;
        }

        public double GetEmployeeBaseBenefitsCost(Employee employee)
        {
            return BenefitCosts.BaseEmployeeBenefitsCost;
        }

        public double GetDependentBenefitsBaseCost(Employee employee)
        {
            if (employee.Dependents == null || !employee.Dependents.Any()) return 0;
            return employee.Dependents.Count() * BenefitCosts.DependentBenefitsBaseCost;
        }

        public double GetBeneficiaryCostWithOptionalDiscount(IBeneficiary beneficiary)
        {
            const double nameDiscount = .1;
            return beneficiary.FirstName.ToLower().StartsWith("a") 
                ? (BenefitCosts.DependentBenefitsBaseCost) * (BenefitCosts.BeneficiaryNameDiscount) 
                : BenefitCosts.DependentBenefitsBaseCost;
        }

    }

    public static class BenefitCosts
    {
        public static double BaseRate = 2000;
        public static double BaseEmployeeBenefitsCost = 1000;
        public static double DependentBenefitsBaseCost = 500;
        public static double BeneficiaryNameDiscount = .1;
    }
}
