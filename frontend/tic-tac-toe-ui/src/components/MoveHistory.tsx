function MoveHistory({ moves }: { moves: any[] }) {
  if (!moves || moves.length === 0) return null;

  return (
    <div className="move-history">
      <h3>📜 Move History</h3>
      <table>
        <thead>
          <tr>
            <th>#</th>
            <th>Player</th>
            <th>Position</th>
          </tr>
        </thead>
        <tbody>
          {moves.map((move) => (
            <tr key={move.moveNumber}>
              <td>{move.moveNumber}</td>
              <td className={move.player === "X" ? "player-x" : "player-o"}>
                {move.player}
              </td>
              <td>
                Row {move.row + 1}, Col {move.column + 1}
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default MoveHistory;