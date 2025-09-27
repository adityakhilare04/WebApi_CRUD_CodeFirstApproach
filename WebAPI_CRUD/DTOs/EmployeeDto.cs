using WebAPI_CRUD.Models;

namespace WebAPI_CRUD.DTOs
{
    public class EmployeeDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public Guid DepartmentId { get; set; }
    }
}
