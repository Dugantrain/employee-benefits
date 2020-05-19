using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmployeeBenefits.Models;
using EmployeeBenefits.Services;
using JetBrains.Annotations;
using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;
using Xunit;

namespace EmployeeBenefits.Tests.Unit
{
    public class BenefitsCalculatorServiceTests
    {
        [Theory, AutoMockData]
        public void
            ShouldApplyNoDeductionsWhenEmployeeHasNoDependentsAndNameDoesNotBeginWithA(
                Employee employee
            )
        {
            employee.Dependents = null;
            employee.Spouse = null;
            employee.FirstName = "Steve";
            var sut = new BenefitsCalculatorService();
            employee = sut.SetEmployeeCostsAndDeductions(employee);
            Assert.Equal(employee.YearlyNetBenefitsCost, BenefitCosts.EmployeeBenefitsYearlyBaseCost);
            Assert.Equal(employee.PayPeriodNetBenefitsCost, 
                BenefitCosts.EmployeeBenefitsYearlyBaseCost / BenefitCosts.NumberOfPayPeriods);

        }

        [Theory, AutoMockData]
        public void
            ShouldApplyEmployeeDeductionWhenEmployeeNameBeginsWithA(
                Employee employee
            )
        {
            employee.Dependents = null;
            employee.Spouse = null;
            employee.FirstName = "Allen";
            var sut = new BenefitsCalculatorService();
            employee = sut.SetEmployeeCostsAndDeductions(employee);
            Assert.Equal(employee.YearlyNetBenefitsCost, 
                BenefitCosts.EmployeeBenefitsYearlyBaseCost - 
                (BenefitCosts.EmployeeBenefitsYearlyBaseCost * BenefitCosts.BeneficiaryYearlyDiscountForLetterA));
        }

        [Theory, AutoMockData]
        public void
            ShouldIncludeSpouseCostWhenEmployeeIsMarriedWithNoDeductionsWhenNoNameBeginsWithA(
                Employee employee
            )
        {
            employee.Dependents = null;
            employee.Spouse = new Dependent
            {
                FirstName = "Mary"
            };
            employee.FirstName = "Dave";
            var sut = new BenefitsCalculatorService();
            employee = sut.SetEmployeeCostsAndDeductions(employee);
            Assert.Equal(employee.TotalYearlyNetBenefitsCost,
                BenefitCosts.EmployeeBenefitsYearlyBaseCost + BenefitCosts.DependentBenefitsYearlyBaseCost);
        }

        [Theory, AutoMockData]
        public void
            ShouldIncludeDependentCostWhenEmployeeHasNoDependents(
                Employee employee
            )
        {
            employee.Spouse = null;
            employee.Dependents = new List<Dependent>
            {
                new Dependent
                {
                    FirstName = "Skippy"
                },
                new Dependent
                {
                    FirstName = "Joey"
                }
            };
            employee.FirstName = "Dave";
            var sut = new BenefitsCalculatorService();
            employee = sut.SetEmployeeCostsAndDeductions(employee);
            Assert.Equal(employee.TotalYearlyNetBenefitsCost,
                BenefitCosts.EmployeeBenefitsYearlyBaseCost + (BenefitCosts.DependentBenefitsYearlyBaseCost*2));
        }

        [Theory, AutoMockData]
        public void
            ShouldApplyDependentDeductionWhenDependentNameBeingsWithA(
                Employee employee
            )
        {
            employee.Spouse = null;
            employee.Dependents = new List<Dependent>
            {
                new Dependent
                {
                    FirstName = "Annie"
                }
            };
            employee.FirstName = "Dave";
            var sut = new BenefitsCalculatorService();
            employee = sut.SetEmployeeCostsAndDeductions(employee);
            Assert.Equal(employee.TotalYearlyNetBenefitsCost,
                BenefitCosts.EmployeeBenefitsYearlyBaseCost + (BenefitCosts.DependentBenefitsYearlyBaseCost * (1-BenefitCosts.BeneficiaryYearlyDiscountForLetterA)));
        }
    }
}
