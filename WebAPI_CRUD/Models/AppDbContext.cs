using Microsoft.EntityFrameworkCore;

namespace WebAPI_CRUD.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var ITDepartmentId = Guid.NewGuid();
            var HRDepartmentId = Guid.NewGuid();
            var FinanceDepartmentId = Guid.NewGuid();

            modelBuilder.Entity<Employee>().HasKey(e => e.Id);
            modelBuilder.Entity<Department>().HasKey(e => e.Id);

            modelBuilder.Entity<Department>().HasData(
            new Department { Id = ITDepartmentId, Name = "IT" });

            modelBuilder.Entity<Department>().HasData(
            new Department { Id = HRDepartmentId, Name = "HR" });

            modelBuilder.Entity<Department>().HasData(
            new Department { Id = FinanceDepartmentId, Name = "Finance" });

            modelBuilder.Entity<Employee>().HasData(
                new Employee {
                    Id = Guid.NewGuid(),
                    FirstName = "Ram",
                    LastName = "Kumar",
                    Email = "Ram.Kumar@gmail.com",
                    DateOfBirth = new DateOnly(1994, 12, 2),
                    Gender = Gender.Male,
                    DepartmentId = ITDepartmentId
                },
                new Employee
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Sham",
                    LastName = "Patil",
                    Email = "Sham.Patil@gmail.com",
                    DateOfBirth = new DateOnly(1998, 2, 23),
                    Gender = Gender.Male,
                    DepartmentId = HRDepartmentId
                },
                new Employee
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Asha",
                    LastName = "Bhosale",
                    Email = "Asha.Bhosale@gmail.com",
                    DateOfBirth = new DateOnly(1999, 3, 17),
                    Gender = Gender.Female,
                    DepartmentId = HRDepartmentId
                },
                new Employee
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Lata",
                    LastName = "Mangeshkar",
                    Email = "Lata.Mangeshkar@gmail.com",
                    DateOfBirth = new DateOnly(1990, 10, 10),
                    Gender = Gender.Female,
                    DepartmentId = ITDepartmentId
                }
            );
        }
    }
}
