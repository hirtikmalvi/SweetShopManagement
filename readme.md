
---

# Sweet Shop Management System – Backend API

## 📌 Overview

This project is a **Backend REST API** for managing a Sweet Shop, developed as part of the **Incubyte Practical Assessment**.

The focus of this implementation is on:

* Clean code
* Test Driven Development (TDD)
* Proper separation of concerns
* Business rule correctness
* Maintainable architecture

> ⚠️ **Note**:
> The frontend is intentionally **not implemented**.
> The API is fully frontend-ready and documented via Swagger.

---

## 🧱 Tech Stack

* **Framework**: .NET 9
* **Language**: C#
* **Database**: SQL Server
* **ORM**: Entity Framework Core
* **Authentication**: JWT (Bearer Token)
* **Authorization**: Role-based (Admin / User)
* **Testing**: xUnit + Moq
* **API Documentation**: Scalar API
* **Version Control**: Git

---

## 📂 Project Structure

```
SweetShopManagementSystem
│
├── SweetShop.Api
│   ├── Controllers          # API endpoints
│   ├── Data                 # DbContext & DB configuration
│   ├── DTOs                 # Request / Response contracts
│   ├── Entities             # Domain entities
│   ├── Helpers              # CurrentUserContext, utilities
│   ├── Middleware           # Global exception & auth handling
│   ├── Migrations           # EF Core migrations
│   ├── Repositories
│   │   ├── Interfaces
│   │   └── Implementations
│   ├── Services
│   │   ├── Interfaces
│   │   └── Implementations
│   ├── Shared               # CustomResult<T>
│   ├── appsettings.json
│   └── Program.cs
│
├── SweetShop.Tests
│   └── Services              # Service-level unit tests
│
├── README.md
└── .gitignore
```

---

## 🏗️ Architecture & Design Decisions

### Key Design Principles

* **Controllers are thin**
* **Services contain business logic**
* **Repositories handle persistence only**
* **CustomResult<T>** is used for consistent service responses

### CustomResult Pattern

All services return:

```csharp
CustomResult<T>
{
    Success,
    Data,
    StatusCode,
    Message,
    Errors
}
```

This ensures:

* Consistent API responses
* Easier frontend integration

---

## 🔐 Authentication & Authorization

* JWT-based authentication
* Role is derived from `IsAdmin` boolean
* Role claim mapping:

  * `IsAdmin = true` → `Admin`
  * `IsAdmin = false` → `User`

### Authorization Examples

```csharp
[Authorize]                  // Any authenticated user
[Authorize(Roles = "Admin")] // Admin-only access
```

Custom responses are returned for:

* Unauthorized (401)
* Forbidden (403)

---

## 🚀 Implemented API Endpoints

### 🔑 Authentication

* `POST /api/auth/register`
* `POST /api/auth/login`

### 🍬 Sweets Management

* `POST /api/sweets` (Admin)
* `GET /api/sweets`
* `GET /api/sweets/search`
* `PUT /api/sweets/{id}` (Admin)
* `DELETE /api/sweets/{id}` (Admin)

### 📦 Inventory Management

* `POST /api/sweets/{id}/purchase`
* `POST /api/sweets/{id}/restock` (Admin)

---

## 🧪 Test Driven Development (TDD)

The project strictly follows **Red → Green → Refactor**.

### Testing Strategy

* Only **service layer** is unit tested
* Repositories and external dependencies are **mocked**
* JWT generation is mocked in tests
* No database or HTTP calls in unit tests

### Test Coverage

* Authentication (login, register, JWT)
* Authorization (admin vs user)
* Sweet CRUD operations
* Inventory operations (purchase, restock)
* Validation & error scenarios

### Test Results

* ✅ **29 Tests**
* ❌ 0 Failed
* ⏱ ~706ms execution time

📸 **Screenshot – Test Results**
**[Link](https://drive.google.com/file/d/1fuPkctWd_kdKHv8R-AvQ5hMvs1orkQc3/view?usp=drive_link)**

---

# 🧑‍💻 Running the Project (For First-Time .NET Users)

> This section is written for users who have **never worked with .NET or SQL Server before**.

---

## 1️⃣ Install .NET SDK (Required)

### What is this?

.NET SDK is required to **build and run** the backend API.

### How to install

1. Open:
   👉 [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download)

2. Download and install:

   * **.NET SDK 9.0**

3. Verify installation:

```bash
dotnet --version
```

You should see a version number like:

```
9.0.xxx
```

---

## 2️⃣ Install SQL Server (Choose ONE option)

### ✅ Option A: SQL Server Express (Recommended)

1. Download from:
   👉 [https://www.microsoft.com/en-us/sql-server/sql-server-downloads](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

2. Choose **Express** → Install

3. During setup:

   * Authentication: **Windows Authentication**
   * Note server name (usually `localhost` or `.`)

---

## 3️⃣ Configure Database Connection

Open:

```
SweetShop.Api/appsettings.json
```

Update:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DbConnectionString": "Server=<localhost>\\SQLEXPRESS;Database=SweetShopManagementSystem;Trusted_Connection=True;TrustServerCertificate=True"
  },
  "JwtSettings": {
      "Key": "THIS_IS_SECRET_KEY_FOR_SWEET_SHOP_MANAGEMENT_SYSTEM_432004",
      "Issuer": "SweetShop.Api",
      "Audience": "SweetShop.Client",
      "ExpiryMinutes": 60
   }
}

```
---

## 4️⃣ Create Database Automatically (EF Core)

Run this command from project root:

```bash
dotnet ef database update --project SweetShop.Api
```

### What this does:

* Creates database
* Creates tables
* Applies migrations

---

## 5️⃣ Run the Backend API

```bash
dotnet run --project SweetShop.Api
```

Expected output:

```
Now listening on: https://localhost:xxxx
```

---

## 6️⃣ Open API Documentation (Scalar)

Open browser and visit:

```
https://localhost:xxxx/scalar
```

You should see all API endpoints listed.

---

## 7️⃣ Run Automated Tests (Optional but Recommended)

```bash
dotnet test
```

Expected result:

```
29 Tests Passed
0 Failed
```

---
## 🤖 AI Usage Disclosure

AI tools (ChatGPT) were used **as an assistant** for:

* Architectural discussions
* Test case structuring
* Refactoring guidance

---

## ⚠️ Future Improvements

* Frontend UI not implemented

---

## ✅ Conclusion

This project demonstrates:

* Strong understanding of backend fundamentals
* Practical use of TDD
* Clean architecture & separation of concerns
* Real-world problem solving with EF Core & JWT
---