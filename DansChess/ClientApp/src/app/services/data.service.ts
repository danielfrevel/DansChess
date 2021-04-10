import { BoardResultModel } from './../models/BoardResultModel';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Move } from 'app/models/MoveModel';
import { SquareModel } from 'app/models/SquareModel';
import { BehaviorSubject, Observable, ReplaySubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class DataService {
  private board: ReplaySubject<SquareModel[]> = new ReplaySubject<
    SquareModel[]
  >();
  private moves: BehaviorSubject<Move[]> = new BehaviorSubject<Move[]>([]);

  public board$: Observable<SquareModel[]> = this.board.asObservable();
  public moves$: Observable<Move[]> = this.moves.asObservable();

  public baseUrl: string = 'http://localhost:5000/api/game';

  constructor(private http: HttpClient) {}

  initBoard() {
    let squares: SquareModel[] = [];
    for (let i = 0; i < 64; i++) {
      let square = new SquareModel(i, Math.floor(Math.random() * 16) + 1);
      squares.push(square);
    }
    this.board.next(squares);
  }

  apiCreateNewBoard(): Observable<BoardResultModel> {
    return this.http.get<BoardResultModel>(`${this.baseUrl}/CreateNewGame`);
  }

  getMoves() {}

  getBoard() {}
}
