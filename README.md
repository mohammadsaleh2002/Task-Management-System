# Task Management System

A full-stack **Task Management System** built with **ASP.NET Core 8** following **Clean Architecture** principles. The system provides a RESTful API for managing users, projects, and tasks, along with an MVC web frontend. It uses **MongoDB** as the database, **JWT** for authentication, and implements the **CQRS pattern** with **MediatR**.

---

## Table of Contents

- [Architecture](#architecture)
- [Tech Stack](#tech-stack)
- [Project Structure](#project-structure)
- [Features](#features)
- [API Endpoints](#api-endpoints)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Configuration](#configuration)
  - [Running the Application](#running-the-application)
- [Authentication](#authentication)
- [Domain Models](#domain-models)
- [Contributors](#contributors)
- [Development Timeline](#development-timeline)

---

## Architecture

The solution follows **Clean Architecture** with clear separation of concerns across four layers:

```
TaskManagement.Domain          → Entities, Enums (innermost layer, no dependencies)
TaskManagement.Application     → CQRS Commands/Queries, Validators, Interfaces
TaskManagement.Infrastructure  → MongoDB, JWT, Password Hashing (implements Application interfaces)
TaskManagement.Api             → REST API Controllers, Middleware, Swagger
TaskManagement.Web             → MVC Web Frontend (Bootstrap UI)
```

---

## Tech Stack

| Technology | Purpose |
|---|---|
| **ASP.NET Core 8** | Web framework |
| **MongoDB** | NoSQL database |
| **MediatR** | CQRS & Mediator pattern |
| **FluentValidation** | Request validation |
| **JWT Bearer** | Authentication & Authorization |
| **BCrypt** | Password hashing |
| **Serilog** | Structured logging (Console + File) |
| **Swagger / Swashbuckle** | API documentation |
| **Bootstrap 5** | Web frontend UI |

---

## Project Structure

```
TaskManagementSystem.sln
│
├── TaskManagement.Domain/              # Domain Layer
│   ├── Entities/
│   │   ├── User.cs                     # User entity
│   │   ├── Project.cs                  # Project entity
│   │   └── TaskItem.cs                 # Task entity
│   └── Enums/
│       └── TaskItemStatus.cs           # ToDo, InProgress, Completed
│
├── TaskManagement.Application/         # Application Layer
│   ├── Common/
│   │   ├── Behaviors/
│   │   │   └── ValidationBehavior.cs   # MediatR pipeline validation
│   │   ├── Interfaces/
│   │   │   ├── IJwtProvider.cs
│   │   │   └── IPasswordHasher.cs
│   │   └── IMongoDbContext.cs
│   ├── Users/
│   │   ├── Commands/                   # Create, Login, Delete, UpdatePassword
│   │   └── Queries/                    # GetAllUsers
│   ├── Projects/
│   │   ├── Commands/                   # Create, Update, Delete, AddMember
│   │   └── Queries/                    # GetAllProjects
│   ├── Tasks/
│   │   ├── Commands/                   # Create, Delete, UpdateStatus
│   │   └── Queries/                    # GetAll, GetById
│   └── DependencyInjection.cs
│
├── TaskManagement.Infrastructure/      # Infrastructure Layer
│   ├── Authentication/
│   │   ├── JwtProvider.cs              # JWT token generation
│   │   └── PasswordHasher.cs          # BCrypt password hashing
│   ├── Data/
│   │   └── MongoDbContext.cs           # MongoDB collections
│   └── DependencyInjection.cs
│
├── TaskManagement.Api/                 # API Layer
│   ├── Controllers/
│   │   ├── UsersController.cs
│   │   ├── ProjectsController.cs
│   │   └── TasksController.cs
│   ├── Middleware/
│   │   └── ExceptionHandlingMiddleware.cs
│   └── Program.cs
│
└── TaskManagement.Web/                 # Web Frontend (MVC)
    ├── Controllers/
    ├── Views/
    └── wwwroot/
```

---

## Features

- **User Management** — Register, login, change password, delete users
- **Project Management** — Create, update, delete projects and add members
- **Task Management** — Create tasks, assign to users/projects, update status, delete
- **Authentication** — JWT-based authentication with secure password hashing (BCrypt)
- **Authorization** — Protected endpoints using `[Authorize]` attribute
- **Validation** — Request validation via FluentValidation pipeline behavior
- **Error Handling** — Global exception handling middleware with structured error responses
- **Logging** — Structured logging with Serilog (Console + rolling file sinks)
- **API Documentation** — Interactive Swagger UI with JWT Bearer support
- **CQRS Pattern** — Clean command/query separation using MediatR

---

## API Endpoints

### Users (`/api/users`)

| Method | Endpoint | Description | Auth |
|---|---|---|---|
| `POST` | `/api/users` | Register a new user | No |
| `POST` | `/api/users/login` | Login and receive JWT token | No |
| `GET` | `/api/users` | Get all users | No |
| `PUT` | `/api/users/change-password` | Change password | Yes |
| `DELETE` | `/api/users/{id}` | Delete a user | No |

### Projects (`/api/projects`)

| Method | Endpoint | Description | Auth |
|---|---|---|---|
| `POST` | `/api/projects` | Create a new project | No |
| `POST` | `/api/projects/add-member` | Add a member to a project | No |
| `GET` | `/api/projects` | Get all projects | No |
| `PUT` | `/api/projects/{id}` | Update a project | No |
| `DELETE` | `/api/projects/{id}` | Delete a project | No |

### Tasks (`/api/tasks`)

| Method | Endpoint | Description | Auth |
|---|---|---|---|
| `POST` | `/api/tasks` | Create a new task | Yes |
| `GET` | `/api/tasks` | Get all tasks | Yes |
| `GET` | `/api/tasks/{id}` | Get task by ID | Yes |
| `PATCH` | `/api/tasks/{id}/status` | Update task status | Yes |
| `DELETE` | `/api/tasks/{id}` | Delete a task | Yes |

---

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [MongoDB](https://www.mongodb.com/try/download/community) (local or remote instance)

### Configuration

Update the connection string and JWT settings in `TaskManagement.Api/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "MongoDb": "mongodb://localhost:27017/TaskManagementDb"
  },
  "JwtSettings": {
    "Secret": "YourSuperSecretKeyThatIsAtLeast32CharactersLong!",
    "Issuer": "TaskManagementApp",
    "Audience": "TaskManagementUsers",
    "ExpiryMinutes": 60
  }
}
```

### Running the Application

1. **Clone the repository**

```bash
git clone <repository-url>
cd Task-Management-System
```

2. **Restore dependencies**

```bash
dotnet restore
```

3. **Run the API**

```bash
cd TaskManagement.Api
dotnet run
```

The API will be available at:
- **HTTP:** `http://localhost:5013`
- **HTTPS:** `https://localhost:7133`
- **Swagger UI:** `http://localhost:5013/swagger`

4. **Run the Web Frontend** (optional)

```bash
cd TaskManagement.Web
dotnet run
```

---

## Authentication

The API uses **JWT Bearer tokens** for authentication.

1. **Register** a user via `POST /api/users`
2. **Login** via `POST /api/users/login` to receive a JWT token
3. Include the token in the `Authorization` header for protected endpoints:

```
Authorization: Bearer <your-jwt-token>
```

In Swagger UI, click the **Authorize** button and enter your token to authenticate requests.

---

## Domain Models

### User

| Field | Type | Description |
|---|---|---|
| `Id` | `string` | Unique identifier (GUID) |
| `Username` | `string` | Username |
| `Email` | `string` | Email address |
| `PasswordHash` | `string` | BCrypt hashed password |
| `CreatedAt` | `DateTime` | Account creation date |

### Project

| Field | Type | Description |
|---|---|---|
| `Id` | `string` | Unique identifier (GUID) |
| `Name` | `string` | Project name |
| `Description` | `string` | Project description |
| `OwnerId` | `string` | Project manager/owner user ID |
| `MemberIds` | `List<string>` | List of member user IDs |

### TaskItem

| Field | Type | Description |
|---|---|---|
| `Id` | `string` | Unique identifier (GUID) |
| `Title` | `string` | Task title |
| `Description` | `string` | Task description |
| `Status` | `TaskItemStatus` | ToDo, InProgress, or Completed |
| `ProjectId` | `string` | Associated project ID |
| `AssignedUserId` | `string` | Assigned user ID |
| `CreatedAt` | `DateTime` | Task creation date |
| `DueDate` | `DateTime?` | Optional due date |

---

## Contributors

This project was developed collaboratively by two programmers, each taking on distinct roles and responsibilities:

### mohammadsaleh2002 (Mohammad Saleh Mostafaei) 

**Role:** Led the overall backend development, project architecture, database integration, authentication system, and API implementation.

| Area | Contributions |
|---|---|
| **Project Setup** | Initialized project structure with Clean Architecture layers, added `.gitignore` |
| **Database** | Integrated MongoDB driver, implemented `MongoDbContext`, fixed database connection string issues |
| **CQRS & MediatR** | Implemented `CreateProject` feature using CQRS pattern, added DependencyInjection for Application & Infrastructure layers |
| **Entities** | Added `User` entity, refactored User / Project / Task entities |
| **Application Logic** | Implemented `AddMemberToProject` logic, task creation with membership validation, created separate GetAll queries for Users, Projects, and Tasks |
| **Controllers** | Built `ProjectsController`, `UsersController`, modified controllers with `IMongoDbContext` |
| **Authentication** | Implemented auth interfaces (`IPasswordHasher`, `IJwtProvider`), BCrypt password hashing, JWT token generation & login logic |
| **API Configuration** | Finalized `Program.cs` with JWT Authentication & Swagger Security, downgraded JwtBearer package to match .NET 8 framework |
| **CRUD Operations** | Implemented all CRUD methods across Users, Projects, and Tasks |

**Commits:** 20 — spanning project init through full CRUD implementation

---

### saberghm (Saber Gholami) 

**Role:** Focused on domain modeling, input validation, error handling, and logging infrastructure.

| Area | Contributions |
|---|---|
| **Project Initialization** | Initialized solution and projects using .NET 8.0 with Clean Architecture |
| **Domain Entities** | Defined core domain entities: `Project`, `TaskItem`, and `TaskItemStatus` enum |
| **Validation** | Added FluentValidation for Project creation (`CreateProjectValidator`), implemented `CreateTaskValidator` and `CreateUserValidator` |
| **Validation Pipeline** | Completed validation logic by adding `ValidationBehavior` MediatR pipeline behavior |
| **Error Handling** | Implemented Global Exception Handling Middleware (`ExceptionHandlingMiddleware`) for structured error responses |
| **Logging** | Fixed `appsettings.json` configuration, set up Serilog with Console and rolling File sinks |

**Commits:** 6 — focused on domain design, validation, error handling, and logging

---

## Development Timeline

The project was developed on **January 5–7, 2026**. Below is the chronological development flow:

| Phase | Date | Developer | Key Milestones |
|---|---|---|---|
| **1. Project Init** | Jan 5, 06:43 | Mohammad Saleh | `.gitignore`, initial Clean Architecture structure |
| **2. Solution Setup** | Jan 5, 06:58 | Saber | Initialized .NET 8.0 solution and projects |
| **3. Domain Layer** | Jan 5, 07:34 | Saber | Defined `Project`, `TaskItem` entities and `TaskStatus` enum |
| **4. Database Layer** | Jan 5, 07:39–07:51 | Mohammad Saleh | MongoDB driver, `MongoDbContext`, registered in `Program.cs` |
| **5. CQRS Pattern** | Jan 5, 09:07–09:44 | Mohammad Saleh | `CreateProject` with MediatR, DI setup, Swagger testing |
| **6. Entities & Logic** | Jan 5, 09:53–10:02 | Mohammad Saleh | `User` entity, entity refactoring, `AddMemberToProject` |
| **7. Task & User APIs** | Jan 5, 11:05–11:19 | Mohammad Saleh | Task creation with validation, `UsersController` |
| **8. Validation** | Jan 5, 11:34–12:09 | Saber | FluentValidation for Project/Task/User, `ValidationBehavior`, Exception Middleware |
| **9. Authentication** | Jan 5, 12:18–13:45 | Mohammad Saleh | `IPasswordHasher`, BCrypt, `IJwtProvider`, JWT generation, login, Swagger security |
| **10. Logging** | Jan 5, 14:08–14:18 | Both | Serilog integration (Mohammad Saleh initiated, Saber configured) |
| **11. DB Fix & CRUD** | Jan 7, 06:26–07:41 | Mohammad Saleh | Connection string fix, GetAll queries, all CRUD methods |

---

## License

This project is for educational and personal use.

