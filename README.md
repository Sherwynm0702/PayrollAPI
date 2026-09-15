# PayrollAPI

A small ASP.NET Core API that works out South African payroll deductions — PAYE, UIF, and SDL — for a list of employees.

I've spent most of my time in React/TypeScript/Node, including building a live payroll module for a hotel client. This is me picking up C# and .NET properly, and instead of doing yet another to-do list tutorial, I rebuilt something I already understand deeply — SA payroll tax logic — so I could focus on learning the new stack rather than a new domain at the same time.

It manages employees (create, read, update, delete), generates a payslip for one of them — gross salary in, PAYE + UIF + net pay out — and works out the employer's monthly SDL liability across the whole payroll. Tax rules (brackets, rebate, UIF ceiling, SDL threshold) live in the database per tax year rather than being hardcoded, so adding a new tax year is a data change, not a code change.

Built with ASP.NET Core 8, EF Core against SQL Server (LocalDB locally), and Swagger for poking at the endpoints without needing Postman.

## Running it

You'll need the .NET 8 SDK and SQL Server LocalDB (comes with Visual Studio, or grab it separately).

```bash
git clone <repo-url>
cd PayrollAPI/PayrollAPI
dotnet ef database update
dotnet run
```

That spins up the API on `https://localhost:<port>` with Swagger UI at `/swagger` — easiest way to try it without wiring up a frontend. The database seeds itself with two tax years (2026/27 live rates, plus an older 2024/25 set I used to sanity-check the bracket logic across years), so you can create an employee and hit the payslip endpoint straight away.

## Endpoints

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/employees` | List all employees |
| GET | `/api/employees/{id}` | Get one employee |
| POST | `/api/employees` | Create an employee |
| PUT | `/api/employees/{id}` | Update an employee |
| DELETE | `/api/employees/{id}` | Delete an employee |
| GET | `/api/employees/{id}/payslip` | Work out PAYE, UIF, and net pay for an employee |
| GET | `/api/payroll/sdl` | Work out the employer's monthly SDL liability |

## What's missing

It's a learning project, not something running in production, so a few things are deliberately left out for now rather than forgotten: there's no auth, so every endpoint is wide open; no automated tests yet; tax year data goes in by hand rather than through an admin UI or config file; and it's not deployed anywhere — local-only, against SQL Server LocalDB. I'll chip away at these as I keep using the project to learn more of the .NET ecosystem.
