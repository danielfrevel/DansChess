export class SquareModel {
  constructor(index: number, num: number) {
    this.index = index;
    this.pieceNum = num;
  }
  public index!: number;
  public pieceNum!: number;

  //sqaure[index] = pieceNum
}
