export class Move {
  public startSquare!: number;
  public targetSquare!: number;

  //nur zu test zwecken
  constructor(st: number, ts: number) {
    this.startSquare = st;
    this.targetSquare = ts;
  }
}
