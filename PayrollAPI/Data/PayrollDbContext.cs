using Microsoft.EntityFrameworkCore;
using PayrollAPI.Models;

namespace PayrollAPI.Data
{
    public class PayrollDbContext : DbContext
    {
        public PayrollDbContext(DbContextOptions<PayrollDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<TaxYear> TaxYears { get; set; }
        public DbSet<TaxBracket> TaxBrackets { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaxYear>().HasData(new TaxYear
            {
                Id = 1,
                Label = "2026/27",
                EffectiveFrom = DateTime.SpecifyKind(new DateTime(2026, 3, 1), DateTimeKind.Utc),
                EffectiveTo = DateTime.SpecifyKind(new DateTime(2027, 2, 28), DateTimeKind.Utc),
                PrimaryRebate = 17820m,
                UifRate = 0.01m,
                UifCeiling = 17712m,
                SdlRate = 0.01m,
                SdlAnnualThreshold = 500000m
            });

            modelBuilder.Entity<TaxBracket>().HasData(
                new TaxBracket { Id = 1, TaxYearId = 1, Threshold = 0m, BaseTax = 0m, Rate = 0.18m },
                new TaxBracket { Id = 2, TaxYearId = 1, Threshold = 245100m, BaseTax = 44118m, Rate = 0.26m },
                new TaxBracket { Id = 3, TaxYearId = 1, Threshold = 383100m, BaseTax = 79998m, Rate = 0.31m },
                new TaxBracket { Id = 4, TaxYearId = 1, Threshold = 530200m, BaseTax = 125599m, Rate = 0.36m },
                new TaxBracket { Id = 5, TaxYearId = 1, Threshold = 695800m, BaseTax = 185215m, Rate = 0.39m },
                new TaxBracket { Id = 6, TaxYearId = 1, Threshold = 887000m, BaseTax = 259783m, Rate = 0.41m },
                new TaxBracket { Id = 7, TaxYearId = 1, Threshold = 1878600m, BaseTax = 666339m, Rate = 0.45m }
            );
            modelBuilder.Entity<TaxYear>().HasData(new TaxYear
            {
                Id = 2,
                Label = "2024/25 (test)",
                EffectiveFrom = DateTime.SpecifyKind(new DateTime(2024, 3, 1), DateTimeKind.Utc),
                EffectiveTo = DateTime.SpecifyKind(new DateTime(2025, 2, 28), DateTimeKind.Utc),
                PrimaryRebate = 17235m,
                UifRate = 0.01m,
                UifCeiling = 5000m,
                SdlRate = 0.01m,
                SdlAnnualThreshold = 500000m
            });

            modelBuilder.Entity<TaxBracket>().HasData(
                new TaxBracket { Id = 8, TaxYearId = 2, Threshold = 0m, BaseTax = 0m, Rate = 0.18m }
            );
        }

    }
}
