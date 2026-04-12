# TaskManager Portfolio Solution

A .NET 10 multi-project solution demonstrating Clean Architecture pattern with REST API and gRPC service integration for ToDo task management.

## Project Structure

### 1. **Domain Layer** (`Domain/`)
Define core business entities and value types independent of technology.

- **EntityModels:** `ToDoTask`.
- **Enums:** `ToDoTaskStatus` enum (TODO=1, IN_PROGRESS=2, DONE=3...).
- **No dependencies:** Pure C#, no EF Core, no gRPC.

### 2. **Application Layer** (`Application/`)
Implement business logic, coordinate between presentation and infrastructure, define contracts.

- **Interfaces:** `IToDoTaskService` — business operations (list, fetch, create, update, delete).
- **Services:** `ToDoTaskService` — orchestrates repository calls, applies filters, returns DTOs.
- **DTOs:** `ToDoTaskDTO` (for transfer), `GetToDoTaskRequest` (for filtering).
- **DI:** `AddApplication()` registers services in the container.
- **Dependencies:** Domain Layer.

### 3. **Infrastructure Layer** (`Infrastructure/`)
Handle data persistence, database context, repository pattern.

- **Repositories:** `IToDoTaskRepository` implementation using EF Core; methods: `GetByIdAsync`, `AddAsync`, `GetWhereAsync`, `UpdateAsync`, `DeleteByIdAsync`.
- **DbContext:** `ApplicationDbContext` — EF Core configuration, migrations, DbSet<ToDoTask>.
- **DI:** `AddInfrastructure()` registers repositories and DbContext.
- **Dependencies:** Domain Layer, Application Layer, ORM (EF Core NuGet packages).

### 4. **Presentation Layer** (`Presentation/`)
Expose business logic via user-facing interfaces (REST API and gRPC in this case).

#### **GrpcService** (Server)
- **Program.cs:**
  - Starts Kestrel on `https://localhost:5001` with HTTP/2 (required for gRPC).
  - Registers DI: `AddApplication()`, `AddInfrastructure()`, `AddGrpc()`.
  - Maps gRPC service: `app.MapGrpcService<ToDoTaskGrpcService>()`.
  
- **ToDoTaskGrpcService.cs:**
  - Implements proto-generated base class `ToDoTaskService.ToDoTaskServiceBase`.
  - RPC methods: `GetById`, `Add`, `Update`, `Delete`.
  - Adapts proto messages (`AddRequest`, `ToDoTask`, etc.) ↔ application DTOs.
  - ToDo: Handles validation, error mapping to gRPC statuses (InvalidArgument, NotFound, Internal), implement all CRUD operations.
  - Injects and calls `IToDoTaskService` from Application layer.

- **Protos/todotask.proto:**
  - Defines gRPC service contract: service `ToDoTaskService` with 4 RPCs.
  - Message types: `ToDoTask`, `AddRequest`, `AddResponse`, `GetByIdRequest`, `UpdateRequest`, `DeleteRequest`.
  - Enum: `Status` (maps to `Domain.Enums.Status`).
  - Namespace: `GrpcService` (C# namespace for generated code).

#### **GrpcClient** (Consumer)
- **Program.cs:**
  - Creates channel to `https://localhost:5001`.
  - Calls RPC methods (e.g., `AddAsync`).
  - Prints results. 
  - ToDo: Implement calls to all CRUD operations.

#### **WebApi** (REST API Server)

- **Controllers/ToDoTaskController.cs:**
  - RESTful endpoints: `GET /api/todotasks`, `GET /api/todotasks/{id}`, `POST /api/todotasks`, `PUT /api/todotasks/{id}`, `DELETE /api/todotasks/{id}`.
  - Adapts HTTP requests/responses ↔ application DTOs.
  - Handles validation, HTTP status codes (200, 201, 400, 404, 500).
  - Injects and calls `IToDoTaskService` from Application layer.
  - Swagger/OpenAPI documentation via `AddSwaggerGen()`.