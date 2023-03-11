using AuthApi.Context;
using AuthApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentDbContext _departmentContext;

        public DepartmentController(DepartmentDbContext departmentDbContext)
        {
            _departmentContext = departmentDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Departments>>> getDepartments()
        {
            if(_departmentContext == null)
            {
                return NotFound();
            }

            return await _departmentContext.Departments.ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> saveDepartment(Departments departments)
        {
            if(departments == null)
            {
                return BadRequest();
            }

            await _departmentContext.AddAsync(departments);
            await _departmentContext.SaveChangesAsync();
            return Ok(new { Message = "Department Added" });
        }

        [HttpPost("updateDepartment")]
        public async Task<IActionResult> UpdateDepartment(Departments departments)
        {
            if (departments == null)
            {
                return BadRequest();
            }

            var existingDepartment = await _departmentContext.Departments.FindAsync(departments.Id);

            if (existingDepartment == null)
            {
                return NotFound();
            }

            // Update the department properties
            existingDepartment.Name = departments.Name;

            // Save the changes to the database
            await _departmentContext.SaveChangesAsync();

            return Ok(new
            {
                Maessage = "Department Updated"
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            // Retrieve the department from the database
            var department = await _departmentContext.Departments.FindAsync(id);


            if (department == null)
            {
                return NotFound();
            }

            // Remove the department from the database
            _departmentContext.Departments.Remove(department);

            // Save the changes to the database
            await _departmentContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
