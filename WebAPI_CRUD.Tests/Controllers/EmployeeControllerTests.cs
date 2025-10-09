using FakeItEasy;
using WebAPI_CRUD.Controllers;
using WebAPI_CRUD.Interfaces;

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

    }

}
