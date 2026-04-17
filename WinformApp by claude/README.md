# SE Lab Assignment 2 – Exercise 1
## Windows Forms Application (3-Tier Architecture)

---

## Project Structure

```
WinformApp/
│
├── SQL/
│   └── DatabaseSetup.sql       ← Run this first in SQL Server
│
├── DTO/
│   └── Models.cs               ← Data Transfer Objects (ItemDTO, AgentDTO, …)
│
├── DAL/                        ← Data Access Layer
│   ├── DBConnection.cs         ← SqlConnection factory (reads App.config)
│   ├── UserDAL.cs
│   ├── ItemDAL.cs
│   ├── AgentDAL.cs
│   └── OrderDAL.cs             ← Includes OrderDetail logic + transactions
│
├── BLL/                        ← Business Logic Layer
│   └── BusinessLogic.cs        ← UserBLL, ItemBLL, AgentBLL, OrderBLL
│
├── UI/                         ← Presentation Layer (WinForms)
│   ├── LoginForm.cs            ← Login → MainForm
│   ├── MainForm.cs             ← Navigation hub
│   ├── ItemForm.cs             ← Item CRUD (search, add, edit, delete)
│   ├── AgentForm.cs            ← Agent CRUD
│   ├── OrderForm.cs            ← Master-Detail Order form (one-to-many)
│   ├── OrderReportForm.cs      ← Print preview + PrintDocument
│   └── FilterForm.cs           ← 4-tab filter: best items, items/agent, agents/item, summary
│
├── App.config                  ← Connection string (edit Server name here)
└── Program.cs                  ← Application entry point
```

---

## Setup Instructions

### 1. Database
1. Open **SQL Server Management Studio (SSMS)**
2. Run `SQL/DatabaseSetup.sql` – this creates the database,
   tables, stored procedures and inserts seed data.

### 2. Connection String
Edit `App.config`:
```xml
<add name="OrderDB"
     connectionString="Server=YOUR_SERVER;Database=OrderManagementDB;
                       Integrated Security=True;"
     providerName="System.Data.SqlClient" />
```
Change `Server=.` to your SQL Server instance name if needed
(e.g. `Server=DESKTOP-ABC\SQLEXPRESS`).

### 3. Visual Studio
1. Create a new **Windows Forms App (.NET Framework 4.7.2)** project.
2. Copy all `.cs` files into the project (keep the folder structure).
3. Add `App.config` entries.
4. Install **System.Data.SqlClient** via NuGet if not already present.
5. Set `Program.cs` as startup object.
6. Build and Run (`F5`).

---

## Default Login Credentials

| Username | Password |
|----------|----------|
| admin    | 123456   |
| alice    | 123456   |
| bob      | 123456   |

---

## Features Implemented

| Feature | Form |
|---------|------|
| Login with MD5 hashed password | LoginForm |
| Main navigation dashboard | MainForm |
| Item CRUD with search | ItemForm |
| Agent CRUD with search | AgentForm |
| Order master-detail (one-to-many) | OrderForm |
| Order print report + PrintDocument | OrderReportForm |
| Best selling items (Top N) | FilterForm – Tab 1 |
| Items purchased by a specific agent | FilterForm – Tab 2 |
| Agents who purchased a specific item | FilterForm – Tab 3 |
| Agent purchase summary | FilterForm – Tab 4 |

---

## Architecture Overview

```
┌───────────────────────────────────────────┐
│              UI Layer (WinForms)          │
│  LoginForm  MainForm  ItemForm  ...       │
└─────────────────────┬─────────────────────┘
                      │  calls
┌─────────────────────▼─────────────────────┐
│           Business Logic Layer (BLL)      │
│  UserBLL   ItemBLL   AgentBLL  OrderBLL   │
└─────────────────────┬─────────────────────┘
                      │  calls
┌─────────────────────▼─────────────────────┐
│           Data Access Layer (DAL)         │
│  UserDAL   ItemDAL   AgentDAL  OrderDAL   │
│              DBConnection                 │
└─────────────────────┬─────────────────────┘
                      │  ADO.NET SqlClient
┌─────────────────────▼─────────────────────┐
│           SQL Server Database             │
│  OrderManagementDB                        │
└───────────────────────────────────────────┘
```
