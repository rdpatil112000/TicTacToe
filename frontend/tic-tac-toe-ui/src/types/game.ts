export interface Move {
  moveNumber:number;
  player:string;
  row:number;
  column:number;
}

export interface Game {
  id:string;
  board:string[];
  currentPlayer:string;
  status:string;
  winner:string;
  winningCells:number[];
  moveHistory:Move[];
}