import { BoardResultModel } from '../models/BoardResultModel';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Move } from 'app/models/MoveModel';
import { SquareModel } from 'app/models/SquareModel';
import {
  BehaviorSubject,
  combineLatest,
  Observable,
  ReplaySubject,
} from 'rxjs';
import { map, shareReplay, take, timeoutWith } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class DataService {
  private board: ReplaySubject<SquareModel[]> = new ReplaySubject<
    SquareModel[]
  >();
  private moves: BehaviorSubject<Move[]> = new BehaviorSubject<Move[]>([]);
  private highlighted: BehaviorSubject<number[]> = new BehaviorSubject<
    number[]
  >([]);

  public board$: Observable<SquareModel[]> = this.board.asObservable();
  public moves$: Observable<Move[]> = this.moves.asObservable();

  public currentSelectedSquare: BehaviorSubject<SquareModel> = new BehaviorSubject<SquareModel>(
    new SquareModel(0, 0)
  );
  public baseUrl: string = 'http://localhost:5000/api/game';

  public highlighted$: Observable<number[]> = combineLatest([
    this.currentSelectedSquare,
    this.moves$,
  ]).pipe(
    map((value) => {
      return value[1]
        .filter((t) => t.startSquare === value[0].index)
        .map((t) => t.targetSquare);
    })
  );
  constructor(private http: HttpClient) {
    this.genRandomMoves();
    // observebale => CurrentAvailableMoves
    // moves.filter(t => t.startIdx === currentSqr.Idx)
    // setCurrentSqr(currentsrq){
    //   this.cSqr.next(currentsrq);
    // }
  }

  setSelectedSquareModel(model: SquareModel) {
    this.currentSelectedSquare.next(model);
  }

  genRandomMoves() {
    let randomoves: Move[] = [];

    //for (let i = 0; i < 20; i++) {
    randomoves.push(new Move(1, 2));
    randomoves.push(new Move(1, 3));
    randomoves.push(new Move(1, 4));
    randomoves.push(new Move(1, 5));

    //}

    this.moves.next(randomoves);
  }

  initBoard() {
    let squares: SquareModel[] = [];
    for (let i = 0; i < 64; i++) {
      let square = new SquareModel(i, Math.floor(Math.random() * 16) + 1);
      squares.push(square);
    }
    this.board.next(squares);
  }

  initGame() {
    //initial Board holen mit getBoard
    //apiMakeMove(move) callen
    //apiGetBoard() callen -> schickt die neuen Moves wieder
  }

  apiGetBoard(): Observable<BoardResultModel> {
    return this.http.get<BoardResultModel>(`${this.baseUrl}/GetBoard`);
  }

  apiMakeMove(move: Move): void {
    this.http.post(`${this.baseUrl}/MakeMove`, move);
  }

  getBoard() {}
}
