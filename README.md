
## Architecture

1. **SuperTask.Domain**: Contains structural, core business model. Pure C# with no framework dependencies.
2. **SuperTask.Application**: Orchestrates use cases, application business operations, and interface definitions.
3. **SuperTask.Data / Infrastructure**: Holds database implementation models (`TaskListDocument`), repositories (`MongoTaskListRepository`), data serialization settings, and automatic test seed mechanics.
4. **SuperTask.Api**: The entry point providing the web interface using ASP.NET Core controllers, error handling middleware, and custom header request interception.

## Possible improvments (Require investigation)

1. Consider the issue of indexes in a MongoDB database
2. Add Custom Exceptions for difference cases
3. Think about adding value objects to Domain layer
4. Consider adding value objects to the domain layer
5. Consider improving the Pagination to add TotalCount, IfNextPage and other Meta.
6. Enable XML Documentation for readeble swagger 

---

## Prerequisites

Before starting up the application stack, ensure the following local software assets are active in your runtime environment:
* **.NET 8.0 SDK** (For native execution and code compilation)
* **Docker & Docker Compose** (For orchestrating application and MongoDB containers)

---

## Quick Start: Running the API

The easiest way to initialize the database and start up the api is by using Docker Compose.

### 1. Fire up the Stack via Docker Compose
Open a terminal in the root solution folder (where your `compose.yaml` or `docker-compose.yml` resides) and issue a forced rebuild command:
```bash
docker compose up -d --build
