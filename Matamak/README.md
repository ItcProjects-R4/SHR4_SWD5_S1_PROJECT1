# Matamak System

Matamak is a restaurant ordering system built with two main parts:

- `Backend`: A .NET RESTful API responsible for business logic, authentication, database access, real-time order updates, and payment integration.
- `Frontend`: A client application that consumes the API and provides the user interface for customers, cashiers, and admins.

At the moment, this repository contains the `Backend` implementation and its supporting layers. The `Frontend` is still not included in this repository.

## Table of Contents

- [Project Overview](#project-overview)
- [System Components](#system-components)
- [Features](#features)
- [Technologies Used](#technologies-used)
- [Architecture](#architecture)
- [Installation](#installation)
- [System Requirements](#system-requirements)
- [Configuration](#configuration)
- [Execution Guide](#execution-guide)
- [API Endpoints](#api-endpoints)
- [API Documentation](#api-documentation)
- [Sample JSON](#sample-json)
- [Executable Files and Deployment Link](#executable-files-and-deployment-link)
- [Folder Structure](#folder-structure)
- [Future Improvements](#future-improvements)
- [Author](#author)

## Project Overview

Matamak is designed to support restaurant operations digitally through a connected backend and frontend experience.

The system can currently be used to:

- Manage menu items and food categories
- Organize items by country or cuisine
- Create dine-in and delivery orders
- Handle user registration, activation, login, refresh token, password reset, and account updates
- Support different roles such as `Admin`, `Cashier`, and `Customer`
- Integrate online payment workflows with Paymob
- Provide real-time order communication using SignalR
- Allow a frontend application to consume and display backend data

The backend also contains takeaway order service and repository logic, but a dedicated takeaway controller has not been added yet.

## System Components

### Backend

The backend is implemented as an ASP.NET Core Web API and is responsible for:

- Business logic
- Database operations
- Authentication and authorization
- User and role management
- Order processing
- Payment integration with Paymob
- Real-time communication through SignalR
- Exposing RESTful endpoints for the frontend

### Frontend

The frontend is the presentation layer of the system and is responsible for:

- Displaying menus and categories
- Allowing users to place and track orders
- Handling login and user interaction
- Consuming the backend API endpoints

Note:
The current repository contains the backend codebase only. If you later add a frontend project, this README can be extended with its framework, setup instructions, build commands, and deployment link.

## Features

- Full backend foundation for a restaurant ordering system
- Frontend-ready API with CORS enabled
- Clean architecture with separation between domain, infrastructure, and API layers
- SQL Server integration using Entity Framework Core
- JWT-based authentication setup
- ASP.NET Core Identity integration
- Swagger UI for API testing and documentation
- SignalR hub for real-time order updates
- Support for:
  - Menu items
  - Categories
  - Countries
  - Order items
  - Dine-in orders
  - Delivery orders
  - Account activation
  - Refresh token flow
  - Forgot/reset password flow
  - Admin, Cashier, and Customer account management
- Paymob payment URL generation
- Daily order counters موجودة في الموديل وقاعدة البيانات
- Takeaway order service/repository logic is present in the backend layer

## Technologies Used

### Backend Technologies

- C#
- ASP.NET Core Web API
- .NET 10
- Entity Framework Core
- SQL Server
- ASP.NET Core Identity
- JWT Bearer Authentication
- Swagger / Swashbuckle
- SignalR
- MailKit / MimeKit
- Paymob integration

### Frontend Technologies

- Frontend framework: `To be added`
- Client-side UI: `To be added`
- API consumption from backend endpoints

## Architecture

The backend follows a clean architecture style with three main layers:

### 1. Core

The `Core` layer contains the business domain and application contracts.

It includes:

- Domain models such as `Item`, `Order`, `DeliveryOrder`, and `DineinOrder`
- DTOs for requests and responses
- Service interfaces
- Repository interfaces
- ModelView classes used by the application

### 2. Infrastructure

The `Infrastructure` layer contains implementation details.

It includes:

- `DataContext` for Entity Framework Core and Identity
- Repository implementations
- Service implementations
- EF Core migrations
- Identity user model
- Paymob payment integration
- Email sending functionality
- SignalR `OrderHub`

### 3. API

The `API` layer is the backend application entry point.

It is responsible for:

- Dependency injection configuration
- Authentication configuration
- Database registration
- Swagger setup
- CORS setup
- SignalR mapping
- Routing and application startup

### Frontend Relationship

The frontend communicates with the backend through HTTP requests to the exposed API endpoints. It acts as the user-facing layer while the backend handles data, security, and application logic.

## Installation

### 1. Clone the Repository

```bash
git clone <your-repository-url>
cd Matamak
```

### 2. Restore Dependencies

```bash
dotnet restore Resturant/Resturant.csproj
```

### 3. Install EF Core CLI Tools

If `dotnet ef` is not available:

```bash
dotnet tool install --global dotnet-ef
```

### 4. Prepare the Database

```bash
dotnet ef database update --project Infrastructure --startup-project Resturant
```

### Frontend Installation

No frontend source folder is included in the current repository.
If a frontend project is added later, include its installation steps here, such as:

```bash
npm install
npm run dev
```

## System Requirements

### Hardware Requirements

- Dual-core processor or better
- Minimum 4 GB RAM
- At least 1 GB free disk space

### Software Requirements

- Windows 10 or Windows 11
- .NET 10 SDK
- SQL Server
- Visual Studio 2022 or later, or Visual Studio Code
- Git
- Optional: Postman for API testing
- Optional: `dotnet-ef` for migrations

### Frontend Requirements

If a frontend is added, it will typically require:

- Node.js
- npm or yarn
- A modern web browser

## Configuration

Update `Resturant/appsettings.json` before running the backend.

### Database Connection

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=ResturantSys;Trusted_Connection=True;MultipleActiveResultSets=true;Trust Server Certificate=True"
}
```

### JWT Settings

```json
"Jwt": {
  "Key": "your-secret-key",
  "Issuer": "http://localhost:5270",
  "Audience": "http://localhost:4200"
}
```

### Paymob Settings

```json
"PaymobSettings": {
  "ApiKey": "your-paymob-api-key",
  "IntegrationId": 0000000,
  "IframeId": 0000000
}
```

### SignalR Hub

The order hub is mapped at:

```text
/orderHub
```

### Frontend Configuration

When the frontend is added, configure its API base URL to point to the backend, for example:

```text
http://localhost:5270
```

## Execution Guide

### Run the Backend Locally

```bash
dotnet run --project Resturant
```

Default local backend URL:

```text
http://localhost:5270
```

Swagger UI:

```text
http://localhost:5270/
```

### Build the Backend

```bash
dotnet build Resturant/Resturant.csproj -c Release
```

### Publish the Backend

```bash
dotnet publish Resturant/Resturant.csproj -c Release -o publish
```

Expected output:

```text
Matamak/publish/
```

### Run or Access the Frontend

No deployed frontend URL or frontend source code is included in this repository yet.
Once available, document it here, for example:

```text
Frontend Local URL: http://localhost:3000
Frontend Production URL: https://your-frontend-domain.com/
```

## API Endpoints

The current repository contains working controllers for the following API areas.

### Authentication / Accounts

| Method | Endpoint | Description |
|---|---|---|
| `POST` | `/api/Account/Customerregister` | Register a new customer |
| `POST` | `/api/Account/manger&cashierRegister` | Create manager or cashier account by admin |
| `POST` | `/api/Account/activeAccount/{email}` | Activate account using verification code |
| `POST` | `/api/Account/login` | Authenticate and return token data |
| `POST` | `/api/Account/refreshToken` | Refresh an expired token |
| `PUT` | `/api/Account/EditAccount/{username}` | Edit account data |
| `PUT` | `/api/Account/ChangePassword/{username}` | Change account password |
| `DELETE` | `/api/Account/DeleteMyAccount/{username}` | Delete current account |
| `DELETE` | `/api/Account/DeleteAnyAccount/{username}` | Delete any account by admin |
| `POST` | `/api/Account/ForgotPassword` | Send forgot-password code |
| `POST` | `/api/Account/VerifyForgetPasswordCode/{email}` | Verify forgot-password code |
| `PUT` | `/api/Account/ResetPassword/{email}` | Reset password |
| `GET` | `/api/Account/GetAllAdmins` | Get all admins |
| `GET` | `/api/Account/GetAdminByUsername/{username}` | Get admin by username |
| `GET` | `/api/Account/GetAllCashiers` | Get all cashiers |
| `GET` | `/api/Account/GetCashierByUsername/{username}` | Get cashier by username |
| `GET` | `/api/Account/GetAllCustomers` | Get all customers |
| `GET` | `/api/Account/GetCustomerByUsername/{username}` | Get customer by username |

### Items

| Method | Endpoint | Description |
|---|---|---|
| `GET` | `/api/Item/getAllItem` | Get all items |
| `GET` | `/api/Item/getItemById/{id}` | Get item by ID |
| `GET` | `/api/Item/sortItems?countryId=1&categoryId=2` | Filter items by country and category |
| `POST` | `/api/Item/addItem` | Create a new item |
| `PUT` | `/api/Item/updateItem/{id}` | Update an existing item |
| `DELETE` | `/api/Item/removeItem` | Remove an item |

### Categories

| Method | Endpoint | Description |
|---|---|---|
| `GET` | `/api/Category/getCategoryById` | Get category by ID |
| `GET` | `/api/Category/getAllCategories` | Get all categories |
| `POST` | `/api/Category/addCategory` | Add a category |
| `DELETE` | `/api/Category/removeCategory` | Remove a category |
| `PUT` | `/api/Category/editCategory` | Edit a category |

### Countries

| Method | Endpoint | Description |
|---|---|---|
| `POST` | `/api/Country/addCountry` | Add a country |
| `PUT` | `/api/Country/editCountry` | Edit a country |
| `DELETE` | `/api/Country/removeCountry` | Remove a country |
| `GET` | `/api/Country/getCountryById` | Get country by ID |
| `GET` | `/api/Country/getAllCountries` | Get all countries |

### Delivery Orders

| Method | Endpoint | Description |
|---|---|---|
| `GET` | `/api/DeliveryOrder/getDeliveryOrders` | Get all delivery orders |
| `GET` | `/api/DeliveryOrder/getDeliveryOrderById/{id}` | Get delivery order details |
| `POST` | `/api/DeliveryOrder/addDelveryOrder` | Create a delivery order |
| `PUT` | `/api/DeliveryOrder/updateDeliveryOrder/{id}` | Update a delivery order |
| `PUT` | `/api/DeliveryOrder/cancelDeliveryOrder/{id}` | Cancel a delivery order |
| `PUT` | `/api/DeliveryOrder/handOrderToDriver/{id}` | Mark order as handed to driver |
| `PUT` | `/api/DeliveryOrder/handOrderToCustomer/{id}` | Mark order as handed to customer |
| `DELETE` | `/api/DeliveryOrder/removeDeliveryOrder/{id}` | Remove a delivery order |

### Dine-In Orders

| Method | Endpoint | Description |
|---|---|---|
| `GET` | `/api/DineinOrder/getAllDineinOrders` | Get all dine-in orders |
| `GET` | `/api/DineinOrder/getDineinOrder/{id}` | Get dine-in order details |
| `POST` | `/api/DineinOrder/addDineinOrder` | Create a dine-in order |
| `PUT` | `/api/DineinOrder/updateDineinOrder/{id}` | Update a dine-in order |
| `PUT` | `/api/DineinOrder/ChangeDineinOrderStatus/{id}` | Change dine-in order status |
| `DELETE` | `/api/DineinOrder/removeDineinOrder/{id}` | Remove a dine-in order |

### Payments

| Method | Endpoint | Description |
|---|---|---|
| `POST` | `/api/Payment/pay/{orderId}` | Generate a Paymob payment URL |

### Real-Time

| Type | Endpoint | Description |
|---|---|---|
| `SignalR Hub` | `/orderHub` | Real-time communication for order updates |

### Takeaway Orders

Takeaway order contracts and service/repository code exist in the backend layers, but there is currently no exposed controller endpoint in the `Resturant` API project.

## API Documentation

Swagger is available for local backend testing and API exploration.

- Swagger UI: `http://localhost:5270/`
- Swagger JSON: `http://localhost:5270/swagger/v1/swagger.json`

This documentation is useful for frontend developers to test requests and understand payload formats.

## Sample JSON

### Login Request

```json
{
  "email": "user@example.com",
  "password": "P@ssw0rd123"
}
```

### Create Item Request

```json
{
  "name": "Chicken Shawarma",
  "description": "Grilled chicken wrap with garlic sauce",
  "price": 95.00,
  "imageUrl": "https://example.com/images/shawarma.jpg",
  "catogryId": 1,
  "countryId": 2,
  "isAvailable": true
}
```

### Item Response

```json
{
  "id": 12,
  "name": "Chicken Shawarma",
  "description": "Grilled chicken wrap with garlic sauce",
  "price": 95.00,
  "imageUrl": "https://example.com/images/shawarma.jpg",
  "catogryId": 1,
  "countryId": 2,
  "isAvailable": true
}
```

### Create Delivery Order Request

```json
{
  "address": "12 Tahrir Street, Cairo",
  "phoneNumber": "01000000000",
  "note": "Extra garlic",
  "items": [
    {
      "itemId": 1,
      "quantity": 2
    },
    {
      "itemId": 4,
      "quantity": 1
    }
  ]
}
```

### Payment Response

```json
{
  "paymentUrl": "https://accept.paymob.com/..."
}
```

## Executable Files and Deployment Link

### Backend Build Output

This backend can be published using:

```bash
dotnet publish Resturant/Resturant.csproj -c Release -o publish
```

Expected published output:

```text
Matamak/publish/
```

Typical output files:

- `Resturant.dll`
- `Resturant.deps.json`
- `Resturant.runtimeconfig.json`

### Frontend Build Output

No frontend build artifact is included in this repository yet.
When a frontend project is added, you can document its production output here, such as:

- `dist/`
- `build/`
- deployed web URL

### Deployment Links

No public deployment links are included in the current repository.
Replace the placeholders below when deployment is available:

```text
Backend Production URL: https://your-api-domain.com/
Backend Swagger URL: https://your-api-domain.com/swagger
Frontend Production URL: https://your-frontend-domain.com/
```

## Folder Structure

```text
Matamak/
|-- Core/
|   |-- DTO/
|   |-- IReprosatory/
|   |-- IServices/
|   |-- Models/
|   |-- ModelView/
|   `-- Core.csproj
|-- Infrastructure/
|   |-- Context/
|   |-- Migrations/
|   |-- Reprosatory/
|   |-- Services/
|   `-- Infrastructure.csproj
`-- Resturant/
    |-- Controllers/
    |-- Properties/
    |-- Program.cs
    |-- appsettings.json
    |-- appsettings.Development.json
    |-- Resturant.csproj
    |-- Resturant.http
    |-- Resturant.slnx
    `-- README.md
```

## Future Improvements

- Add the frontend source code to the repository
- Add a dedicated `TakeAwayOrderController`
- Connect the frontend to all backend endpoints
- Add validation and centralized error handling
- Add unit tests and integration tests
- Add role-based authorization refinements
- Move secrets to environment variables or user secrets
- Add Docker support and CI/CD pipelines
- Fix naming inconsistencies such as `Delivary`, `Oredr`, and `Reprosatory`
- Complete payment flow using real order data instead of temporary values

## Author

**Matamak Team**

- Name 1
- Name 2
- Name 3
- Name 4
- Name 5
