# TicTacToe API - Backend

A modern ASP.NET Core REST API for a Tic-Tac-Toe game with real-time game management, move history tracking, and scoreboard functionality.

## Project Overview

The TicTacToe API is a backend service that manages the core game logic for Tic-Tac-Toe. It provides endpoints for creating games, making moves, retrieving game state, and tracking player scores. The API is built with scalability and clean architecture principles in mind.

## Tech Stack

- **Framework**: ASP.NET Core 10.0
- **Language**: C#
- **Database**: In-memory (Entity Framework Core)
- **Testing**: xUnit / NUnit
- **Architecture Pattern**: Repository Pattern + Service Layer
- **API Style**: RESTful
- **Build System**: .NET CLI / MSBuild

## Features Implemented

- ✅ Create new Tic-Tac-Toe games
- ✅ Make moves on the game board
- ✅ Real-time game state tracking
- ✅ Win/Draw detection
- ✅ Move history tracking
- ✅ Player scoreboard management
- ✅ Game mode support (Player vs Player, Player vs AI)
- ✅ Comprehensive error handling
- ✅ RESTful API endpoints

## How to Run the Backend Locally

### Prerequisites

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) or later
- Visual Studio 2022 or VS Code with C# extension

### Setup Steps

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd TicTacToe.Api/TicTacToe.Api
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Build the project**
   ```bash
   dotnet build
   ```

4. **Run the API**
   ```bash
   dotnet run
   ```

5. **Access the API**
   - The API will be available at `https://localhost:5001` or `http://localhost:5000`
   - View API in VS Code using the REST Client extension (`.http` file included)

### Environment Configuration

- **Development**: Uses `appsettings.Development.json` with detailed logging and CORS enabled
- **Production**: Uses `appsettings.json` with optimized settings

## API Endpoint Summary

### Games Controller

#### Create a New Game
```
POST /api/games
Content-Type: application/json

{
  "player1Name": "Alice",
  "player2Name": "Bob",
  "gameMode": 0
}

Response: 201 Created
{
  "gameId": "uuid",
  "player1Name": "Alice",
  "player2Name": "Bob",
  "currentPlayer": "X",
  "status": "InProgress",
  "board": [null, null, null, null, null, null, null, null, null],
  "moves": []
}
```

#### Get Game State
```
GET /api/games/{gameId}

Response: 200 OK
{
  "gameId": "uuid",
  "player1Name": "Alice",
  "player2Name": "Bob",
  "currentPlayer": "X",
  "status": "InProgress",
  "board": [...],
  "moves": [...]
}
```

#### Make a Move
```
POST /api/games/{gameId}/move
Content-Type: application/json

{
  "row": 0,
  "column": 0
}

Response: 200 OK
{
  "gameId": "uuid",
  "board": [...],
  "currentPlayer": "O",
  "status": "InProgress",
  "moves": [...]
}
```

### Scoreboard Controller

#### Get All Scores
```
GET /api/scoreboard

Response: 200 OK
[
  {
    "playerId": "uuid",
    "playerName": "Alice",
    "wins": 5,
    "losses": 2,
    "draws": 1
  }
]
```

#### Get Player Score
```
GET /api/scoreboard/{playerId}

Response: 200 OK
{
  "playerId": "uuid",
  "playerName": "Alice",
  "wins": 5,
  "losses": 2,
  "draws": 1
}
```

## How to Run Tests

### Run All Tests
```bash
cd ../TicTacToe.Api.Tests
dotnet test
```

### Run Specific Test Class
```bash
dotnet test --filter ClassName=GameServiceTests
```

### Run with Coverage (requires coverage tool)
```bash
dotnet test /p:CollectCoverage=true /p:CoverageFormat=opencover
```

## AI Tools and Prompt Summary

### AI-Assisted Development

This project was developed with AI assistance for:

- **Architecture Design**: Service layer and repository pattern implementation
- **API Contract Definition**: RESTful endpoint design and DTO structure
- **Game Logic**: Win condition detection and game state management
- **Error Handling**: Comprehensive exception handling and validation
- **Code Generation**: Boilerplate code and CRUD operations
- **Testing**: Unit test structure and assertions

### Key Prompts Used

1. "Design a scalable Tic-Tac-Toe API with service and repository patterns"
2. "Implement win detection algorithm for Tic-Tac-Toe"
3. "Create RESTful endpoints for game management"
4. "Write unit tests for game service logic"
5. "Implement scoreboard tracking with player statistics"

## Design Decisions

### Architecture

- **Service Layer**: Abstracts business logic from controllers
- **Repository Pattern**: Enables data access abstraction and testability
- **DTO Pattern**: Separates API contracts from domain models
- **Dependency Injection**: Manages service lifetimes and dependencies

### Game Logic

- **Immutable Board State**: Each move creates a new game state
- **Move Validation**: All moves validated before application
- **Comprehensive Game Status**: InProgress, Won, Draw states tracked
- **Move History**: Complete audit trail of all moves

### API Design

- **Resource-Oriented**: Follows RESTful conventions
- **Stateless**: Each request is independent
- **Versioning Ready**: Structure allows for future API versioning
- **Error Responses**: Standard HTTP status codes with descriptive messages

## Clarifications and Assumptions

### Assumptions

- Players take turns strictly (no concurrent moves from same player)
- Board size is fixed at 3x3
- Game state persists in-memory during session
- Player identifiers are UUIDs
- Win detection uses standard Tic-Tac-Toe rules (3 in a row/column/diagonal)

### Clarifications

- **Game Modes**: Enum supports future expansion (PvP, PvAI, AiVsAI)
- **Move History**: Stored as Move objects with timestamp, player, and position
- **Scoreboard**: Automatically updated on game completion
- **Concurrency**: Current implementation uses in-memory storage (thread-safe operations recommended for production)

## Known Limitations

1. **In-Memory Storage**: Game data is not persisted to a database (resets on restart)
2. **No Authentication**: No user authentication or authorization implemented
3. **No AI Implementation**: AI opponent mode is defined in enum but not implemented
4. **No Rate Limiting**: No request rate limiting in place
5. **Limited Validation**: Minimal input validation on request payloads
6. **Single Instance**: Not designed for multi-server deployments
7. **No WebSocket Support**: Real-time updates would require SignalR or WebSocket implementation

## Future Improvements

1. **Database Integration**: Persist games and scores using Entity Framework Core with SQL Server/PostgreSQL
2. **AI Opponent**: Implement AI using minimax algorithm for Player vs AI mode
3. **Authentication & Authorization**: Add JWT-based authentication and user roles
4. **Real-Time Updates**: Implement SignalR for live game updates to multiple clients
5. **Rate Limiting**: Add rate limiting middleware for API endpoints
6. **Input Validation**: Implement FluentValidation for comprehensive request validation
7. **Logging & Monitoring**: Add structured logging (Serilog) and application insights
8. **Caching**: Implement caching for frequently accessed data
9. **API Documentation**: Add Swagger/OpenAPI documentation with examples
10. **Performance Optimization**: Add database indexing, query optimization, and caching strategies
11. **Multi-Server Support**: Implement distributed state management for scalability
12. **Advanced Game Modes**: Support for larger boards, time limits, and tournament modes

---

## Project Structure

```
TicTacToe.Api/
├── Controllers/          # API endpoints
├── Services/             # Business logic
├── Repositories/         # Data access
├── Models/               # Domain entities
├── DTOs/                 # Data transfer objects
├── Enums/                # Enumeration types
├── Program.cs            # Entry point & configuration
└── appsettings.json      # Configuration
```

## Support

For issues or questions, please create an issue in the repository or contact the development team.
