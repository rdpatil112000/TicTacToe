interface Props {
  value: string;
  onClick: () => void;
  highlight: boolean;
}

function Cell({ value, onClick, highlight }: Props) {
  return (
    <button
  className={`cell ${highlight ? "winner" : ""} ${value === "X" ? "player-x" : value === "O" ? "player-o" : ""}`}
  onClick={onClick}
>
  {value}
</button>

  );
}

export default Cell;
