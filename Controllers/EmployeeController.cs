using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoleBasedAuthorizationAPI.Data;
using RoleBasedAuthorizationAPI.Models;

namespace RoleBasedAuthorizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public EmployeeController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetEmployee()
        {
            var employee = await _applicationDbContext.employees.ToListAsync();
            return Ok(employee);
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            _applicationDbContext.employees.AddAsync(employee);
            await _applicationDbContext.SaveChangesAsync();
            return Ok(employee);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> UpdateEmployee(int id, Employee employee)
        {
            var existingEmployee = await _applicationDbContext.employees.FindAsync(id);

            if (existingEmployee == null)
            {
                return NotFound("Employee not found");
            }

            existingEmployee.Name = employee.Name;
            existingEmployee.Department = employee.Department;
            existingEmployee.Email = employee.Email;

            await _applicationDbContext.SaveChangesAsync();

            return Ok(existingEmployee);
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _applicationDbContext.employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound("Employee not found");
            }

            _applicationDbContext.employees.Remove(employee);

            await _applicationDbContext.SaveChangesAsync();

            return Ok("Employee deleted successfully");
        }
    }
}
