import { Move } from './MoveModel';

export class BoardResultModel {
  public boardRepresentation!: number[];
  public whiteToMove!: boolean;
  public moves!: Move[];
}
