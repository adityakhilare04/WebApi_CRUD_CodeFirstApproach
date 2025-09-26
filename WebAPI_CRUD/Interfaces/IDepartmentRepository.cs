using WebAPI_CRUD.Models;

namespace WebAPI_CRUD.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllDepartments();
        Task<Department> GetDepartmentById(Guid id);
        Task<Department> AddDeaprtment(string name);
    }
}
