using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayrollAPI.Data;
using PayrollAPI.Models;
using Microsoft.EntityFrameworkCore;
using PayrollAPI.Dtos;
using PayrollAPI.Services;

namespace PayrollAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly PayrollDbContext _context;
        private readonly PayrollCalculator _calculator;
        public EmployeesController(PayrollDbContext context, PayrollCalculator calculator)
        {
            _context = context;
            _calculator = calculator;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return employee;
        }
        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(CreateEmployeeDto dto)
        {
            var employee = new Employee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                GrossMonthlySalary = dto.GrossMonthlySalary,
                DateOfBirth = dto.DateOfBirth,
                UifExempt = dto.UifExempt
            };
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }
            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // GET: api/employees/5/payslip
        [HttpGet("{id}/payslip")]
        public async Task<ActionResult> GetPayslip(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            decimal annualSalary = employee.GrossMonthlySalary * 12;
            DateTime payDate = DateTime.UtcNow.Date;
            decimal annualPaye = await _calculator.CalculateAnnualPayeAsync(annualSalary, payDate);
            decimal monthlyPaye = annualPaye / 12;
            decimal monthlyUif = await _calculator.CalculateUifAsync(employee.GrossMonthlySalary, employee.UifExempt, payDate);
            decimal netPay = employee.GrossMonthlySalary - monthlyPaye - monthlyUif;

            return Ok(new
            {
                employee.FirstName,
                employee.LastName,
                GrossMonthlySalary = employee.GrossMonthlySalary,
                MonthlyPaye = Math.Round(monthlyPaye, 2),
                MonthlyUif = Math.Round(monthlyUif, 2),
                NetPay = Math.Round(netPay, 2)
            });
        }
        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
