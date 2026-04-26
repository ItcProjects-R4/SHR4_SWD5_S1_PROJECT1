# Matamak System

Matamak is a restaurant ordering system built with two main parts:

- `Backend`: A .NET RESTful API responsible for business logic, authentication, database access, and payment integration.
- `Frontend`: A client application that consumes the API and provides the user interface for customers or admins.

At the moment, this repository contains the `Backend` implementation and its supporting layers. The `Frontend` is part of the system concept and can be documented here as the client application connected to this API.

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

The system can be used to:

- Manage menu items and food categories
- Organize items by country or cuisine
- Create takeaway, dine-in, and delivery orders
- Authenticate users securely
- Integrate online payment workflows
- Allow a frontend application to consume and display backend data

## System Components

### Backend

The backend is implemented as an ASP.NET Core Web API and is responsible for:

- Business logic
- Database operations
- Authentication and authorization
- Order processing
- Payment integration with Paymob
- Exposing RESTful endpoints for the frontend

### Frontend

The frontend is the presentation layer of the system and is responsible for:

- Displaying menus and categories
- Allowing users to place and track orders
- Handling login and user interaction
- Consuming the backend API endpoints

Note:
The current repository contains the backend codebase. If you later add a frontend project, this README can be extended with its framework, setup instructions, build commands, and deployment link.

## Features

- Full backend foundation for a restaurant ordering system
- Frontend-ready API with CORS enabled
- Clean architecture with separation between domain, infrastructure, and API layers
- SQL Server integration using Entity Framework Core
- JWT-based authentication setup
- ASP.NET Core Identity integration
- Swagger UI for API testing and documentation
- Support for:
  - Menu items
  - Categories
  - Countries
  - Order items
  - Takeaway orders
  - Dine-in orders
  - Delivery orders
- Paymob payment URL generation

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

### 2. Infrastructure

The `Infrastructure` layer contains implementation details.

It includes:

- `DataContext` for Entity Framework Core
- Repository implementations
- Service implementations
- EF Core migrations
- Identity user model
- Paymob payment integration
- Email sending functionality

### 3. API

The `API` layer is the backend application entry point.

It is responsible for:

- Dependency injection configuration
- Authentication configuration
- Database registration
- Swagger setup
- CORS setup
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
dotnet restore API/Resturant.csproj
```

### 3. Install EF Core CLI Tools

If `dotnet ef` is not available:

```bash
dotnet tool install --global dotnet-ef
```

### 4. Prepare the Database

```bash
dotnet ef database update --project Infrastructure --startup-project API
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

Update `API/appsettings.json` before running the backend.

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

### Frontend Configuration

When the frontend is added, configure its API base URL to point to the backend, for example:

```text
http://localhost:5270
```

## Execution Guide

### Run the Backend Locally

```bash
dotnet run --project API
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
dotnet build API/Resturant.csproj -c Release
```

### Publish the Backend

