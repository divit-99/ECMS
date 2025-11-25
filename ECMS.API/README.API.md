# Employee Contact Management System — Backend (.NET 8 Web API + Entity Framework Core 8)

A solid, production-ready backend built with **.NET 8 Web API + Entity Framework Core 8** for the Employee Contact Management System.
Implements authentication, employee/company management, and third-party email validation.

-----------------------------------------------------------------------

## 🚀 Technologies Used
- .NET 8 Web API
- Entity Framework Core 8
- SQL Server
- JWT Authentication
- AutoMapper
- Repository + Service Layer pattern
- Third-Party Email Validation (AbstractAPI)
- Global Filters & Exception Handling Middleware

## 📁 Project Structure
```
db/
 ├── schema.sql
 └── sample-data.sql
ECMS.API/
 ├── Controllers/
 ├── Data/
 ├── DTOs/
 ├── Exceptions/
 ├── Filters/
 ├── Mappings/
 ├── Middlewares/
 ├── Migrations/
 ├── Models/
 ├── Repositories/
 ├── Services/
 ├── Program.cs
 ├── appsettings.json
 └── ECMS.API.csproj
ECMS.UI/
 |
 └── <Details inside README.UI.md file>
```

## ⚙️ Setup Instructions

### 1️⃣ Install Dependencies
```
dotnet restore
```

### 2️⃣ Configure Database (appsettings.json)
```
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=ECMS_DB;Trusted_Connection=True;TrustServerCertificate=True"
}
```

# 🔧 App Settings (Required Sections)

## 📨 AbstractAPI Email Validation
```
"AbstractApi": {
  "ApiKey": "<YOUR_API_KEY>"
}
```

Used in:
`Services/EmailValidationService.cs`

## 🔐 JWT Authentication
```
"Jwt": {
  "Key": "<YOUR_JWT_SECRET>",
  "Issuer": "ECMS.API",
  "Audience": "ECMS.Client"
}
```

## 🌐 CORS Configuration (Program.cs)
```
app.UseCors(builder =>
    builder.WithOrigins("http://localhost:5173")
           .AllowAnyHeader()
           .AllowAnyMethod()
);
```

## 🛢️ Database Setup

### Option A — SQL Scripts
Run in order:
- db/schema.sql  
- db/sample-data.sql

### Option B — EF Migrations
```
dotnet ef database update
```

## 🧰 API Endpoints Overview

### Employees
- GET /api/employees
- POST /api/employees
- PUT /api/employees/{id}
- DELETE /api/employees/{id}

### Companies
- GET /api/companies (used by frontend)
- GET /api/companies/{id}
- POST /api/companies
- PUT /api/companies/{id}
- DELETE /api/companies/{id}

### Auth
- POST /api/auth/login

## 🛡️ Error Handling
- Middlewares/ExceptionHandlingMiddleware.cs  
- Filters/ValidationFilter.cs

## 🔄 Mapping (AutoMapper)
Configured in:
`Mappings/AutoMapperProfile.cs`

## ▶️ Running the API
```
dotnet run
```
Swagger:
```
https://localhost:<port>/swagger
```

--------------------------------