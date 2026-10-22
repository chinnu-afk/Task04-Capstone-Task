
# Task 04 - Capstone Task API

## Overview
A task management REST API built using ASP.NET Core Web API, C#, Entity Framework Core, and SQLite.

## Features
- Create, read, update, and delete tasks (CRUD)
- SQLite database integration
- Entity Framework Core
- Service layer using dependency injection
- Search tasks by title or description
- Filter tasks by completion status
- Pagination with page and pageSize parameters
- Input validation and HTTP status codes
- Swagger API documentation

## Task Properties
- Id: Unique task identifier
- Title: Required task title (maximum 150 characters)
- Description: Optional task description (maximum 1000 characters)
- IsCompleted: Task completion status
- CreatedAt: UTC creation date and time

## API Endpoints

| Method | Endpoint | Purpose |
|---|---|---|
| GET | /api/tasks | List tasks |
| GET | /api/tasks/{id} | Get one task |
| POST | /api/tasks | Create a task |
| PUT | /api/tasks/{id} | Update a task |
| DELETE | /api/tasks/{id} | Delete a task |

## Filtering and Pagination

Example:

`/api/tasks?search=report&isCompleted=false&page=1&pageSize=10`

- `search`: Search title or description
- `isCompleted`: Filter by completion status
- `page`: Page number, starting at 1
- `pageSize`: Number of records per page (maximum 100)

## Requirements
- .NET 8 SDK
- Entity Framework Core tools

## Run Locally

Restore dependencies:

`dotnet restore`

Create a migration:

`dotnet ef migrations add InitialCreate`

Apply migrations to create the database:

`dotnet ef database update`

Run the API:

`dotnet run`

Open the Swagger URL printed in the terminal to test the endpoints.

## Database
The application uses SQLite and stores data in `tasks.db`.

## Note
The project source has been uploaded to GitHub. Build and test it locally before considering it production-ready.

