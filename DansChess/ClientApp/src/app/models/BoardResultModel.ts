import { Move } from './MoveModel';

export class BoardResultModel {
  public squares!: number[];
  public whiteToMove!: boolean;
  public moves!: Move[];
}
