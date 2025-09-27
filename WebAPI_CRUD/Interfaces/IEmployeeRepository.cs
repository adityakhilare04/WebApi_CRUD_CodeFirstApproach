using WebAPI_CRUD.DTOs;
using WebAPI_CRUD.Models;

namespace WebAPI_CRUD.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> Search(string? name, Gender? gender);
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployee(Guid id);
        Task<Employee> GetEmployeeByEmail(string email);
        Task<Employee> AddEmployee(EmployeeDto employee);
        Task<Employee> UpdateEmployee(Guid id, EmployeeDto employee);
        Task<bool> DeleteEmployee(Guid id);
    }
}
