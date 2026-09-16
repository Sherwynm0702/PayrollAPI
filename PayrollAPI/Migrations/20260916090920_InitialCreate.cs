using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PayrollAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    GrossMonthlySalary = table.Column<decimal>(type: "numeric", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    UifExempt = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaxYears",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "text", nullable: false),
                    EffectiveFrom = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EffectiveTo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PrimaryRebate = table.Column<decimal>(type: "numeric", nullable: false),
                    UifRate = table.Column<decimal>(type: "numeric", nullable: false),
                    UifCeiling = table.Column<decimal>(type: "numeric", nullable: false),
                    SdlRate = table.Column<decimal>(type: "numeric", nullable: false),
                    SdlAnnualThreshold = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxYears", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaxBrackets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Threshold = table.Column<decimal>(type: "numeric", nullable: false),
                    BaseTax = table.Column<decimal>(type: "numeric", nullable: false),
                    Rate = table.Column<decimal>(type: "numeric", nullable: false),
                    TaxYearId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxBrackets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaxBrackets_TaxYears_TaxYearId",
                        column: x => x.TaxYearId,
                        principalTable: "TaxYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TaxYears",
                columns: new[] { "Id", "EffectiveFrom", "EffectiveTo", "Label", "PrimaryRebate", "SdlAnnualThreshold", "SdlRate", "UifCeiling", "UifRate" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2027, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "2026/27", 17820m, 500000m, 0.01m, 17712m, 0.01m },
                    { 2, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "2024/25 (test)", 17235m, 500000m, 0.01m, 5000m, 0.01m }
                });

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
                    { 7, 666339m, 0.45m, 1, 1878600m },
                    { 8, 0m, 0.18m, 2, 0m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaxBrackets_TaxYearId",
                table: "TaxBrackets",
                column: "TaxYearId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "TaxBrackets");

            migrationBuilder.DropTable(
                name: "TaxYears");
        }
    }
}
