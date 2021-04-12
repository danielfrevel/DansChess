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
  private board: BehaviorSubject<SquareModel[]> = new BehaviorSubject<
    SquareModel[]
  >([]);
  private moves: BehaviorSubject<Move[]> = new BehaviorSubject<Move[]>([]);
  private highlighted: BehaviorSubject<number[]> = new BehaviorSubject<
    number[]
  >([]);

  public board$: Observable<SquareModel[]> = this.board.asObservable();
  public moves$: Observable<Move[]> = this.moves.asObservable();

  public currentSelectedSquare: BehaviorSubject<SquareModel> = new BehaviorSubject<SquareModel>(
    new SquareModel(0, 0)
  );
  public baseUrl: string = 'https://localhost:5001/game';

  public isMate: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

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
    // observebale => CurrentAvailableMoves
    // moves.filter(t => t.startIdx === currentSqr.Idx)
    // setCurrentSqr(currentsrq){
    //   this.cSqr.next(currentsrq);
    // }
  }

  setSelectedSquareModel(model: SquareModel) {
    this.currentSelectedSquare.next(model);
  }

  makeMove(targetSquare: SquareModel) {
    combineLatest([this.moves, this.currentSelectedSquare])
      .pipe(take(1))
      .subscribe((t) => {
        this.applyMove(
          t[0].filter(
            (x) =>
              x.startSquare === t[1].index &&
              x.targetSquare === targetSquare.index
          )[0]
        );
      });
  }

  initBoard() {
    // let squares: SquareModel[] = [];
    // for (let i = 0; i < 64; i++) {
    //   let square = new SquareModel(i, Math.floor(Math.random() * 16) + 1);
    //   squares.push(square);
    // }
    // this.board.next(squares);
    //public int[] BoardRepresentation { get; set; }
    //    public IEnumerable<Move> Moves { get; set; }
    //    public BoardResultModel()

    this.loadBoard(true);
  }

  toSquareModel(squares: number[]): SquareModel[] {
    let models: SquareModel[] = [];
    squares.forEach((value, idx) => {
      models.push(new SquareModel(idx, value));
    });
    return models;
  }

  initGame() {
    //initial Board holen mit getBoard
    //apiMakeMove(move) callen
    //apiGetBoard() callen -> schickt die neuen Moves wieder
  }

  loadBoard(createNew: boolean = false) {
    this.apiGetBoard(createNew).subscribe((result) => {
      this.board.next(this.toSquareModel(result.boardRepresentation));
      this.moves.next(result.moves);
    });
  }

  apiGetBoard(createNew: boolean = false): Observable<BoardResultModel> {
    return this.http.get<BoardResultModel>(
      `${this.baseUrl}/GetBoard?createNewBoard=${createNew}`
    );
  }

  applyMove(move: Move) {
    this.apiMakeMove(move).subscribe((val) => {
      this.loadBoard(false);
    });
  }
  apiMakeMove(move: Move): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/MakeMove`, move);
  }

  getBoard() {}
}
