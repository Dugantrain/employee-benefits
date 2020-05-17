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
            double yearlyNetBenefitsCost = 0;
            employee.WeeklyPayRate = BenefitCosts.WeeklyPayRate;
            employee.YearlyPayRate = employee.WeeklyPayRate * BenefitCosts.NumberOfPayPeriods;
            employee.YearlyBenefitsCost = BenefitCosts.EmployeeBenefitsYearlyBaseCost;
            ApplyBeneficiaryDiscountIfApplicable(employee);
            yearlyNetBenefitsCost += employee.YearlyBenefitsCost;
            if (employee.Spouse != null)
            {
                employee.Spouse.YearlyBenefitsCost = BenefitCosts.DependentBenefitsYearlyBaseCost;
                ApplyBeneficiaryDiscountIfApplicable(employee.Spouse);
                yearlyNetBenefitsCost += employee.Spouse.YearlyBenefitsCost;
            }

            if (employee.Dependents != null)
            {
                foreach (var dependent in employee.Dependents)
                {
                    dependent.YearlyBenefitsCost = BenefitCosts.DependentBenefitsYearlyBaseCost;
                    ApplyBeneficiaryDiscountIfApplicable(dependent);
                    yearlyNetBenefitsCost += dependent.YearlyBenefitsCost;
                }
            }
            employee.YearlyNetBenefitsCost = yearlyNetBenefitsCost;
            employee.PayPeriodNetBenefitsCost = yearlyNetBenefitsCost / BenefitCosts.NumberOfPayPeriods;
            return employee;
        }

        private IBeneficiary ApplyBeneficiaryDiscountIfApplicable(IBeneficiary beneficiary)
        {
            if (beneficiary.FirstName.ToLower().StartsWith("a"))
            {
                beneficiary.YearlyBenefitsCost -= (beneficiary.YearlyBenefitsCost * BenefitCosts.BeneficiaryYearlyDiscountForLetterA);
                beneficiary.DiscountApplied = true;
            }
            return beneficiary;
        }

    }

    

    public static class BenefitCosts
    {
        public static double WeeklyPayRate = 2000;
        public static double EmployeeBenefitsYearlyBaseCost = 1000;
        public static double DependentBenefitsYearlyBaseCost = 500;
        public static double BeneficiaryYearlyDiscountForLetterA = .1;
        public static int NumberOfPayPeriods = 26;
    }
}
