using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayrollAPI.Data;
using PayrollAPI.Services;

namespace PayrollAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayrollController : ControllerBase
    {
        private readonly PayrollDbContext _context;
        private readonly PayrollCalculator _calculator;

        public PayrollController(PayrollDbContext context, PayrollCalculator calculator)
        {
            _context = context;
            _calculator = calculator;
        }
        [HttpGet("sdl")]
        public async Task<ActionResult> GetSdl()
        {
            decimal totalMonthlyPayroll = await _context.Employees.SumAsync(e => e.GrossMonthlySalary);

            int employeeCount = await _context.Employees.CountAsync();

            DateTime payDate = DateTime.Today;
            decimal monthlySdl = await _calculator.CalculateMonthlySdlAsync(totalMonthlyPayroll, payDate);

            return Ok(new
            {
                EmployeeCount = employeeCount,
                TotalMonthlyPayroll = totalMonthlyPayroll,
                EstimatedAnnualPayroll = totalMonthlyPayroll * 12,
                SdlApplies = monthlySdl > 0,
                MonthlySdl = Math.Round(monthlySdl, 2)
            });
        }
    }
}
