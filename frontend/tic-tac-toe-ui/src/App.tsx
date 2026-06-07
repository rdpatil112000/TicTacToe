import { useEffect, useState } from "react";
import api from "./api/gameapi";

import Board from "./components/Board";
import MoveHistory from "./components/MoveHistory";
import Scoreboard from "./components/Scoreboard";

import "./App.css";

function App() {
  const [game, setGame] = useState<any>(null);
  const [scoreboard, setScoreboard] = useState<any>(null);
  const [mode, setMode] = useState<number>(1);
  const [message, setMessage] = useState<string | null>(null);

  useEffect(() => {
    createGame(mode);
    loadScoreboard();
  }, []);

  const loadScoreboard = async () => {
    const response = await api.get("/scoreboard");
    setScoreboard(response.data);
  };

  const createGame = async (selectedMode: number) => {
    const response = await api.post("/games", { mode: selectedMode });
    setGame(response.data);
  };

  const handleMove = async (index: number) => {
    if (!game) return;

    if (game.board[index] !== null && game.board[index] !== "") {
      setMessage("⚠️ Please select another cell, it is already taken!");
      setTimeout(() => setMessage(null), 2000);
      return; 
    }

    try {
      const response = await api.post(`/games/${game.id}/moves`, {
        cellIndex: index,
        player: game.currentPlayer,
      });
      setGame(response.data);
      loadScoreboard();
    } catch (err: any) {
      
      const errorMsg =
        err.response?.data?.message || "Unexpected error occurred!";
      setMessage(errorMsg);
      setTimeout(() => setMessage(null), 2000);
      console.error(err);
    }
  };

  const undoMove = async () => {
    const response = await api.post(`/games/${game.id}/undo`);
    setGame(response.data);
  };

  const resetGame = async () => {
    const response = await api.post(`/games/${game.id}/reset`);
    setGame(response.data);
    setMessage("✅ Game reset successfully!");
    setTimeout(() => setMessage(null), 2000);
  };

  const resetScoreboard = async () => {
    await api.post("/scoreboard/reset");
    loadScoreboard();
    setMessage("✅ Scoreboard reset successfully!");
    setTimeout(() => setMessage(null), 2000);
  };

  if (!game) return <div>Loading...</div>;

  return (
    <div className="container">
      <h1>Tic Tac Toe</h1>

      <div>
        <label>Game Mode</label>
        <select
          value={mode}
          onChange={async (e) => {
            const selected = Number(e.target.value);
            setMode(selected);
            await createGame(selected);
          }}
        >
          <option value={1}>Two Player</option>
          <option value={2}>Computer</option>
        </select>
      </div>

      {message && (
        <div
          className={`alert ${
            message.includes("successfully") ? "success" : "error"
          }`}
        >
          {message}
        </div>
      )}

      {game.status === 0 && <h3>Current Player : {game.currentPlayer}</h3>}

      {game.status === 1 && (
        <div className="end-screen">
          <h2>WINNER!</h2>
          <div className="winner-symbol">{game.winner}</div>
          <button onClick={resetGame}>Restart Game</button>
        </div>
      )}

      {game.status === 2 && (
        <div className="end-screen">
          <h2>DRAW!</h2>
          <button onClick={resetGame}>Restart Game</button>
        </div>
      )}

      {game.status === 0 && (
        <>
          <Board
            board={game.board}
            winningCells={game.winningCells}
            onMove={handleMove}
          />
          <br />
          <button onClick={undoMove} disabled={game.moveHistory.length === 0}>
            Undo
          </button>
          <button className="restart" onClick={resetGame}>
            Restart Game
          </button>
          <button className="reset-scoreboard" onClick={resetScoreboard}>
            Reset Scoreboard
          </button>
        </>
      )}

      <Scoreboard scoreboard={scoreboard} />
      <MoveHistory moves={game.moveHistory} />
    </div>
  );
}

export default App;
