# ✈️ Check-In System

A modular .NET-based check-in system for managing flight passenger check-ins, seat assignments, flight status updates, and real-time data synchronization via SignalR and TCP sockets.

---

# 📦 CheckIn System Project Structure

## ✅ Core/ — Core Application Logic
This folder holds your core layers: DTOs, Data Access, and Business Logic.

### 🔹 CheckInSystem.DTO/
Contains all Data Transfer Objects (DTOs) and Enums used across the app.

Used for communication between layers and API payloads.

**Contents:**
- `FlightDto.cs`
- `SeatDto.cs`
- `PassengerDto.cs`
- `AssignSeatRequestDto.cs`
- `FlightStatusUpdateDto.cs`

**Enums/**
- `FlightStatus.cs`
- `PassengerStatus.cs`

### 🔹 CheckInSystem.Data/
Data Access Layer (DAL) for all database interaction using Entity Framework Core.

Contains models (EF entities), database context, migrations, and repository implementations.

**Structure:**

**Models/**
- `Flight.cs`
- `Passenger.cs`
- `Seat.cs`

**Repositories/**
- `FlightRepository.cs`
- `PassengerRepository.cs`
- `SeatRepository.cs`

**Interfaces/**
- `IFlightRepository.cs`
- `IPassengerRepository.cs`
- `ISeatRepository.cs`

- `CheckInDbContext.cs` — main EF Core DbContext  
- `Migrations/` — auto-generated EF Core migration files  
- `CheckInDbContextFactory.cs` — for CLI EF tools

### 🔹 CheckInSystem.Business/
Business Logic Layer (BLL) for seat assignment, flight management, etc.

Interfaces and service implementations for encapsulating logic.

**Structure:**

**Interfaces/**
- `ISeatAssignmentService.cs`
- `IFlightStatusService.cs`
- `IFlightNotifier.cs` (SignalR)
- `ISeatNotifier.cs` (SignalR)
- `ISocketNotifier.cs`

**Services/**
- `SeatAssignmentService.cs`
- `FlightStatusService.cs`
- `SocketNotifier.cs`

---

## ✅ Server/ — ASP.NET Core Server App
Contains the REST API, SignalR hubs, and configuration.

### 🔹 CheckInServer.API/
**Structure:**

**Controllers/**
- `FlightController.cs` — Manage flight status, list flights.
- `PassengerController.cs` — Add/search passengers, print boarding pass.
- `SeatController.cs` — Assign, lock/unlock, and list seats.

**Hubs/**
- `FlightStatusHub.cs` — Broadcast flight status changes.
- `SeatHub.cs` — Broadcast seat assignment updates.

**Notifiers/**
- `SignalRFlightNotifier.cs`
- `SignalRSeatNotifier.cs`

- `Program.cs` — Main app startup (with DI, EF, SignalR, CORS)

🧠 SignalR Hubs are mapped at:
- `/hub/flight-status`
- `/hub/seat-updates`

### 🔹 CheckInServer.Socket/
Standalone TCP socket server for handling real-time seat locking/assigning via JSON.

**Structure:**

**Models/**
- `SocketMessage.cs` — message format (Assign, Lock, Unlock)

**Services/**
- `SeatSocketService.cs` — TCP server using TcpListener

- `Program.cs` — Entry point (runs the socket listener)

---

## ✅ TestClient/ — Testing Clients
Used to simulate requests and test integrations.

### 🔹 SocketTestClient/
Simple console app to send test socket messages to TCP server.

- `Program.cs`
- `SocketTestClient.csproj`

### 🔹 Root
- `Program.cs` — test API (REST) client  
- `test.html` — test SignalR client with JS + `signalr.min.js`  
- `TestClient.csproj`

---

## ✅ Usage Summary

| Component          | Purpose                                   | Test Tool         |
|--------------------|-------------------------------------------|-------------------|
| REST API           | Assign seat, list flights, update status | Postman / curl    |
| SignalR (Hubs)     | Live updates (flight status + seat updates) | test.html         |
| TCP Socket Server  | Accept seat lock/assign notifications     | SocketTestClient  |
| EF Core (SQLite)   | Persist passengers, seats, flights        | dotnet-ef CLI     |

---

## 🎯 Features

### ✅ Core Functionality

| Feature                        | Description                                               |
|-------------------------------|-----------------------------------------------------------|
| 🧾 Passenger Registration      | Add and retrieve passengers by passport number            |
| 🎟 Seat Assignment             | Assign seats to booked passengers                         |
| 🔄 Flight Status Update        | Change flight status (Scheduled, Boarding, Departed, etc) |
| 📡 Real-Time SignalR           | Push flight status and seat assignments live              |
| 🔌 TCP Socket Notifier         | Manual message sync for seat assignment/locking           |
| 📥 SQLite Storage              | Uses EF Core + SQLite for portable local database         |

---

# 🚀 How to Run
---

## ✅ Prerequisites

- .NET SDK 9  

```bash
dotnet tool install --global dotnet-ef
```

---

## 🛠️ Step-by-Step Setup

### 1. Clone the repository

```bash
git clone <your-repo-url>
cd CheckInSystem
```

### 2. Restore all NuGet packages

```bash
dotnet restore
```

### 3. Build the full solution

```bash
dotnet build
```

---

## 🧩 Database Setup (SQLite + EF Core)

We are using SQLite and EF Core Migrations to create and seed the `checkin.db`.

### 4. Add Migration (optional if not yet created)

```bash
dotnet ef migrations add InitWithSeed \
  --project Core/CheckInSystem.Data/CheckInSystem.Data.csproj \
  --startup-project Server/CheckInServer.API/CheckInServer.API.csproj
```

### 5. Apply Migration to Create `checkin.db`

```bash
dotnet ef database update \
  --project Core/CheckInSystem.Data/CheckInSystem.Data.csproj \
  --startup-project Server/CheckInServer.API/CheckInServer.API.csproj
```

🔹 This will create `checkin.db` and seed passengers, seats, and flights (if defined in `OnModelCreating`).

---

### 6. Check Database (Optional)

Use SQLite tools to browse `checkin.db`:

- [DB Browser for SQLite](https://sqlitebrowser.org/)
- Or command line:

```bash
sqlite3 checkin.db
```

# 🏁 Running the CheckInSystem

## 1. 🔌 Run the REST API (with SignalR Hubs)

This runs your HTTP REST API on `https://localhost:5052`.

```bash
dotnet run --project Server/CheckInServer.API
```

### 🔍 Example Endpoints

| Endpoint                                      | Method | Description                          |
|----------------------------------------------|--------|--------------------------------------|
| `/api/seats/available?flightId=1`            | GET    | Get available seats                 |
| `/api/seats/assign`                          | POST   | Assign a seat to a passenger        |
| `/api/passengers`                            | GET    | Get all passengers                  |
| `/api/passengers/{passport}`                 | GET    | Get passenger by passport           |
| `/api/passengers/boarding/{pass}`            | GET    | Generate boarding pass (JSON)       |
| `/api/flights`                               | GET    | Get all flights                     |
| `/api/flights/status`                        | POST   | Update flight status                |

✅ Make sure to open the port shown in the terminal and match it in Postman/test client.

---

## 2. 🪵 Run the TCP Socket Server

This server listens for seat updates over TCP (`localhost:5050`) and prints/logs actions.

```bash
dotnet run --project Server/CheckInServer.Socket
```

### Expected Output:

```bash
Socket server listening on port 5050...
Client connected.
Assigning seat...
```

---

## 🔪 Test the REST API

### 🕠 Using `curl`

```bash
curl http://localhost:5052/api/seats/available?flightId=1
```

```bash
curl -X POST http://localhost:5052/api/seats/assign \
  -H "Content-Type: application/json" \
  -d '{ "passengerId": 101, "seatId": 15 }'
```

### 🗡️ Using Postman

1. Open Postman.
2. Set method to `POST` or `GET`.
3. Use URLs like:

```
http://localhost:5052/api/seats/assign
```

4. Add raw JSON body:

```json
{
  "passengerId": 101,
  "seatId": 15
}
```



# 🔗 REST API Endpoints

The REST API is served via `CheckInServer.API`, typically at:

- **HTTP**: `http://localhost:5052`

---

## 🎟️ Seat Management

| Endpoint                                | Method | Description                                       |
|----------------------------------------|--------|-------------------------------------------------|
| `/api/seats/available?flightId=1`      | GET    | Get all available seats for a specific flight   |
| `/api/seats/all?flightId=1`            | GET    | Get all seats (including assigned/locked)       |
| `/api/seats/assign`                    | POST   | Assign a seat to a passenger (if available)     |

### 🔄 Sample Payload for Assigning Seat:

```json
POST /api/seats/assign
{
  "passengerId": 101,
  "seatId": 15
}
```

---

## 🥃 Passenger Management

| Endpoint                                 | Method | Description                                 |
|-----------------------------------------|--------|---------------------------------------------|
| `/api/passengers`                       | GET    | List all passengers                        |
| `/api/passengers/{passportNumber}`      | GET    | Get passenger info by passport             |
| `/api/passengers`                       | POST   | Add a new passenger                        |
| `/api/passengers/boarding/{passportNumber}` | GET | Generate a boarding pass (JSON)            |

### 🗾 Sample Payload to Add Passenger:

```json
POST /api/passengers
{
  "fullName": "Jane Doe",
  "passportNumber": "XA123456",
  "flightId": 1,
  "status": 0
}
```

---

## ✈️ Flight Management

| Endpoint               | Method | Description                  |
|------------------------|--------|------------------------------|
| `/api/flights`         | GET    | List all flights            |
| `/api/flights/status`  | POST   | Change flight status        |

### 🔄 Sample Payload for Updating Flight Status:

```json
POST /api/flights/status
{
  "flightId": 1,
  "newStatus": "Boarding"
}
```

---

## 📡 SignalR Real-time Updates

The system uses SignalR hubs for real-time seat and flight status updates.

### 🧱 1. Endpoints

| Purpose                 | Hub                 | Path                        |
|-------------------------|---------------------|-----------------------------|
| Seat assignment updates | `SeatHub`          | `/hub/seat-updates`         |
| Flight status updates   | `FlightStatusHub`  | `/hub/flight-status`        |

### 📜 2. JavaScript Example (Live Seat Listener)

```html
<script src="https://cdnjs.cloudflare.com/ajax/libs/microsoft-signalr/7.0.5/signalr.min.js"></script>
<script>
    const seatConnection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:5052/hub/seat-updates")
        .build();

    seatConnection.on("SeatAssigned", (seat) => {
        console.log("🎟 Seat Assigned:", seat);
        // You can update seat map UI here
    });

    seatConnection.start().then(() => {
        console.log("✅ Connected to SeatHub");
    }).catch(err => console.error("❌ Error connecting:", err));
</script>
```

### ✈️ JavaScript Listener for Flight Status

```javascript
const flightConnection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:5052/hub/flight-status")
    .build();

flightConnection.on("FlightStatusChanged", (flight) => {
    console.log("✈️ Flight status updated:", flight);
    // Update status board
});

flightConnection.start().then(() => {
    console.log("✅ Connected to FlightStatusHub");
});
```

---

## 🛡️ CORS Configuration (Important)

Ensure in `Program.cs`:

```csharp
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

app.UseCors(); // Must be above UseAuthorization()

```

---
