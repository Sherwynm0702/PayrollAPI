using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayrollAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedTaxYear2024Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TaxYears",
                columns: new[] { "Id", "EffectiveFrom", "EffectiveTo", "Label", "PrimaryRebate", "SdlAnnualThreshold", "SdlRate", "UifCeiling", "UifRate" },
                values: new object[] { 2, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "2024/25 (test)", 17235m, 500000m, 0.01m, 5000m, 0.01m });

            migrationBuilder.InsertData(
                table: "TaxBrackets",
                columns: new[] { "Id", "BaseTax", "Rate", "TaxYearId", "Threshold" },
                values: new object[] { 8, 0m, 0.18m, 2, 0m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TaxBrackets",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "TaxYears",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
