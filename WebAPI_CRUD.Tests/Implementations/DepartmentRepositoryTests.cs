using Microsoft.EntityFrameworkCore;
using WebAPI_CRUD.Implementations;
using WebAPI_CRUD.Models;

namespace WebAPI_CRUD.Tests.Implementations;

public class DepartmentRepositoryTests
{
    private static AppDbContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        return new AppDbContext(options);
    }

    [Fact]
    public async Task DepartmentRepository_AddDepartment_ShouldAddDepartment()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var repository = new DepartmentRepository(context);
        string departmentName = "HR";
        // Act
        var result = await repository.AddDepartment(departmentName);
        // Assert
        Assert.NotNull(result);
        Assert.Equal(departmentName, result.Name);
        Assert.NotEqual(Guid.Empty, result.Id);
    }

    [Fact]
    public async Task DepartmentRepository_GetDepartmentById_ShouldReturnDepartment()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var repository = new DepartmentRepository(context);
        string departmentName = "Finance";
        var addedDepartment = await repository.AddDepartment(departmentName);
        // Act
        var result = await repository.GetDepartmentById(addedDepartment.Id);
        // Assert
        Assert.NotNull(result);
        Assert.Equal(departmentName, result.Name);
        Assert.Equal(addedDepartment.Id, result.Id);
    }
    [Fact]
    public async Task DepartmentRepository_GetAllDepartments_ShouldReturnAllDepartments()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var repository = new DepartmentRepository(context);
        string departmentName1 = "IT";
        string departmentName2 = "Marketing";
        await repository.AddDepartment(departmentName1);
        await repository.AddDepartment(departmentName2);
        // Act
        var result = await repository.GetAllDepartments();
        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Contains(result, d => d.Name == departmentName1);
        Assert.Contains(result, d => d.Name == departmentName2);
    }
    [Fact]
    public async Task DepartmentRepository_GetDepartmentById_ShouldReturnNullForNonExistentId()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var repository = new DepartmentRepository(context);
        // Act
        var result = await repository.GetDepartmentById(Guid.NewGuid());
        // Assert
        Assert.Null(result);
    }
    
}
