using Microsoft.EntityFrameworkCore;
using PayrollAPI.Data;
using PayrollAPI.Models;


namespace PayrollAPI.Services
{
    public class PayrollCalculator
    {
        private readonly PayrollDbContext _context;

        public PayrollCalculator(PayrollDbContext context)
        {
            _context = context;
        }
        private async Task<TaxYear> GetTaxYearAsync(DateTime payDate)
        {
            var taxYear = await _context.TaxYears
                .Include(ty => ty.Brackets)
                .FirstOrDefaultAsync(ty => payDate >= ty.EffectiveFrom && payDate <= ty.EffectiveTo);

            if (taxYear == null)
            {
                throw new InvalidOperationException($"No tax year configured covering {payDate:yyyy-MM-dd}.");
            }

            return taxYear;
        }
        public async Task<decimal> CalculateUifAsync(decimal grossMonthlySalary, bool uifExempt, DateTime payDate)
        {
            if (uifExempt)
            {
                return 0m;
            }

            var taxYear = await GetTaxYearAsync(payDate);

            decimal uifableSalary = Math.Min(grossMonthlySalary, taxYear.UifCeiling);

            return uifableSalary * taxYear.UifRate;
        }

         private const decimal PrimaryRebate = 17820m;

        public async Task<decimal> CalculateAnnualPayeAsync(decimal annualTaxableIncome, DateTime payDate)
        {
            var taxYear = await GetTaxYearAsync(payDate);

            var orderedBrackets = taxYear.Brackets.OrderBy(b => b.Threshold).ToList();

            var bracket = orderedBrackets[0];

            foreach (var b in orderedBrackets)
            {
                if (annualTaxableIncome >= b.Threshold)
                {
                    bracket = b;
                }
                else
                {
                    break;
                }
            }

            decimal taxBeforeRebate = bracket.BaseTax + (annualTaxableIncome - bracket.Threshold) * bracket.Rate;

            decimal taxAfterRebate = taxBeforeRebate - taxYear.PrimaryRebate;

            return Math.Max(taxAfterRebate, 0m);
        }
        public async Task<decimal> CalculateMonthlySdlAsync(decimal totalMonthlyPayroll, DateTime payDate)
        {
            var taxYear = await GetTaxYearAsync(payDate);

            decimal annualPayroll = totalMonthlyPayroll * 12;

            if (annualPayroll <= taxYear.SdlAnnualThreshold)
            {
                return 0m;
            }

            return totalMonthlyPayroll * taxYear.SdlRate;
        }
    }
}
