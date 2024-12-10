# Restaurant Management API

## Overview

This project is a **Restaurant Management API** designed for managing ingredients, dishes, tables, and orders. Built with **ASP.NET Core MVC 7**, it incorporates **JWT-based authentication** and follows **Onion Architecture** for clean and maintainable code.

---

## Features

### General Functionalities
- **Login and Security:**
  - **Roles:** Administrator, Waiter, and Super Administrator (combines both roles).
  - **JWT Authentication:** Secures endpoints and restricts unauthorized access.
  - Predefined roles and users created by default.

- **User Authentication:**
  - Login to obtain a JWT token for accessing the API.
  - Role-specific access:
    - **Waiter:** Restricted to waiter-specific endpoints.
    - **Administrator:** Restricted to administrator-specific endpoints.
    - Unauthorized access:
      - **401:** If not logged in.
      - **403:** If logged in with insufficient permissions.

### Modules and Endpoints

#### **Ingredient Management**
- **Data:** `Id`, `Name`.
- **Endpoints:**
  - `POST /api/ingredients`: Create a new ingredient.
  - `PUT /api/ingredients/{id}`: Update an ingredient.
  - `GET /api/ingredients`: List all ingredients.
  - `GET /api/ingredients/{id}`: Get an ingredient by ID.

#### **Dish Management**
- **Data:** 
  - `Id`, `Name`, `Price`, `Servings`, `Ingredients`, `Category` (Appetizer, Main Course, Dessert, Beverage).
- **Endpoints:**
  - `POST /api/dishes`: Create a new dish.
  - `PUT /api/dishes/{id}`: Update a dish and manage its ingredients.
  - `GET /api/dishes`: List all dishes.
  - `GET /api/dishes/{id}`: Get a dish by ID.

#### **Table Management**
- **Data:** 
  - `Id`, `Capacity`, `Description`, `Status` (Available, In Service, Attended).
- **Endpoints:**
  - `POST /api/tables`: Create a new table (default status: Available).
  - `PUT /api/tables/{id}`: Update table details.
  - `GET /api/tables`: List all tables.
  - `GET /api/tables/{id}`: Get a table by ID.
  - `GET /api/tables/{id}/orders`: Get active orders for a specific table.
  - `PATCH /api/tables/{id}/status`: Change table status.

#### **Order Management**
- **Data:** 
  - `Id`, `Table`, `Dishes`, `Subtotal`, `Status` (In Process, Completed).
- **Endpoints:**
  - `POST /api/orders`: Create a new order (default status: In Process).
  - `PUT /api/orders/{id}`: Update order details.
  - `GET /api/orders`: List all orders.
  - `GET /api/orders/{id}`: Get an order by ID.
  - `DELETE /api/orders/{id}`: Delete an order.

---

## Technical Details

1. **Architecture:**
   - Onion Architecture for modularity and separation of concerns.
   - Proper implementation of services and repositories.

2. **Authentication:**
   - User management using **ASP.NET Identity**.
   - Endpoint security via **JWT Authentication**.

3. **Validation and Mapping:**
   - **ViewModels** for data validation.
   - **AutoMapper** for mapping between ViewModels, DTOs, and Entities.

4. **Persistence:**
   - Database interaction using **Entity Framework Code-First**.

---

## Getting Started

Follow these instructions to set up the project locally.

### Prerequisites

- [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Visual Studio](https://visualstudio.microsoft.com/) or any preferred IDE for [ASP.NET](http://asp.net/) Core development

### Installation

1. **Clone the repository**:

```bash
git clone https://github.com/rachelyperezdev/Restaurant.git
cd Restaurant
```

1. **Set up the database**:
- Update the `appsettings.json` file with your SQL Server connection string.
- Run the following commands to apply migrations and update the database:

```bash
dotnet ef database update
```

1. **Run the application**:

```bash
dotnet run
```
