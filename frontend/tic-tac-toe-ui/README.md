# TicTacToe UI - Frontend

A modern, responsive React frontend for playing Tic-Tac-Toe with real-time game state synchronization, move history tracking, and player scoreboard display.

## Project Overview

The TicTacToe UI is a React-based web application providing an intuitive interface for playing Tic-Tac-Toe against another player or an AI opponent. The application connects to the TicTacToe API backend to manage game state, persist scores, and maintain game history.

## Tech Stack

- **Framework**: React 18+ with TypeScript
- **Build Tool**: Vite
- **Language**: TypeScript
- **Styling**: CSS (Vanilla CSS with CSS Modules)
- **HTTP Client**: Fetch API
- **Package Manager**: npm
- **Linting**: ESLint
- **Testing**: Vitest (recommended)

## Features Implemented

- ✅ Create new games with player names
- ✅ Interactive game board with click-to-play functionality
- ✅ Real-time game state updates
- ✅ Move history tracking and display
- ✅ Player scoreboard with win/loss statistics
- ✅ Win/Draw detection with game status display
- ✅ Responsive design for desktop and tablet devices
- ✅ Move validation and error handling
- ✅ Game restart capability
- ✅ Player turn indicator

## How to Run the Frontend Locally

### Prerequisites

- [Node.js](https://nodejs.org/) v16.x or later
- [npm](https://www.npmjs.com/) v8.x or later
- TicTacToe API backend running on `http://localhost:5000` or `https://localhost:5001`

### Setup Steps

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd frontend/tic-tac-toe-ui
   ```

2. **Install dependencies**
   ```bash
   npm install
   ```

3. **Configure API endpoint** (if needed)
   - Edit `src/api/gameapi.ts` to update the API base URL if backend is running on a different port
   - Default: `http://localhost:5000/api`

4. **Start the development server**
   ```bash
   npm run dev
   ```

5. **Access the application**
   - Navigate to `http://localhost:5173` (default Vite port)
   - The application will auto-reload on file changes

6. **Build for production**
   ```bash
   npm run build
   ```

7. **Preview production build**
   ```bash
   npm run preview
   ```

## API Endpoint Summary

The frontend communicates with the following backend endpoints:

### Game Endpoints

- **POST** `/api/games` - Create a new game
- **GET** `/api/games/{gameId}` - Retrieve game state
- **POST** `/api/games/{gameId}/move` - Make a move

### Scoreboard Endpoints

- **GET** `/api/scoreboard` - Retrieve all player scores
- **GET** `/api/scoreboard/{playerId}` - Retrieve specific player score

See [Backend README](../TicTacToe.Api/README.md) for detailed endpoint documentation.

## How to Run Tests

### Run Tests in Watch Mode
```bash
npm run test
```

### Run Tests with Coverage
```bash
npm run test:coverage
```

### Run Linting
```bash
npm run lint
```

### Fix Linting Issues
```bash
npm run lint:fix
```

## AI Tools and Prompt Summary

### AI-Assisted Development

This project was developed with AI assistance for:

- **Component Architecture**: React component design and organization
- **State Management**: Game state handling and updates
- **API Integration**: Fetch-based API client implementation
- **UI/UX Design**: Component layout and responsive design
- **TypeScript Types**: Game type definitions and interfaces
- **Event Handling**: Click handlers and form submissions
- **Error Handling**: User-friendly error messages
- **Performance**: Component optimization and memoization

### Key Prompts Used

1. "Design React components for a Tic-Tac-Toe game board"
2. "Create a TypeScript-based API client for game management"
3. "Implement real-time game state updates in React"
4. "Build responsive board and scoreboard components"
5. "Add move history and game status tracking"
6. "Implement error handling and user feedback"

## Design Decisions

### Component Architecture

- **Single Responsibility**: Each component handles one concern
- **Container/Presentational Split**: Smart and dumb component separation
- **Props-Based State Management**: Props drilling for simplicity (scalable to Redux/Zustand)
- **Custom Hooks**: Reusable logic extracted into custom hooks

### Styling Approach

- **Vanilla CSS**: No CSS-in-JS overhead for simple styling
- **CSS Modules**: Component-scoped styling to prevent conflicts
- **Responsive Design**: Mobile-first CSS media queries
- **Accessibility**: Semantic HTML and ARIA labels

### API Integration

- **Fetch API**: Native browser API, no external dependencies
- **Centralized Client**: Single `gameapi.ts` file for all API calls
- **Error Handling**: Try-catch with user-friendly error messages
- **Type Safety**: TypeScript types for all API responses

### Game Logic

- **Immutable State**: Game state treated as immutable
- **Server-Side Validation**: All game logic validated on backend
- **Optimistic Updates**: UI updates before server confirmation
- **Move History**: Complete move trace displayed to players

## Clarifications and Assumptions

### Assumptions

- Backend API is running and accessible at configured endpoint
- Players use modern browsers (ES2020+)
- Network connection is stable
- Browser has localStorage available (for future persistence)
- Game state is managed server-side

### Clarifications

- **Player Names**: Required when creating a new game
- **Move Validation**: Invalid moves are rejected by server and displayed to user
- **Game Status**: Determined by backend (Won, Draw, InProgress)
- **Scoreboard**: Automatically updated after each completed game
- **No AI in Frontend**: AI opponent logic would be implemented on backend

## Known Limitations

1. **No Persistent Storage**: Game data stored only in session (no localStorage implemented)
2. **No User Authentication**: No login/signup system implemented
3. **No Real-Time Updates**: Requires manual refresh to see opponent moves (no WebSocket)
4. **Limited Offline Support**: Application requires constant backend connectivity
5. **No PWA Features**: Not installable as a progressive web app
6. **Basic Error Messages**: Limited error context provided to users
7. **No Accessibility Features**: Limited keyboard navigation and screen reader support
8. **Fixed Board Size**: UI designed only for 3x3 board

## Future Improvements

1. **Persistent Storage**: Implement localStorage for offline game continuation
2. **Progressive Web App**: Add service worker and install capability
3. **Real-Time Updates**: Implement WebSocket/SignalR for live opponent moves
4. **User Authentication**: Add sign-up, login, and profile management
5. **Enhanced Accessibility**: Add keyboard navigation, screen reader support, ARIA labels
6. **Game Statistics Dashboard**: Display detailed player statistics and game history
7. **Themes and Customization**: Dark mode, custom board colors, piece styles
8. **Social Features**: Share game results, invite friends via links
9. **Mobile Optimization**: Touch-optimized controls and responsive layouts
10. **Animations**: Add smooth transitions and celebratory animations for wins
11. **Sound Effects**: Add optional audio feedback for moves and game events
12. **Multiplayer Lobby**: Browse available games and join live matches
13. **Advanced Game Modes**: Support for larger boards (4x4, 5x5) and time-limited games
14. **Performance**: Code splitting, lazy loading, and bundle size optimization
15. **Testing**: Increase test coverage with Vitest and React Testing Library

## Project Structure

```
tic-tac-toe-ui/
├── src/
│   ├── components/       # React components
│   │   ├── Board.tsx     # Game board component
│   │   ├── Cell.tsx      # Individual cell component
│   │   ├── MoveHistory.tsx # Move history display
│   │   └── Scoreboard.tsx # Player scores display
│   ├── api/
│   │   └── gameapi.ts    # Backend API client
│   ├── types/
│   │   └── game.ts       # TypeScript type definitions
│   ├── App.tsx           # Main app component
│   ├── main.tsx          # React entry point
│   └── index.css         # Global styles
├── public/               # Static assets
├── package.json          # Dependencies
├── tsconfig.json         # TypeScript configuration
├── vite.config.ts        # Vite configuration
└── eslint.config.js      # ESLint rules
```

## Environment Variables

Create a `.env` file in the project root (optional):

```env
VITE_API_BASE_URL=http://localhost:5000/api
VITE_APP_NAME=TicTacToe
VITE_APP_VERSION=1.0.0
```

## Browser Support

- Chrome/Edge: Latest versions
- Firefox: Latest versions
- Safari: 14+
- Mobile browsers: iOS Safari, Chrome Mobile

## Development Tips

- Use React DevTools browser extension for debugging component state
- Enable ESLint in VS Code for real-time linting feedback
- Check TypeScript errors with `npm run type-check`
- Use browser DevTools Network tab to monitor API calls
- Test with browser DevTools device emulation for responsive design

## Support

For issues or questions, please create an issue in the repository or contact the development team.
