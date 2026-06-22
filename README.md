# Escape Room Booking System – ASP.NET Core Web API

A RESTful Web API for managing escape rooms and bookings, built with ASP.NET Core 10 and Entity Framework Core.

---

## Table of Contents

- [System Description](#system-description)
- [Installation & Running](#installation--running)
- [Entities](#entities)
- [Layer Structure](#layer-structure)
- [API Endpoints](#api-endpoints)
- [Error Handling](#error-handling)
- [Technologies](#technologies)

---

## System Description

This system allows managing escape rooms and player bookings.  
It supports full CRUD operations for rooms and bookings, with proper validation, error handling, and data mapping via DTOs and AutoMapper.

---

## Installation & Running

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (Express or higher)
- [dotnet-ef tool](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)

### Setup Steps

**1. Clone the repository**
```bash
git clone https://github.com/noemieWeiss/C-Escape-Room-Project.git
cd C-Escape-Room-Project
```

**2. Configure the connection string**

Edit `EscapeRoom/EscapeRoom.API/appsettings.json` if needed:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=EscapeRoomDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

**3. Install the EF Core CLI tool (if not already installed)**
```bash
dotnet tool install --global dotnet-ef
```

**4. Run database migrations**
```bash
dotnet ef database update --project EscapeRoom/EscapeRoom.Data --startup-project EscapeRoom/EscapeRoom.API
```

**5. Run the API**
```bash
dotnet run --project EscapeRoom/EscapeRoom.API
```

**6. Open Swagger UI**

Navigate to: `http://localhost:5114/swagger`

---

## Entities

### EscapeRoom
Represents a single escape room available for booking.

| Field | Type | Description |
|-------|------|-------------|
| Id | int | Primary key |
| Name | string | Room name |
| DifficultyLevelId | int (FK) | Links to DifficultyLevel |
| MaxParticipants | int | Maximum number of players |

### Booking
Represents a player's reservation for an escape room.

| Field | Type | Description |
|-------|------|-------------|
| Id | int | Primary key |
| PlayerId | int (FK) | Links to Player |
| EscapeRoomId | int (FK) | Links to EscapeRoom |
| BookingDateTime | DateTime | Date and time of booking |
| NumberOfParticipants | int | Number of players in the booking |
| Status | string | Booking status (e.g., Confirmed, Cancelled) |

### Player
Represents a customer who makes bookings.

| Field | Type | Description |
|-------|------|-------------|
| Id | int | Primary key |
| FullName | string | Player's full name |
| Email | string | Unique email address |
| Phone | string? | Optional phone number |

### DifficultyLevel
Lookup table for room difficulty levels.

| Field | Type | Description |
|-------|------|-------------|
| Id | int | Primary key |
| Name | string | e.g., Easy, Medium, Hard |

### Hint
Tips associated with a specific escape room.

| Field | Type | Description |
|-------|------|-------------|
| Id | int | Primary key |
| Text | string | Hint content |
| EscapeRoomId | int (FK) | Links to EscapeRoom |

### Entity Relationships

```
DifficultyLevel  ──<  EscapeRoom  ──<  Booking  >──  Player
                           │
                           └──<  Hint
```

---

## Layer Structure

```
C-Escape-Room-Project/
└── EscapeRoom/
    ├── EscapeRoom.API/          # Presentation layer
    │   ├── Controllers/         # RoomsController, BookingsController
    │   ├── Middleware/          # Global error handling
    │   └── Program.cs           # DI configuration, middleware pipeline
    │
    ├── EscapeRoom.Core/         # Business Logic layer
    │   ├── DTOs/                # RoomDto, BookingDto
    │   ├── Entities/            # Domain models
    │   ├── Exceptions/          # NotFoundException, BadRequestException
    │   ├── Mapping/             # AutoMapper profile
    │   └── Services/            # IRoomService, IBookingService (interfaces)
    │
    └── EscapeRoom.Data/         # Data Access layer
        ├── Services/            # RoomService, BookingService (implementations)
        ├── Migrations/          # EF Core migrations
        └── AppDbContext.cs      # Database context
```

**Key design principles:**
- Each layer only communicates with the layer below it
- The API layer never touches the database directly
- Entities are never exposed to the client — only DTOs are returned
- All database operations are fully async

---

## API Endpoints

### Rooms – `/api/rooms`

| Method | Endpoint | Description | Status Codes |
|--------|----------|-------------|--------------|
| GET | `/api/rooms` | Get all rooms | 200 |
| GET | `/api/rooms/{id}` | Get room by ID | 200, 404 |
| POST | `/api/rooms` | Create a new room | 201, 400 |
| PUT | `/api/rooms/{id}` | Update a room | 200, 400, 404 |
| DELETE | `/api/rooms/{id}` | Delete a room | 204, 404 |

**Example – Create Room (POST `/api/rooms`)**
```json
{
  "name": "The Lost Dungeon",
  "difficulty": "Hard",
  "maxCapacity": 6
}
```

---

### Bookings – `/api/bookings`

| Method | Endpoint | Description | Status Codes |
|--------|----------|-------------|--------------|
| GET | `/api/bookings` | Get all bookings | 200 |
| GET | `/api/bookings/{id}` | Get booking by ID | 200, 404 |
| POST | `/api/bookings` | Create a new booking | 201, 400 |
| PUT | `/api/bookings/{id}` | Update a booking | 200, 400, 404 |
| DELETE | `/api/bookings/{id}` | Delete a booking | 204, 404 |

**Example – Create Booking (POST `/api/bookings`)**
```json
{
  "playerId": 1,
  "escapeRoomId": 2,
  "bookingDateTime": "2026-07-15T18:00:00",
  "numberOfParticipants": 4,
  "status": "Confirmed"
}
```

---

## Error Handling

All errors are handled globally via `ErrorHandlingMiddleware` and return a consistent JSON structure:

```json
{
  "error": "Room with ID 5 was not found."
}
```

| Exception | HTTP Status |
|-----------|-------------|
| NotFoundException | 404 Not Found |
| BadRequestException | 400 Bad Request |
| Any other exception | 500 Internal Server Error |

---

## Technologies

| Technology | Version | Purpose |
|-----------|---------|---------|
| ASP.NET Core | 10.0 | Web API framework |
| Entity Framework Core | 10.0 | ORM / database access |
| SQL Server | 2025 | Database |
| AutoMapper | 16.1 | Entity ↔ DTO mapping |
| Swashbuckle (Swagger) | 10.2 | API documentation & testing |
