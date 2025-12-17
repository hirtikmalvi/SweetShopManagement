
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
> The frontend is **not implemented** yet.
> The API is fully frontend-ready and documented via Scalar OpenAPI.

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

Use this credentials For Admin:
  * `Email` : `hitesh@gmail.com`
  * `Password` : `Hitesh@1234`

---

## 🚀 Implemented API Endpoints

### 🔑 Authentication

* `POST /api/auth/register`
* `POST /api/auth/login`

### 🍬 Sweets Management

* `POST /api/sweets` (Admin)
* `GET /api/sweets`
* `GET /api/sweets/search`
* `PUT /api/sweets/:id` (Admin)
* `DELETE /api/sweets/:id` (Admin)

### 📦 Inventory Management

* `POST /api/sweets/:id/purchase`
* `POST /api/sweets/:id/restock` (Admin)

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
## 📌 API Endpoints & Evidence

> For each endpoint below, screenshots are provided to demonstrate:
>
> * Successful execution
> * Authorization behavior (Admin vs User)

---

## 🔑 Authentication

### `POST /api/auth/register`

**Description**
Registers a new user.

**Access**

* Public

**Screenshots**

* ✅ Successful registration
  📸 [Link](https://drive.google.com/file/d/1edhS9H_T9bSwITMAgtBdgEOhH-yUYBzJ/view?usp=drive_link)
* ❌ Registration with existing email
  📸 [Link](https://drive.google.com/file/d/1oBMkNXGI0wDCt8gymnhdWg9OFTRffQnz/view?usp=drive_link)

---

### `POST /api/auth/login`

**Description**
Authenticates a user and returns a JWT token.

**Access**

* Public

**Screenshots**

* ✅ Login success (JWT returned)
  📸 [Link](https://drive.google.com/file/d/1XjKXkHMfkOOeMf1L5U3YldtocreTnjK4/view?usp=drive_link)
* ❌ Login with invalid credentials
  📸 [Link](https://drive.google.com/file/d/19Tcgv3ZyWHdPSm4Wn56SwOetHKn0I06U/view?usp=drive_link)

---

## 🍬 Sweets Management

---

### `POST /api/sweets` *(Admin only)*

**Description**
Creates a new sweet.

**Access**

* ✅ Admin
* ❌ Normal User

**Screenshots**

* ✅ Admin can create sweet
  📸 [Link](https://drive.google.com/file/d/1gMYESkDCttLCIWSLSENA4tAjQJRY8pJr/view?usp=sharing)
* ❌ Normal user forbidden (403)
  📸 [Link](https://drive.google.com/file/d/1MzvDcbiFfUlo2Oce99Jlk6A3158k508g/view?usp=drive_link)

---

### `GET /api/sweets`

**Description**
Returns all sweets.

**Access**

* ✅ Admin
* ✅ Normal User

**Screenshots**

* ✅ Get sweets (admin/user)
  📸 [Link](https://drive.google.com/file/d/1LD-lH4hYTfRsdn7T-1YNbRKThVh6YZzo/view?usp=drive_link)
---

### `GET /api/sweets/search`

**Description**
Search sweets by name, category, or price range.

**Access**

* ✅ Admin
* ✅ Normal User

**Screenshots**

* ✅ Search by name
  📸 [Link](https://drive.google.com/file/d/1PFixUPj0I3pCTps-Hlpu5de9f-eKTEcR/view?usp=drive_link)
* ✅ Search by category
  📸 [Link](https://drive.google.com/open?id=1WMkDiV3qZoE5PG-9tAfJIBlRsUk15irq&usp=drive_fs)
* ✅ Search with Price Range
  📸 [Link](https://drive.google.com/open?id=13rfootTx0CtAmwapjs86Sw-TZs7JQyJI&usp=drive_fs)
* ✅ Search with no filers
  📸 [Link](https://drive.google.com/open?id=1uT9W1noTyzqHRuLhpB3qsVG7a_3WNdu0&usp=drive_fs)

---

### `PUT /api/sweets/:id (Admin only)`
**Description**
Updates sweet details.

**Access**

* ✅ Admin
* ❌ Normal User

**Screenshots**

* ✅ Admin updates sweet
  📸 [Link](https://drive.google.com/open?id=10sQNDGjp1Xmo9ChDIeRzDC_oyRmGnMb8&usp=drive_fs)
* ❌ Normal user forbidden
  📸 [Link](https://drive.google.com/open?id=1Hj2ibL6ATqCn6-PED0-YSsf9U7TOWULk&usp=drive_fs)
* ❌ Route ID vs Body ID mismatch (400)
  📸 [Link](https://drive.google.com/open?id=1MB2yTQJkZmlzumBML4mi_Lp8BvZbDVuv&usp=drive_fs)

---

### `DELETE /api/sweets/:id (Admin only)`

**Description**
Deletes a sweet.

**Access**

* ✅ Admin
* ❌ Normal User

**Screenshots**

* ✅ Admin deletes sweet
  📸 [Link](https://drive.google.com/open?id=1t4-pWYl4bIuJtT1jB3d1iUUrxjc_kMEJ&usp=drive_fs)
* ❌ Normal user forbidden
  📸 [Link](https://drive.google.com/open?id=1ymmlHFZ4smZ8RbparxCU94zrVwhXUobf&usp=drive_fs)

---

## 📦 Inventory Management

---

### `POST /api/sweets/:id/purchase`

**Description**
Purchases a sweet and reduces stock.

**Access**

* ✅ Admin
* ✅ Normal User

**Screenshots**

* ✅ Purchase success
  📸 [Link](https://drive.google.com/open?id=1Mb8BR3JuDwPRX7RU8ZRmImt2Rn9qLqPh&usp=drive_fs)
* ❌ Insufficient stock
  📸 [Link](https://drive.google.com/open?id=1Ykpj4LjruuldBCQuQzzkAYxHqool_Q8A&usp=drive_fs)
---

### `POST /api/sweets/:id/restock` *(Admin only)*

**Description**
Restocks sweet quantity.

**Access**

* ✅ Admin
* ❌ Normal User

**Screenshots**

* ✅ Admin restocks sweet
  📸 [Link](https://drive.google.com/open?id=1SQXWp49M6Ol6bk9aAh-_QlcEXAZk4hjR&usp=drive_fs)
* ❌ Normal user forbidden
  📸 [Link](https://drive.google.com/open?id=1Vf5aWWGjPaKtI6k7KCERodkiU6ayedyG&usp=drive_fs)
* ❌ Invalid quantity
  📸 [Link](https://drive.google.com/open?id=111tFvUAuISaa-NHMORfoIt3jnpNWqpUS&usp=drive_fs)

---

## 🧪 Test Evidence

**Tests**

* ✅ 29 unit tests passing

📸 Screenshot:
[Link](https://drive.google.com/file/d/1fuPkctWd_kdKHv8R-AvQ5hMvs1orkQc3/view?usp=drive_link)