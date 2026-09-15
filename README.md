# PayrollAPI

A small ASP.NET Core Web API that calculates South African payroll deductions — PAYE, UIF, and SDL — for a list of employees. I built this to get real hands-on experience with C# and .NET after working mostly in the Node/TypeScript world, and picked payroll because it's a domain I already know well from [LedgerJack](https://github.com/Sherwynm0702) and a live payroll system I built for a client. Rebuilding that logic in a new stack felt like a better way to learn than a generic tutorial project.

## What it does

- Manage employees (create, read, update, delete) with basic validation (required names, positive salary, etc.)
- Generate a payslip for an employee: gross salary in, PAYE + UIF + net pay out
- Calculate the employer's monthly SDL liability across the whole payroll
- Tax rules (brackets, rebate, UIF ceiling, SDL threshold) are stored per tax year in the database rather than hardcoded, so a new tax year can be added without touching the calculation code

## Stack

- **ASP.NET Core 8** (Web API)
- **Entity Framework Core** with **SQL Server** (LocalDB for local dev)
- **Swagger / OpenAPI** for exploring and testing endpoints

## Getting started

**Requirements:** .NET 8 SDK, SQL Server LocalDB (comes with Visual Studio) or any SQL Server instance.

```bash
git clone <repo-url>
cd PayrollAPI/PayrollAPI
dotnet ef database update
dotnet run
```

The API comes up on `https://localhost:<port>` with Swagger UI at `/swagger` — that's the easiest way to try the endpoints without needing a separate client.

The database is seeded with two tax years (2026/27 live rates, plus a 2024/25 set used for testing bracket logic across years), so you can create an employee and hit the payslip endpoint immediately.

## API overview

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/employees` | List all employees |
| GET | `/api/employees/{id}` | Get one employee |
| POST | `/api/employees` | Create an employee |
| PUT | `/api/employees/{id}` | Update an employee |
| DELETE | `/api/employees/{id}` | Delete an employee |
| GET | `/api/employees/{id}/payslip` | Calculate PAYE, UIF, and net pay for an employee |
| GET | `/api/payroll/sdl` | Calculate the employer's monthly SDL liability |

## Known limitations

This is a learning/portfolio project, not a production system, so a few things are intentionally out of scope for now:

- No authentication/authorization — every endpoint is open
- No automated tests yet
- Tax year data is seeded manually rather than sourced from a config or admin UI
- Not yet deployed — runs locally against SQL Server LocalDB

## Why this exists

Most of my production work has been in React/TypeScript/Node, including a live payroll module for a hotel client. This project is the same core problem — SA statutory payroll calculations — solved again in C# and ASP.NET Core, to build real (not just theoretical) exposure to the .NET stack that's common across a lot of the SA enterprise job market.
