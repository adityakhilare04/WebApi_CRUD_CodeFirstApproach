using Microsoft.EntityFrameworkCore;
using WebAPI_CRUD.Interfaces;
using WebAPI_CRUD.Models;

namespace WebAPI_CRUD.Implementations
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _context;
        public DepartmentRepository(AppDbContext context)
        {
            this._context = context;
        }
        public async Task<Department> AddDeaprtment(string name)
        {
            Department department = new Department()
            {
                Id = Guid.NewGuid(),
                Name = name,
            };
            var result = await _context.AddAsync(department);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<IEnumerable<Department>> GetAllDepartments()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<Department> GetDepartmentById(Guid id)
        {
            return await _context.Departments.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