```bash
dotnet publish API/Resturant.csproj -c Release -o publish
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

The current repository contains DTOs, services, models, and backend configuration that represent the following API areas. The routes below are suitable RESTful endpoint definitions for the frontend to consume.

### Authentication

| Method | Endpoint | Description |
|---|---|---|
| `POST` | `/api/auth/register` | Register a new user |
| `POST` | `/api/auth/login` | Authenticate and return JWT |
| `POST` | `/api/auth/refresh-token` | Refresh an expired token |
| `POST` | `/api/auth/send-activation-code` | Send account activation code |

### Items

| Method | Endpoint | Description |
|---|---|---|
| `GET` | `/api/items` | Get all items |
| `GET` | `/api/items/{id}` | Get item by ID |
| `POST` | `/api/items` | Create a new item |
| `PUT` | `/api/items/{id}` | Update an existing item |
| `GET` | `/api/items/category/{categoryId}` | Get items by category |
| `GET` | `/api/items/country/{countryId}` | Get items by country |
| `GET` | `/api/items/filter?countryId=1&categoryId=2` | Filter items by country and category |

### Delivery Orders

| Method | Endpoint | Description |
|---|---|---|
| `GET` | `/api/delivery-orders` | Get all delivery orders |
| `GET` | `/api/delivery-orders/{orderNumber}` | Get delivery order details |
| `POST` | `/api/delivery-orders` | Create a delivery order |
| `PUT` | `/api/delivery-orders/{orderNumber}` | Update a delivery order |

### Dine-In Orders

| Method | Endpoint | Description |
|---|---|---|
| `GET` | `/api/dinein-orders` | Get all dine-in orders |
| `GET` | `/api/dinein-orders/{orderNumber}` | Get dine-in order details |
| `POST` | `/api/dinein-orders` | Create a dine-in order |
| `PUT` | `/api/dinein-orders/{orderNumber}` | Update a dine-in order |

### Takeaway Orders

| Method | Endpoint | Description |
|---|---|---|
| `GET` | `/api/takeaway-orders` | Get all takeaway orders |
| `GET` | `/api/takeaway-orders/{orderNumber}` | Get takeaway order details |
| `POST` | `/api/takeaway-orders` | Create a takeaway order |
| `PUT` | `/api/takeaway-orders/{orderNumber}` | Update a takeaway order |

### Payments

| Method | Endpoint | Description |
|---|---|---|
| `POST` | `/api/payments/paymob` | Generate a Paymob payment URL |

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
  "orderNumber": 501,
  "orderDate": "2026-04-25T18:30:00",
  "totalPrice": 220.00,
  "deliveryAddress": "12 Tahrir Street, Cairo",
  "contactNumber": "01000000000",
  "customerName": "Ahmed Ali",
  "items": [
    {
      "name": "Chicken Shawarma",
      "priceForOne": 95.00,
      "quantity": 2,
      "note": "Extra garlic",
      "totalPrice": 190.00
    },
    {
      "name": "Cola",
      "priceForOne": 30.00,
      "quantity": 1,
      "note": null,
      "totalPrice": 30.00
    }
  ]
}
```

### Delivery Order Response

```json
{
  "orderNumber": 501,
  "orderDate": "2026-04-25T18:30:00",
  "totalPrice": 220.00,
  "deliveryAddress": "12 Tahrir Street, Cairo",
  "contactNumber": "01000000000",
  "customerName": "Ahmed Ali",
  "items": [
    {
      "name": "Chicken Shawarma",
      "priceForOne": 95.00,
      "quantity": 2,
      "note": "Extra garlic",
      "totalPrice": 190.00
    },
    {
      "name": "Cola",
      "priceForOne": 30.00,
      "quantity": 1,
      "note": null,
      "totalPrice": 30.00
    }
  ]
}
```

## Executable Files and Deployment Link

### Backend Build Output

This backend can be published using:

```bash
dotnet publish API/Resturant.csproj -c Release -o publish
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
|-- API/
|   |-- Program.cs
|   |-- appsettings.json
|   |-- appsettings.Development.json
|   `-- Resturant.csproj
|-- Core/
|   |-- DTO/
|   |-- IReprosatory/
|   |-- IServices/
|   |-- Models/
|   `-- Core.csproj
|-- Infrastructure/
|   |-- Context/
|   |-- Migrations/
|   |-- Reprosatory/
|   |-- Services/
|   `-- Infrastructure.csproj
`-- README.md
```

## Future Improvements

- Add the frontend source code to the repository
- Connect the frontend to all backend endpoints
- Add controller classes for all backend services if not already included in another branch
- Add full CRUD support for categories and countries
- Add validation and centralized error handling
- Add unit tests and integration tests
- Add role-based authorization
- Move secrets to environment variables or user secrets
- Add Docker support and CI/CD pipelines
- Fix naming inconsistencies such as `Delivary`, `Oredr`, and `Reprosatory`

## Author

**Matamak Team**

- Name 1
- Name 2
- Name 3
- Name 4
- Name 5
