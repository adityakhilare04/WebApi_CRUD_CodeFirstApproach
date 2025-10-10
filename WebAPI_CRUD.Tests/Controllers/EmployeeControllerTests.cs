using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using WebAPI_CRUD.Controllers;
using WebAPI_CRUD.Interfaces;
using WebAPI_CRUD.Models;

namespace WebAPI_CRUD.Tests.Controllers;

public class EmployeeControllerTests
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly EmployeeController _employeeController;
    public EmployeeControllerTests()
    {
        _employeeRepository = A.Fake<IEmployeeRepository>();
        _employeeController = new EmployeeController(_employeeRepository);
    }

    [Fact]
    public void EmployeeController_GetAllEmployees_ReturnsOkResult()
    {
        //Arrange
        var employees = A.Fake<IEnumerable<Employee>>();
        A.CallTo(() => _employeeRepository.GetEmployees()).Returns(employees);

        //Act
        var result = _employeeController.GetAllEmployees();

        //Assert
        result.Should().BeOfType<Task<IActionResult>>();    // Using FluentAssertions
        Assert.IsType<Task<IActionResult>>(result);     // Using xUnit Assert
    }

    [Fact]
    public void EmployeeController_GetEmployeeById_ReturnsOkResult_WhenEmployeeExists()
    {
        //Arrange
        var employeeId = Guid.NewGuid();
        var employee = A.Fake<Employee>();
        A.CallTo(() => _employeeRepository.GetEmployee(employeeId)).Returns(employee);
        //Act
        var result = _employeeController.GetEmployeeById(employeeId).Result;
        //Assert
        result.Should().BeOfType<OkObjectResult>();    // Using FluentAssertions
        Assert.IsType<OkObjectResult>(result);     // Using xUnit Assert
    }

    [Fact]
    public void EmployeeController_GetEmployeeById_ReturnsNotFoundResult_WhenEmployeeDoesNotExist()
    {
        //Arrange
        var employeeId = Guid.NewGuid();
        A.CallTo(() => _employeeRepository.GetEmployee(employeeId)).Returns((Employee?)null);
        //Act
        var result = _employeeController.GetEmployeeById(employeeId).Result;
        //Assert
        result.Should().BeOfType<NotFoundObjectResult>();    // Using FluentAssertions
        Assert.IsType<NotFoundObjectResult>(result);     // Using xUnit Assert
    }

    [Fact]
    public void EmployeeCOntroller_GetEmployeeByEmail_ReturnEmployee()
    {
        //Arrange
        var email = "existingEmployee@gmail.com";
        var employee = A.Fake<Employee>();
        A.CallTo(() => _employeeRepository.GetEmployeeByEmail(email)).Returns(employee);

        //Act
        var result = _employeeController.GetEmployeeByEmail(email).Result;

        //Assert
        result.Should().BeOfType<OkObjectResult>();
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void EmployeeController_GetEmployeeByEmail_ReturnsNotFoundResult_WhenEmployeeDoesNotExist()
    {
        //Arrange
        var email = "nonExistingEmployee@gmail.com";
        A.CallTo(() => _employeeRepository.GetEmployeeByEmail(email)).Returns((Employee?)null);

        //Act
        var result = _employeeController.GetEmployeeByEmail(email).Result;

        //Assert
        result.Should().BeOfType<NotFoundObjectResult>();
        Assert.IsType<NotFoundObjectResult>(result);
    }


    [Fact]
    public void EmployeeController_GetEmployeeById_ThrowsException_WhenRepositoryThrowsException()
    {
        //Arrange
        var employeeId = Guid.NewGuid();
        A.CallTo(() => _employeeRepository.GetEmployee(employeeId)).Throws(new Exception("Database error"));
        //Act
        Action act = () => _employeeController.GetEmployeeById(employeeId).Wait();
        //Assert
        act.Should().Throw<AggregateException>()
            .WithInnerException<Exception>()
            .WithMessage("Database error");
    }
}
