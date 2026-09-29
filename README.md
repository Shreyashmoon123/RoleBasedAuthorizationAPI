# RoleBasedAuthorizationAPI

A practical **ASP.NET Core Web API** project demonstrating **JWT Authentication and Role-Based Authorization** using ASP.NET Core Identity.

The project implements three roles — **Admin, Manager, and User** — with different permissions for Employee management.

## 🚀 Features

* JWT Authentication
* ASP.NET Core Identity
* Role-Based Authorization
* Multiple Roles
* Admin, Manager & User roles
* Employee CRUD operations
* Role-specific API access
* Password hashing using ASP.NET Core Identity
* Entity Framework Core
* SQL Server
* Swagger/OpenAPI
* JWT Bearer Token authentication
* 401 Unauthorized & 403 Forbidden handling

## 🛠️ Technologies Used

* C#
* .NET 10
* ASP.NET Core Web API
* ASP.NET Core Identity
* JWT
* Entity Framework Core
* SQL Server
* Swagger/OpenAPI

## 🔐 Authorization Rules

| Operation       | Admin | Manager | User |
| --------------- | :---: | :-----: | :--: |
| View Employees  |   ✅   |    ✅    |   ✅  |
| Add Employee    |   ✅   |    ✅    |   ❌  |
| Update Employee |   ✅   |    ✅    |   ❌  |
| Delete Employee |   ✅   |    ❌    |   ❌  |

## 🔑 Authentication Flow

```text
Register
   ↓
User + Role stored in Identity
   ↓
Login
   ↓
JWT Token Generated
   ↓
Role Claim Added to Token
   ↓
Token sent with API Request
   ↓
[Authorize] checks Authentication
   ↓
Role Authorization checks Permissions
   ↓
Access Granted / 403 Forbidden
```

## 📂 Project Structure

```text
RoleBasedAuthorizationAPI
│
├── Controllers
│   ├── AuthController.cs
│   └── EmployeeController.cs
│
├── Data
│   └── ApplicationDbContext.cs
│
├── DTOs
│   ├── LoginDto.cs
│   └── RegisterDto.cs
│
├── Models
│   ├── ApplicationUser.cs
│   └── Employee.cs
│
├── Services
│   └── TokenServices.cs
│
├── Program.cs
└── appsettings.json
```

## ⚙️ Setup

### 1. Clone the repository

```bash
git clone https://github.com/Shreyashmoon123/RoleBasedAuthorizationAPI.git
cd RoleBasedAuthorizationAPI
```

### 2. Configure SQL Server

Update the connection string in `appsettings.json` according to your local SQL Server configuration.

Example:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost,1433;Database=RoleBasedAuthorizationDB;User Id=sa;Password=YOUR_PASSWORD;TrustServerCertificate=True;"
}
```

### 3. Configure JWT

Add your JWT configuration:

```json
"Jwt": {
  "Key": "YOUR_SECRET_KEY",
  "Issuer": "RoleBasedAuthorizationAPI",
  "Audience": "RoleBasedAuthorizationAPIUsers"
}
```

> Do not commit real database passwords or production JWT secrets to GitHub.

### 4. Apply migrations

```bash
dotnet ef database update
```

### 5. Run the project

```bash
dotnet run
```

### 6. Open Swagger

Open the Swagger URL shown in the terminal, for example:

```text
https://localhost:xxxx/swagger
```

## 🧪 Testing

The APIs can be tested using **Swagger/OpenAPI**.

### Register

```http
POST /api/Auth/register
```

### Login

```http
POST /api/Auth/login
```

The login API returns a JWT token containing the user's role claim.

Use the token in Swagger's **Authorize** button:

```text
Bearer YOUR_JWT_TOKEN
```

Then test the Employee APIs according to the user's role.

## 📌 Key Concepts Demonstrated

### Authentication

Verifies **who the user is** using JWT.

### Authorization

Determines **what the authenticated user is allowed to do**.

### Role-Based Authorization

Uses roles to protect API endpoints:

```csharp
[Authorize(Roles = "Admin")]
```

Multiple roles can be allowed:

```csharp
[Authorize(Roles = "Admin,Manager")]
```

### HTTP Status Codes

```text
401 Unauthorized
→ Missing or invalid authentication token

403 Forbidden
→ User is authenticated but does not have the required role
```

## 🎯 Purpose

This project was created to gain practical understanding of:

* JWT Authentication
* ASP.NET Core Identity
* Role Management
* Role Claims
* Role-Based Authorization
* Protected API endpoints
* Secure Employee CRUD APIs

## 👨‍💻 Author

**Shreyash Katiyar**

.NET Developer | ASP.NET Core | C# | Web API | MVC | EF Core | SQL Server
