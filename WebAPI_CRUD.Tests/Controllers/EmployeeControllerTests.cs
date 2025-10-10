using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using WebAPI_CRUD.Controllers;
using WebAPI_CRUD.DTOs;
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

    private static EmployeeDto GetSampleEmployee()
        => new()
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@gmail.com",
            DateOfBirth = new DateOnly(1990, 1, 1),
            Gender = Gender.Male,
            DepartmentId = Guid.NewGuid(),
        };

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

    [Fact]
    public void EmployeeController_GetEmployeeByEmail_ThrowsException_WhenRepositoryThrowsException()
    {
        //Arrange
        var email = "";
        A.CallTo(() => _employeeRepository.GetEmployeeByEmail(email)).Throws(new Exception("Database error"));

        //Act
        Action act = () => _employeeController.GetEmployeeByEmail(email).Wait();

        //Assert
        act.Should().Throw<AggregateException>()
            .WithInnerException<Exception>()
            .WithMessage("Database error");
        Assert.Throws<AggregateException>(() => _employeeController.GetEmployeeByEmail(email).Wait());
    }

    [Fact]
    public void EmployeeController_Search_ReturnsOkResult_WhenSearchedByName()
    {
        // Arrange
        string name = "John";
        var employees = A.Fake<IEnumerable<Employee>>();
        A.CallTo(() => _employeeRepository.Search(name, null)).Returns(new List<Employee>());

        // Act
        var result = _employeeController.Search(name, null).Result;

        //Assert
        result.Should().BeOfType<OkObjectResult>();
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void EmployeeController_Search_ReturnsOkResult_WhenNoEmployeesFound()
    {
        // Arrange
        string? name = null;
        A.CallTo(() => _employeeRepository.Search(name, null)).Returns((IEnumerable<Employee>)null);
        // Act
        var result = _employeeController.Search(name, null).Result;
        // Assert
        result.Should().BeOfType<OkObjectResult>();
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void EmployeeController_Search_ReturnsOkResult_WhenSearchedByGender()
    {
        // Arrange
        Gender gender = Gender.Male;
        var employees = A.Fake<IEnumerable<Employee>>();
        A.CallTo(() => _employeeRepository.Search(null, gender)).Returns(employees);

        // Act
        var result = _employeeController.Search(null, gender).Result;

        //Assert
        result.Should().BeOfType<OkObjectResult>();
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void EmployeeController_Search_ReturnOkResult_WhenSearchByNameAndGender()
    {
        // Arrange
        string name = "John";
        Gender gender = Gender.Male;
        var employees = A.Fake<IEnumerable<Employee>>();
        A.CallTo(() => _employeeRepository.Search(name, gender)).Returns(employees);

        // Act
        var result = _employeeController.Search(name, gender).Result;

        //Assert
        result.Should().BeOfType<OkObjectResult>();
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void EmployeeController_Search_ThrowsException_WhenRepositoryThrowsException()
    {
        // Arrange
        string? name = "Sam";
        Gender gender = Gender.Female;
        A.CallTo(() => _employeeRepository.Search(name, gender)).Throws(new Exception("Database Exception"));

        // Act
        Action act = () => _employeeController.Search(name, gender).Wait();

        // Assert
        act.Should().Throw<AggregateException>()
            .WithInnerException<Exception>()
            .WithMessage("Database Exception");
        Assert.Throws<AggregateException>(() => _employeeController.Search(name, gender).Wait());
    }

    [Fact]
    public void EmployeeController_AddEmployee_ReturnsOkRequest()
    {
        // Arrange
        var newEmployee = GetSampleEmployee();
        var createdEmployee = A.Fake<Employee>();
        A.CallTo(() => _employeeRepository.AddEmployee(newEmployee)).Returns(createdEmployee);

        // Act
        var result = _employeeController.AddEmployee(newEmployee).Result;

        // Assert
        result.Should().BeOfType<CreatedAtActionResult>();
        Assert.IsType<CreatedAtActionResult>(result);
    }

    [Fact]
    public void EmployeeController_AddEmployee_ThrowsException_WhenRepositoryThrowsException()
    {
        // Arrange
        var newEmployee = GetSampleEmployee();
        A.CallTo(() => _employeeRepository.AddEmployee(newEmployee)).Throws(new Exception("Database error"));

        // Act
        Action act = () => _employeeController.AddEmployee(newEmployee).Wait();

        // Assert
        act.Should().Throw<AggregateException>()
            .WithInnerException<Exception>()
            .WithMessage("Database error");
        Assert.Throws<AggregateException>(() => _employeeController.AddEmployee(newEmployee).Wait());
    }

    [Fact]
    public void EmployeeController_AddEmployee_ReturnsBadRequest_WhenModelStateIsInvalid()
    {
        // Arrange
        EmployeeDto newEmployee = null;
        
        // Act
        var result = _employeeController.AddEmployee(newEmployee).Result;
        
        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
        Assert.IsType<BadRequestObjectResult>(result);
    }


}
