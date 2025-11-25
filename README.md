# Employee Contact Management System (ECMS)

Full-stack application built using **.NET 8 Web API + Entity Framework Core 8 + React + Vite + TypeScript**.
Allows managing employees & companies with authentication, validation, and a clean-responsive UI.

This README explains the entire project setup, backend + frontend, in one place.

--------------------------------------------------------------------

## 🚀 Technologies Used

### Backend

- .NET 8 Web API
- Entity Framework Core 8
- SQL Server
- AutoMapper
- Repository + Service architecture
- JWT Authentication
- AbstractAPI (Third-party email validation)
- Global exception middleware
- Fluent model validation

### Frontend
- React 18
- Vite
- TypeScript
- Material UI (MUI 5)
- Axios
- React Hook Form
- Custom hook with caching (useCompanies)
- LocalStorage caching

---

## 🧱 Project Structure

ECMS/
 ├── db/
 │    ├── schema.sql
 │    └── sample-data.sql
 |
 ├── ECMS.API/           → Backend (.NET 8 Web API + EF Core)
 │    └── README.API.md
 │
 ├── ECMS.UI/            → Frontend (React + Vite + TS)
 │    └── README.UI.md
 │
 └── README.md           → Root documentation (this file)

---

## 🚀 Backend Features (ECMS.API)

✔ Employee CRUD (List, Create, Update, Delete)
✔ Company CRUD (Full API; UI uses only GetAllCompanies)
✔ Third-party Email Validation (AbstractAPI)
✔ Server-side email validation enforcement
✔ JWT Authentication (Login + Protected Routes)
✔ DTO mapping via AutoMapper
✔ Global Exception Handling Middleware
✔ Validation filter for model errors
✔ EF Core Migrations support
✔ Token generation & validation
✔ Consistent API response models
✔ Swagger documentation enabled

---

## 🎨 Frontend Features (ECMS.UI)

✔ Authentication (Login with JWT)
✔ Protected routes & auto-redirect
✔ Axios interceptor with token injection
✔ Employee CRUD (List, Add, Edit, Delete)
✔ Employee Form modal with validation
✔ Custom hook: useCompanies()
✔ LocalStorage caching for companies list
✔ Disabled Save when companies list empty
✔ Responsive modal UI + mobile-friendly layout
✔ React Hook Form validation
✔ Toast notifications (success/errors)
✔ Search + filter employees
✔ Paginated employee list
✔ Quick Sorting for Name, Email, Job Title, and Company Name columns
✔ Modular folder structure (api/hooks/components/pages)
✔ Full TypeScript typing

---

# 🔧 Backend Setup (ECMS.API)

## 1️⃣ Install dependencies
```
cd ECMS.API
dotnet restore
```

---

## 2️⃣ Configure appsettings.json

### Database
```
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=ECMS_DB;Trusted_Connection=True;TrustServerCertificate=True"
}
```

### Email Validation (AbstractAPI)
```
"AbstractApi": {
  "ApiKey": "<YOUR_API_KEY>"
}
```

### JWT Authentication
```
"Jwt": {
  "Key": "<YOUR_SECRET_KEY>",
  "Issuer": "ECMS.API",
  "Audience": "ECMS.Client"
}
```

---

## 3️⃣ Database Setup

### Option A — Run scripts (recommended)
Run in order:
1. `db/schema.sql`
2. `db/sample-data.sql`

### Option B — Migrations
```
dotnet ef database update
```

---

## 4️⃣ Run the API
```
dotnet run
```

Swagger UI:
```
https://localhost:<port>/swagger
```

---

## 🌐 CORS Note

`Program.cs` allows:
```
http://localhost:5173
```

---

# 🎨 Frontend Setup (ECMS.UI)

## 1️⃣ Install dependencies
```
cd ECMS.UI
npm install
```

---

## 2️⃣ Create `.env`
```
VITE_API_BASE_URL=http://localhost:5211/api
```

---

## 3️⃣ Start the development server
```
npm run dev
```

UI runs at:
```
http://localhost:5173
```

---