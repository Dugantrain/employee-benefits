using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmployeeBenefits.Controllers;
using EmployeeBenefits.Models;
using EmployeeBenefits.Persistence;
using EmployeeBenefits.Services;
using Moq;
using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;
using Xunit;

namespace EmployeeBenefits.Tests.Unit
{
    public class EmployeeControllerTests
    {
        [Theory, AutoMockData]
        public async Task
            ShouldNotAllowUserCreationWhenEmployeeIdentifierExists(
                Employee employee,
                Mock<IEmployeeRepository> mockEmployeeRepository,
                Mock<IBenefitsCalculatorService> mockBenefitsCalculatorService
            )
        {
            employee.Id = null;
            var sut = new EmployeeController(mockEmployeeRepository.Object, mockBenefitsCalculatorService.Object);
            mockEmployeeRepository.Setup(e => e.GetByEmployeeIdentifier(It.IsAny<string>()))
                .ReturnsAsync(new Employee());
            await Assert.ThrowsAsync<Exception>(async () => await sut.Upsert(employee));
        }

        [Theory, AutoMockData]
        public async Task
            ShouldApplyCostsAndDeductionsWhenCreatingEmployee(
                Employee employee,
                Mock<IEmployeeRepository> mockEmployeeRepository,
                Mock<IBenefitsCalculatorService> mockBenefitsCalculatorService
            )
        {
            employee.Id = null;
            var sut = new EmployeeController(mockEmployeeRepository.Object, mockBenefitsCalculatorService.Object);
            mockEmployeeRepository.Setup(e => e.GetByEmployeeIdentifier(It.IsAny<string>()))
                .ReturnsAsync((Employee) null);
            mockBenefitsCalculatorService.Setup(b=>b.SetEmployeeCostsAndDeductions(It.IsAny<Employee>())).Verifiable();
            await sut.Upsert(employee);
            mockBenefitsCalculatorService.VerifyAll();
            mockEmployeeRepository.Verify(e => e.Upsert(It.IsAny<Employee>()), Times.Once());
        }
    }
}
