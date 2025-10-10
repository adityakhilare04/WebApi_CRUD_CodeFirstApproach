using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using WebAPI_CRUD.DTOs;
using WebAPI_CRUD.Interfaces;
using WebAPI_CRUD.Models;

namespace WebAPI_CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            this._employeeRepository = employeeRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetAllEmployees()
        {
            return Ok(await _employeeRepository.GetEmployees());
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetEmployeeById([FromRoute] Guid id)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployee(id);
                if (employee == null)
                {
                    return NotFound($"Employee with Id:{id} not found.");
                }
                return Ok(employee);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet("{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetEmployeeByEmail([FromRoute] string email)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeByEmail(email);
                if (employee == null)
                {
                    return NotFound($"Employee with email:{email} not found.");
                }
                return Ok(employee);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> Search([FromQuery] string? name, [FromQuery] Gender? gender)
        {
            try
            {
                return Ok(await _employeeRepository.Search(name, gender));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeDto employee)
        {
            try
            {
                if (employee == null)
                {
                    return BadRequest("Employee can't be null.");
                }
                var result = await _employeeRepository.AddEmployee(employee);
                return CreatedAtAction(nameof(GetEmployeeById), new { id = result.Id }, result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteEmployee([FromQuery] Guid id)
        {
            try
            {
                var result = await _employeeRepository.DeleteEmployee(id);
                if (!result)
                {
                    return NotFound($"Employee with Id:{id} not found.");
                }
                return NoContent();
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> UpdateEmployee([FromQuery] Guid id, [FromBody] EmployeeDto employee)
        {
            try
            {
                if (employee == null)
                {
                    return BadRequest("Employee can not be null.");
                }
            
                var result = await _employeeRepository.UpdateEmployee(id, employee);

                if (result == null)
                {
                    return NotFound($"Employee with Id:{id} not found.");
                }
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
