# SE Lab Assignment 2 – Exercise 1
## WindowsFormsApp (3-Tier Architecture)

## Setup Instructions
### 1. Database
1. Open **SQL Server Management Studio (SSMS)**
2. Run `Database.sql` – this creates the database, tables, stored procedures and inserts seed data.
### 2. Connection String
Edit `App.config`:
```
<add name="Con" connectionString="Server=(local);Database=OrderDB;Integrated Security=True;" providerName="System.Data.SqlClient" />
```
Change `Server=(local)` to your SQL Server instance name if needed
### 3. Visual Studio
1. Create a new **Windows Forms App (.NET Framework 4.8.1)** project.
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
