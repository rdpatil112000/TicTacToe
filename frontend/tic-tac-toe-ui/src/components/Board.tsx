import Cell from "./Cell";

interface Props {
  board: string[];
  winningCells: number[];
  onMove: (index: number) => void;
}

function Board({
  board,
  winningCells,
  onMove
}: Props) {
  return (
    <div className="board">
      {board.map((cell, index) => (
        <Cell
          key={index}
          value={cell || ""}
          highlight={winningCells.includes(index)}
          onClick={() => onMove(index)}
        />
      ))}
    </div>
  );
}

export default Board;