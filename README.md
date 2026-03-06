# BeaconFlow

BeaconFlow is a lightweight event ingestion API built with **.NET 8** using **Clean Architecture principles**.  
It demonstrates how to structure a service that receives, validates, and stores events while maintaining strict separation between domain logic, application workflows, infrastructure, and the API layer.

The project is intentionally small but designed with production-grade architectural patterns.

---

# Features

- Event ingestion via REST API
- Event severity classification
- Retrieval of ingested events
- Health check endpoint
- Clean Architecture structure
- Domain-driven validation
- Unit tests for Domain and Application layers
- In-memory repository for fast local development

---

# Architecture

BeaconFlow follows a layered architecture that keeps business rules independent from infrastructure.
API
↓
Application
↓
Domain
↑
Infrastructure

## Layers

### BeaconFlow.Domain
Contains the **core business rules**.

Examples:
- `EventRecord`
- `EventSeverity`
- `DomainException`

The domain layer **does not depend on any other layer**.

---

### BeaconFlow.Application

Implements **use cases** and application workflows.

Examples:

- `IngestEventCommand`
- `IngestEventHandler`
- `IEventRepository`

This layer coordinates domain behavior but **does not know about infrastructure**.

---

### BeaconFlow.Infrastructure

Contains **implementations of interfaces defined in Application**.

Current implementation:

- `InMemoryEventRepository`

This allows the infrastructure to be swapped later (database, event stream, etc.).

---

### BeaconFlow.Api

Exposes the system via HTTP.

Controllers:

- `EventsController`
- `HealthController`

---

# Project Structure
BeaconFlow
│
├── src
│ ├── BeaconFlow.Api
│ ├── BeaconFlow.Application
│ ├── BeaconFlow.Domain
│ └── BeaconFlow.Infrastructure
│
├── tests
│ ├── BeaconFlow.Domain.Tests
│ └── BeaconFlow.Application.Tests
│
├── BeaconFlow.sln
└── global.json
---

# API Endpoints
## Health Check

Exapmle:
curl http://localhost:5067/api/health

Response:

```json
{
  "status": "ok",
  "service": "BeaconFlow",
  "utc": "2026-03-06T18:55:03Z"
}

Ingest Event
POST /api/events

Example:
curl -X POST http://localhost:5067/api/events \
-H "Content-Type: application/json" \
-d '{"message":"system started","severity":"Info"}'

Response:
{
  "id": "6bb4952f-cb99-445e-95de-2859c3543ece"
}

Supported severities:
Info
Warning
Error
Critical

Retrieve Events:
GET /api/events

Example:
curl http://localhost:5067/api/events

Response:
[
  {
    "id": "6bb4952f-cb99-445e-95de-2859c3543ece",
    "message": "system started",
    "severity": "Info",
    "timestamp": "2026-03-06T18:55:03Z"
  }
]

Running Locally
Clone the repository:
git clone https://github.com/YOUR_USERNAME/BeaconFlow.git
cd BeaconFlow

Restore dependencies:
dotnet restore BeaconFlow.sln

Build the solution:
dotnet build BeaconFlow.sln

Run tests:
dotnet test BeaconFlow.sln

Run the API:
dotnet run --project src/BeaconFlow.Api/BeaconFlow.Api.csproj

## API Documentation
Swagger UI is available locally at:
http://localhost:5067/swagger

Testing
BeaconFlow includes unit tests for:
Domain rules
Application workflows
Run all tests:
dotnet test BeaconFlow.sln

Future Improvements
Possible extensions:
PostgreSQL or EventStore persistence
SignalR real-time event streaming
Event filtering and querying
Authentication and authorization
Structured logging and observability

License
This project is licensed under the Apache License 2.0.
See the LICENSE file for details.

