# Employee Contact Management System — Frontend (React + TypeScript + MUI)

A responsive, clean UI built with **React + Vite + TypeScript + Material UI** for the Employee Contact Management System.
Handles employee CRUD, company selection, authentication, and backend-driven email validation.

-----------------------------------------------------------------------

## 🚀 Features (Frontend)

### 🔐 Authentication
- Login & Signup screen
- Form validation using Yup + React Hook Form
- Snackbar-based toast notifications
- AuthContext for managing JWT + user information
- Protected routes with automatic redirects

### 👥 Employee Management
- View paginated employee list
- Search employees by name, or email
- Add new employees
- Edit employee details
- Delete employees, with confirmation dialog
- Real-time validation on form fields
- Auto-refresh after operations

### 🏢 Company Dropdown
- Fetch company list from a custom hook
- Pre-loaded in EmployeeFormModal
- Dropdown to select company for each employee
- Dropdown disabled when no companies available
- Save/Add buttons disabled automatically
- Prevents invalid request

### 🎯 UX & UI
- Fully responsive layout (mobile + tablet + desktop)
- MUI components for consistent styling
- Centered authentication box with smooth height transitions
- Debounced search for optimized API calls
- Toast notifications for all actions
- ErrorBoundary + fallback UI for unexpected failures

### 🔧 Custom Hook — useCompanies()
- Fetch company list from API
- Cache response using localStorage
- Provide loading + error states
- Auto-refresh on mount

### 🗃️ Caching
- Reduced GetCompanies API call everytime Add/Edit pop-up is opened
- Faster navigation
- Works offline for company list
- Safe auto-reload behavior

---

## ▶️ Getting Started

### 1. Install dependencies

npm install

### 2. Create .env file

Update backend API endpoint in `.env` file variable:

'VITE_API_BASE_URL=http://localhost:7899/api'

### 3. Start development server

npm run dev

The app will open at: http://localhost:5173

---

## 🧱 Tech Stack

### **React**
- Functional components
- Custom hooks (Auth context, API calls)
- React Router v6

### **TypeScript**
- Strongly typed props, components, DTOs, contexts, API functions
- Validation schemas with inferred types

### **Material UI**
- Theme-based styling
- Grid, Table, Dialog, Modal, Snackbar, Buttons, TextFields
- Mobile-responsive UI

### **React Hook Form + Yup**
- Schema-based validation
- Clean form architecture

### **Axios**
- API communication through a shared axiosInstance
- Authorization header auto-attached through interceptors

---

## 📂 Project Structure

ECMS.UI/
│
├── ecms-ui/                    # Root React project
|   |
│   ├── .env                    # Environment variables (e.g., VITE_API_BASE_URL)
|   |
│   ├── node_modules/           # Installed npm packages
|   |
│   ├── public/                 # Static public assets (e.g., vite.svg)
│   │
│   └── src/                    # Main source code
│       │
│       ├── api/                # All API calls (Axios services)
│       │   └── auth.api.ts
│       │   └── company.api.ts
│       │   └── employee.api.ts
│       │
│       ├── assets/             # Images, logos, fonts, global static files to be here
│       │
│       ├── components/         # Reusable UI components
│       │   ├── auth/           # Login/Signup Auth form component
│       │   ├── common/         # Shared reusable components (Pagination, SearchBar, etc.)
│       │   ├── employee/       # Employee-related UI elements (List, etc.)
│       │   ├── error/          # Error Boundary component (404, fallback UI)
│       │   └── layout/         # Navbar wrapper
│       │
│       ├── context/            # React Context (e.g., AuthContext)
│       │
│       ├── hooks/              # Custom React hooks (e.g., useCompanies)
│       │
│       ├── layout/             # Page-level layout components (e.g., AppLayout)
│       │
│       ├── pages/              # Route-level pages
│       │   ├── Auth/           # Login, Signup Auth page
│       │   ├── Employees/      # Dashboard page
│       │   └── Error/          # Error page
│       │
│       ├── routes/             # ProtectedRoute / PrivateRoute / Route guards
│       │
│       ├── theme/              # appTheme (e.g., MUI theme, color palette, typography, breakpoints)
│       │
│       ├── types/              # TypeScript interfaces, DTOs, models (e.g., auth.types, employee.types)
│       │
│       ├── utils/              # Helper methods, mappers, formatters, constants (e.g., axiosInstance, mapEmployee)
│       │
│       ├── validations/        # Yup validation schemas / custom validators
│       │
│       ├── App.tsx             # Root App component
│       ├── main.tsx            # ReactDOM root + ThemeProvider / Context providers
│       └── index.css           # Global CSS (resets, fonts)
│
└────────────────────────────────────────────────────────────────────────────────────

---

## 📡 API Integration

The UI communicates with a .NET Web API backend using axios:

- `GET /api/Employee`
- `POST /api/Employee`
- `PUT /api/Employee/{id}`
- `DELETE /api/Employee/{id}`
- `GET /api/Company`
- `POST /api/Auth/login`
- `POST /api/Auth/signup`

A global `axiosInstance` handles:
- Base URL
- JWT injection into Authorization header
- Error propagation

---

## 🔒 Authentication Flow

1. User logs in or signs up
2. Backend returns JWT
3. JWT is stored in localStorage
4. AuthContext exposes:
   - `isAuthenticated`
   - `user`
   - `logout()`
   - `login()`
5. ProtectedRoute checks if JWT exists before rendering secure pages

---

## 📱 Responsiveness

- Auth box scales for mobile screens
- Employee table scrolls horizontally on small devices
- Modals adapt to screen size
- Layout spacing adjusts automatically using MUI breakpoints

---

## 🧪 Validation

### Login / Signup
- Required fields
- Email format check
- Password min-length

### Employee Form
- FullName required
- Email format required
- Phone number required
- JobTitle required
- Company required
- Backend validation errors shown via toast

---

## 📌 Environment Variables

Update backend API endpoint in `.env` file variable:

'VITE_API_BASE_URL'

--------------------------------------------------------------------------------