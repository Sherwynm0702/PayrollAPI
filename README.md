# PayrollAPI

A small ASP.NET Core API that works out South African payroll deductions — PAYE, UIF, and SDL — for a list of employees.

> The free tier sleeps when idle, so the first request can take 30–50 seconds to spin up.

I've spent most of my time in React/TypeScript/Node, including building a live payroll module for a hotel client. This is me picking up C# and .NET properly. I rebuilt something I already understand deeply — SA payroll tax logic — so I could focus on learning the new stack rather than a new domain at the same time.

It manages employees (create, read, update, delete), generates a payslip for one of them — gross salary in, PAYE + UIF + net pay out — and works out the employer's monthly SDL liability across the whole payroll. Tax rules (brackets, rebate, UIF ceiling, SDL threshold) live in the database per tax year rather than being hardcoded, so adding a new tax year is a data change, not a code change.

Built with ASP.NET Core 8, EF Core against PostgreSQL, and Swagger for poking at the endpoints without needing Postman. Deployed as a Docker container on Render, with a free Neon Postgres database.

## Running it locally

You'll need the .NET 8 SDK and a PostgreSQL database (a free [Neon](https://neon.tech) project is the easiest way to get one).

```bash
git clone https://github.com/Sherwynm0702/PayrollAPI.git
cd PayrollAPI/PayrollAPI
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=...;Database=...;Username=...;Password=...;SSL Mode=Require;"
dotnet run
```

Migrations run automatically on startup, so the database gets created and seeded the first time it connects. Swagger UI is at `/swagger`.

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

It's a learning project, not something running in real production, so a few things are deliberately left out for now rather than forgotten: there's no auth, so every endpoint is wide open (don't put real people's data in it); no automated tests yet; and tax year data goes in by hand rather than through an admin UI or config file. I'll chip away at these as I keep using the project to learn more of the .NET ecosystem.
