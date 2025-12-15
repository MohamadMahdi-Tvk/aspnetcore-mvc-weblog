# ASP.NET Core MVC Weblog

A personal weblog application built with ASP.NET Core MVC using a three-layer architecture and SQL Server.  
This project is intended as a portfolio sample.

## Architecture
The solution is implemented using a three-layer architecture:
- Presentation Layer (ASP.NET Core MVC)
- Business Logic Layer
- Data Access Layer

## Features
- Create, edit and delete blog posts
- Clean MVC structure
- Three-layer architecture
- SQL Server database
- Responsive UI using Bootstrap

## Technologies
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- Bootstrap

## Getting Started

### Prerequisites
- .NET SDK
- SQL Server

### Setup Instructions
1. Clone the repository
2. Open the solution in Visual Studio
3. Update the connection string in `appsettings.json`
4. Open **Package Manager Console**
5. Create the database by running:
   ```powershell
   Add-Migration InitialCreate
   Update-Database
6. Run the project
7. Default Admin Username & Password For Login => UserName: Admin | Password: 123456
