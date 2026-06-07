# TicTacToe Game - Full Stack Application

A modern, full-stack Tic Tac Toe game built with .NET Core backend API and React + TypeScript frontend. This project demonstrates clean architecture, responsive UI design, and REST API best practices.

## 📋 Table of Contents

- [Project Overview](#project-overview)
- [Features](#features)
- [Tech Stack](#tech-stack)
- [Project Structure](#project-structure)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
  - [Backend Setup](#backend-setup)
  - [Frontend Setup](#frontend-setup)
- [Running the Application](#running-the-application)
- [API Endpoints](#api-endpoints)
- [Testing](#testing)
  [UI Screenshot](#UI Screnshot)

---

## 🎮 Project Overview

This is a complete Tic Tac Toe game application with:
- **Backend API**: Built with .NET 10 (ASP.NET Core) with clean architecture patterns
- **Frontend UI**: Modern React + TypeScript interface with Vite for fast development
- **Features**: Game creation, move validation, score tracking, and responsive design

---

## ✨ Features

✅ **Game Management**
- Create new games
- Play against AI or another player
- Real-time game state tracking
- Win/Draw/Loss detection

✅ **Scoring System**
- Track player statistics
- Maintain scoreboard
- View game history

✅ **Responsive UI**
- Clean, modern interface
- Mobile-friendly design
- Real-time updates

✅ **REST API**
- Well-structured endpoints
- CORS enabled
- Error handling

---

## 🛠 Tech Stack

### Backend
- **.NET 10** (C#)
- **ASP.NET Core** - Web framework
- **Repository Pattern** - Data access layer
- **Service Layer** - Business logic

### Frontend
- **React 18** - UI framework
- **TypeScript** - Type safety
- **Vite** - Build tool and dev server
- **CSS3** - Styling

### Testing
- **xUnit** - Unit testing framework
- **MSTest** - Alternative testing

---

## 📁 Project Structure

```
TicTacToe.Api/
├── TicTacToe.Api/                 # Backend API
│   ├── Controllers/               # API endpoints
│   │   ├── GamesController.cs
│   │   └── ScoreboardController.cs
│   ├── Services/                  # Business logic
│   │   ├── IGameService.cs
│   │   └── GameService.cs
│   ├── Repositories/              # Data access
│   │   └── GameRepository.cs
│   ├── Models/                    # Domain models
│   │   ├── Game.cs
│   │   ├── Move.cs
│   │   └── Scoreboard.cs
│   ├── DTOs/                      # Data transfer objects
│   │   ├── CreateGameRequest.cs
│   │   └── MoveRequest.cs
│   ├── Enums/                     # Enumerations
│   │   ├── GameMode.cs
│   │   └── GameStatus.cs
│   └── Program.cs                 # Entry point
│
├── TicTacToe.Api.Tests/           # Backend unit tests
│   └── GameServiceTests.cs
│
├── frontend/
│   └── tic-tac-toe-ui/            # React frontend
│       ├── src/
│       │   ├── components/        # React components
│       │   │   ├── Board.tsx
│       │   │   ├── Cell.tsx
│       │   │   ├── Scoreboard.tsx
│       │   │   └── MoveHistory.tsx
│       │   ├── api/               # API client
│       │   │   └── gameapi.ts
│       │   ├── types/             # TypeScript types
│       │   │   └── game.ts
│       │   ├── App.tsx            # Main app component
│       │   ├── main.tsx           # Entry point
│       │   └── index.css          # Styles
│       ├── vite.config.ts         # Vite configuration
│       └── package.json           # Dependencies
│
├── TicTacToe.Api.slnx             # Visual Studio solution file
├── README.md                      # This file
└── .gitignore                     # Git ignore rules
```

---

## ✅ Prerequisites

Make sure you have the following installed on your machine:

### For Backend
- **.NET 10 SDK** - [Download](https://dotnet.microsoft.com/download/dotnet/10.0)
- **Visual Studio 2022** or **VS Code** (optional)

### For Frontend
- **Node.js** (v18 or higher) - [Download](https://nodejs.org/)
- **npm** (comes with Node.js)

### Verify Installation
```bash
# Check .NET version
dotnet --version

# Check Node.js and npm version
node --version
npm --version
```

---

## 🚀 Getting Started

### Backend Setup

1. **Navigate to backend directory:**
```bash
cd TicTacToe.Api
```

2. **Restore dependencies:**
```bash
dotnet restore
```

3. **Build the project:**
```bash
dotnet build
```

### Frontend Setup

1. **Navigate to frontend directory:**
```bash
cd frontend/tic-tac-toe-ui
```

2. **Install dependencies:**
```bash
npm install
```

3. **Verify the installation:**
```bash
npm list
```

---

## ▶️ Running the Application

### Start Backend API

1. **From the backend directory** (`TicTacToe.Api/`):
```bash
dotnet run
```

The API will start at: **http://localhost:5000** (or as configured in `launchSettings.json`)

Example output:
```
Building...
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5000
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to exit.
```

### Start Frontend Application

1. **From the frontend directory** (`frontend/tic-tac-toe-ui/`):
```bash
npm run dev
```

The UI will be available at: **http://localhost:5173** (or as shown in terminal)

Example output:
```
VITE v5.0.0  ready in 245 ms

➜  Local:   http://localhost:5173/
➜  press h + enter to show help
```

### Access the Application

1. **Open your browser** and navigate to:
   ```
   http://localhost:5173
   ```

2. **The UI should load** and you can:
   - Create a new game
   - Play the game
   - View scoreboard
   - Check move history

---

## 🔌 API Endpoints

### Games
| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/api/games/{id}` | Get game by ID |
| `POST` | `/api/games` | Create new game |
| `POST` | `/api/games/{id}/moves` | Make a move |
| `GET` | `/api/games/{id}/status` | Get game status |

### Scoreboard
| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/api/scoreboard` | Get all scores |
| `GET` | `/api/scoreboard/{playerId}` | Get player score |

**Example: Create a Game**
```bash
curl -X POST http://localhost:5000/api/games \
  -H "Content-Type: application/json" \
  -d '{"playerName":"John","gameMode":"PvAI"}'
```

---

## 🧪 Testing

### Run Backend Tests

1. **Navigate to tests directory:**
```bash
cd TicTacToe.Api.Tests
```

2. **Run all tests:**
```bash
dotnet test
```

3. **Run specific test class:**
```bash
dotnet test --filter ClassName=GameServiceTests
```

Example output:
```
Test Run Successful.
Total tests: 5
     Passed: 5
     Failed: 0
```

---

## 📸 Screenshots

### Game Board
- Clean grid-based UI
- Real-time game status display
- Player turn indicator
- Win/Draw notifications

### Features Displayed
- ✅ Active game board
- ✅ Score tracking
- ✅ Move history
- ✅ Responsive design for all screen sizes

---
##UI Screnshot
<img width="379" height="434" alt="image" src="https://github.com/user-attachments/assets/18fedf88-4bb8-43ad-aa8c-6f9682732b42" />

<img width="332" height="480" alt="image" src="https://github.com/user-attachments/assets/230b6844-3fcd-4475-9175-ab58f7992a2e" />
<img width="288" height="426" alt="image" src="https://github.com/user-attachments/assets/8835b35d-fa76-466b-bc42-0a2e232ad53c" />



## 📚 Development

### VS Code Extensions (Recommended)
- **C# Extension Pack**
- **REST Client** - For API testing
- **Thunder Client** or **Postman** - For API testing

### Common Development Tasks

**Format code:**
```bash
# Backend
dotnet format

# Frontend
npm run lint
```

**Build for production:**
```bash
# Frontend
npm run build
```

---

## 🤝 Contributing

Feel free to fork this project and submit pull requests for any improvements.

---

## 📝 License

This project is open source and available for educational purposes.

---

## 👨‍💻 Author

**Rahul Patil**
- GitHub: [@rdpatil112000](https://github.com/rdpatil112000)
- Email: rdpatil112000@gmail.com


**Last Updated:** June 2026
**Version:** 1.0.0
