using Microsoft.EntityFrameworkCore;
using WebAPI_CRUD.Interfaces;
using WebAPI_CRUD.Models;

namespace WebAPI_CRUD.Implementations
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;
        public EmployeeRepository(AppDbContext context)
        {
            this._context = context;
        }
        public async Task<Employee> AddEmployee(Employee employee)
        {
            var result = await _context.AddAsync(employee);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> DeleteEmployee(Guid id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (employee == null)
            { 
                return false;
            }
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return Task.CompletedTask.IsCompletedSuccessfully;
        }

        public async Task<Employee> GetEmployee(Guid id)
        {
            return await _context.Employees
                .Include(x => x.Department)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Employee> GetEmployeeByEmail(string email)
        {
            return await _context.Employees.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<IEnumerable<Employee>> Search(string name, Gender? gender)
        {
            var query = _context.Employees.AsEnumerable();
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => name.Contains(x.FirstName) || name.Contains(x.LastName));
            }
            if(gender != null)
            {
                query = query.Where(x => x.Gender == gender);
            }
            await Task.CompletedTask;
            return query;
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            var existingEmployee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == employee.Id);
            if (existingEmployee != null)
            {
                existingEmployee.Email = employee.Email;
                existingEmployee.FirstName = employee.FirstName;
                existingEmployee.LastName = employee.LastName;
                existingEmployee.Gender = employee.Gender;
                existingEmployee.DateOfBirth = employee.DateOfBirth;
                existingEmployee.DepartmentId = employee.DepartmentId;

                var result = _context.Employees.Update(existingEmployee);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            return null;
        }
    }
}
