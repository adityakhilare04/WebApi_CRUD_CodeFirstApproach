using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using WebAPI_CRUD.Interfaces;

namespace WebAPI_CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            this._departmentRepository = departmentRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetAllDepartments()
        {
            return Ok(await _departmentRepository.GetAllDepartments());
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetDepartmentById([FromRoute] Guid id)
        {
            var department = await _departmentRepository.GetDepartmentById(id);
            if (department == null)
            {
                return NotFound($"Department with Id:{id} not found.");
            }
            return Ok(department);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> AddDepartment([FromQuery] string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    return BadRequest("Department name cannot be null or empty");
                }
                var department = await _departmentRepository.AddDepartment(name);
                return CreatedAtAction(nameof(GetDepartmentById), new { id = department.Id }, department);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
