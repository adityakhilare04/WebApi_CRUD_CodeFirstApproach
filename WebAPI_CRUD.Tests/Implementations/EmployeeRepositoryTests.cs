using Microsoft.EntityFrameworkCore;
using WebAPI_CRUD.DTOs;
using WebAPI_CRUD.Implementations;
using WebAPI_CRUD.Models;

namespace WebAPI_CRUD.Tests.Implementations;

public class EmployeeRepositoryTests
{
    private static AppDbContext GetInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        return new AppDbContext(options);
    }

    private static EmployeeDto GetSampleEmployee()
        => new()
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@gmail.com",
            DateOfBirth = new DateOnly(1999, 04, 12),
            DepartmentId = Guid.NewGuid(),
            Gender = Gender.Male
        };

    [Fact]
    public async Task EmployeeRepository_AddEmployee_ShouldAddEmployee()
    {
        // Arrange
        var context = GetInMemoryContext();
        var repository = new EmployeeRepository(context);
        var newEmployee = GetSampleEmployee();

        // Act
        var result = await repository.AddEmployee(newEmployee);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Employee>(result);
        Assert.Equal("John", result.FirstName);
        Assert.NotEqual(Guid.Empty, result.Id);

    }

    [Fact]
    public async Task EmployeeRepository_DeleteEmployee_ShouldDeleteEmployee()
    {
        // Arrange
        var context = GetInMemoryContext();
        var repository = new EmployeeRepository(context);
        var newEmployee = GetSampleEmployee();
        var entity = await repository.AddEmployee(newEmployee);

        // Act
        var result = await repository.DeleteEmployee(entity.Id);

        // Assert
        Assert.IsType<bool>(result);
        Assert.True(result);
    }
}
