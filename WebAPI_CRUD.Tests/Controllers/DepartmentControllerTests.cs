using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using WebAPI_CRUD.Controllers;
using WebAPI_CRUD.Interfaces;
using WebAPI_CRUD.Models;

namespace WebAPI_CRUD.Tests.Controllers;


public class DepartmentControllerTests
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly DepartmentController _departmentController;
    public DepartmentControllerTests()
    {
        _departmentRepository = A.Fake<IDepartmentRepository>();
        _departmentController = new DepartmentController(_departmentRepository);
    }


    [Fact]
    public void DepartmentController_GetAllDepartments_ReturnsOkResult()
    {
        // Arrange
        var departments = A.Fake<IEnumerable<Models.Department>>();
        A.CallTo(() => _departmentRepository.GetAllDepartments()).Returns(departments);
        
        // Act
        var result = _departmentController.GetAllDepartments();

        // Assert
        Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(result.Result);
    }

    [Fact]
    public void DepartmentController_GetAllDepartments_ThrowsException_WhenRepositoryThrowsException()
    {
        // Arrange
        A.CallTo(() => _departmentRepository.GetAllDepartments()).Throws(new Exception("Database error"));
        
        // Act & Assert
        var exception = Assert.ThrowsAsync<Exception>(() => _departmentController.GetAllDepartments());
        Assert.Equal("Database error", exception.Result.Message);
    }

    [Fact]
    public void DepartmentController_GetDepartmentById_ReturnsOkResult()
    {
        // Arrange
        var departmentId = Guid.NewGuid();
        var department = A.Fake<Models.Department>();
        A.CallTo(() => _departmentRepository.GetDepartmentById(departmentId)).Returns(department);
        // Act
        var result = _departmentController.GetDepartmentById(departmentId);
        // Assert
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public void DepartmentController_GetDepartmentById_NotFoundResult()
    {
        //Arrange
        var departmentId = Guid.NewGuid();
        A.CallTo(() => _departmentRepository.GetDepartmentById(departmentId)).Returns((Department?)null);

        //Act
        var result = _departmentController.GetDepartmentById(departmentId).Result;

        //Assert
        Assert.IsType<NotFoundObjectResult>(result);
        result.Should().BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public void DepartmentController_GetDepartmentById_ThrowsException_WhenRepositoryThrowsException()
    {
        // Arrange
        var departmentId = Guid.NewGuid();
        A.CallTo(() => _departmentRepository.GetDepartmentById(departmentId)).Throws(new Exception("Database error"));

        // Act
        Action act = () => _departmentController.GetDepartmentById(departmentId).Wait();

        //Assert
        Assert.Throws<AggregateException>(() =>
            _departmentController.GetDepartmentById(departmentId).Wait());
    }

    [Fact]
    public void DepartmentController_AddDepartment_ReturnsCreatedResult()
    {
        // Arrange
        string name = "HR";
        var department = A.Fake<Department>();
        A.CallTo(() => _departmentRepository.AddDepartment(name)).Returns(department);

        // Act
        var result = _departmentController.AddDepartment(name)?.Result;

        // Assert
        Assert.IsType<CreatedAtActionResult>(result);
        result.Should().BeOfType<CreatedAtActionResult>();
    }

    [Fact]
    public void DepartmentController_AddEmployee_ThrowsException_WhenRepositoryThrowsException()
    {
        // Arrange
        var name = "HR";
        A.CallTo(() => _departmentRepository.AddDepartment(name)).Throws(new Exception("Database error"));

        // Act
        Action act = () => _departmentController.AddDepartment(name).Wait();

        // Assert
        Assert.Throws<AggregateException>(() => _departmentController.AddDepartment(name).Wait());
    }

    [Fact]
    public void DepartmentController_AddDepartment_ReturnsBadRequest()
    {
        // Arrange
        string? name = null;

        // Act
        var result = _departmentController.AddDepartment(name)?.Result;

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);

    }
}
