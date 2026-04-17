# \# SE Lab Assignment 2 – Exercise 1

# \## WindowsFormsApp (3-Tier Architecture)

# 

# \## Setup Instructions

# \### 1. Database

# 1\. Open \*\*SQL Server Management Studio (SSMS)\*\*

# 2\. Run `Database.sql` – this creates the database, tables, stored procedures and inserts seed data.

# \### 2. Connection String

# Edit `App.config`:

# ```

# <add name="Con" connectionString="Server=(local);Database=OrderDB;Integrated Security=True;" providerName="System.Data.SqlClient" />

# ```

# Change `Server=(local)` to your SQL Server instance name if needed

# \### 3. Visual Studio

# 1\. Create a new \*\*Windows Forms App (.NET Framework 4.8.1)\*\* project.

# 2\. Set `Program.cs` as startup object.

# 3\. Build and Run (`F5`).



