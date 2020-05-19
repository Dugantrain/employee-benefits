using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeBenefits.Models;

namespace EmployeeBenefits.Services
{
    public interface IBenefitsCalculatorService
    {
        Employee SetEmployeeCostsAndDeductions(Employee employee);

    }


    public class BenefitsCalculatorService: IBenefitsCalculatorService
    {
        public Employee SetEmployeeCostsAndDeductions(Employee employee)
        {
            double totalYearlyNetBenefitsCost = 0;
            employee.PeriodPayRate = BenefitCosts.PeriodPayRate;
            employee.YearlyPayRate = employee.PeriodPayRate * BenefitCosts.NumberOfPayPeriods;
            employee.YearlyBaseBenefitsCost = BenefitCosts.EmployeeBenefitsYearlyBaseCost;
            employee.YearlyNetBenefitsCost = BenefitCosts.EmployeeBenefitsYearlyBaseCost;
            ApplyBeneficiaryDiscountIfApplicable(employee);
            totalYearlyNetBenefitsCost += employee.YearlyNetBenefitsCost;
            if (employee.Spouse != null && !string.IsNullOrEmpty(employee.Spouse.FirstName) 
                                        && !string.IsNullOrEmpty(employee.Spouse.LastName))
            {
                employee.Spouse.YearlyBaseBenefitsCost = BenefitCosts.DependentBenefitsYearlyBaseCost;
                employee.Spouse.YearlyNetBenefitsCost = BenefitCosts.DependentBenefitsYearlyBaseCost;
                ApplyBeneficiaryDiscountIfApplicable(employee.Spouse);
                totalYearlyNetBenefitsCost += employee.Spouse.YearlyNetBenefitsCost;
            }

            if (employee.Dependents != null)
            {
                foreach (var dependent in employee.Dependents)
                {
                    dependent.YearlyBaseBenefitsCost = BenefitCosts.DependentBenefitsYearlyBaseCost;
                    dependent.YearlyNetBenefitsCost = BenefitCosts.DependentBenefitsYearlyBaseCost;
                    ApplyBeneficiaryDiscountIfApplicable(dependent);
                    totalYearlyNetBenefitsCost += dependent.YearlyNetBenefitsCost;
                }
            }
            employee.TotalYearlyNetBenefitsCost = totalYearlyNetBenefitsCost;
            employee.PayPeriodNetBenefitsCost = totalYearlyNetBenefitsCost / BenefitCosts.NumberOfPayPeriods;
            return employee;
        }

        private IBeneficiary ApplyBeneficiaryDiscountIfApplicable(IBeneficiary beneficiary)
        {
            if (beneficiary == null) return beneficiary;
            if (string.IsNullOrEmpty(beneficiary.FirstName)) return beneficiary;
            if (!beneficiary.FirstName.ToLower().StartsWith("a")) return beneficiary;
            beneficiary.YearlyNetBenefitsCost = beneficiary.YearlyBaseBenefitsCost - (beneficiary.YearlyBaseBenefitsCost * BenefitCosts.BeneficiaryYearlyDiscountForLetterA);
            beneficiary.DiscountApplied = true;

            return beneficiary;
        }
    }

    

    public static class BenefitCosts
    {
        public static double PeriodPayRate = 2000;
        public static double EmployeeBenefitsYearlyBaseCost = 1000;
        public static double DependentBenefitsYearlyBaseCost = 500;
        public static double BeneficiaryYearlyDiscountForLetterA = .1;
        public static int NumberOfPayPeriods = 26;
    }
}
