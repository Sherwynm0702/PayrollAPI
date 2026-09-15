using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PayrollAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedTaxYear2026 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TaxYears",
                columns: new[] { "Id", "EffectiveFrom", "EffectiveTo", "Label", "PrimaryRebate", "SdlAnnualThreshold", "SdlRate", "UifCeiling", "UifRate" },
                values: new object[] { 1, new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2027, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "2026/27", 17820m, 500000m, 0.01m, 17712m, 0.01m });

            migrationBuilder.InsertData(
                table: "TaxBrackets",
                columns: new[] { "Id", "BaseTax", "Rate", "TaxYearId", "Threshold" },
                values: new object[,]
                {
                    { 1, 0m, 0.18m, 1, 0m },
                    { 2, 44118m, 0.26m, 1, 245100m },
                    { 3, 79998m, 0.31m, 1, 383100m },
                    { 4, 125599m, 0.36m, 1, 530200m },
                    { 5, 185215m, 0.39m, 1, 695800m },
                    { 6, 259783m, 0.41m, 1, 887000m },
                    { 7, 666339m, 0.45m, 1, 1878600m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TaxBrackets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TaxBrackets",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TaxBrackets",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TaxBrackets",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TaxBrackets",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TaxBrackets",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TaxBrackets",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "TaxYears",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
