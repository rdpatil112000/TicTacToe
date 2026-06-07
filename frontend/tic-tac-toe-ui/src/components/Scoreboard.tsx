function Scoreboard({ scoreboard }: { scoreboard: any }) {
  if (!scoreboard) return null;

  return (
    <div className="scoreboard">
      <h3>🏆 Scoreboard</h3>
      <div className="score-row">
        <span className="player-x">X Wins</span>
        <span>{scoreboard.xWins}</span>
      </div>
      <div className="score-row">
        <span className="player-o">O Wins</span>
        <span>{scoreboard.oWins}</span>
      </div>
      <div className="score-row">
        <span className="draws">Draws</span>
        <span>{scoreboard.draws}</span>
      </div>
    </div>
  );
}


export default Scoreboard;