import { Component, OnInit } from '@angular/core';
import { SquareModel } from 'app/models/SquareModel';
import { DataService } from 'app/services/data.service';

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.scss'],
  providers: [DataService],
})
export class BoardComponent implements OnInit {
  public squares!: SquareModel[];

  constructor(private data: DataService) {}

  ngOnInit(): void {
    this.squares = new Array<SquareModel>();
    let board = this.data.getNewBoard();
    for (let i = 0; i < 64; i++)
      this.squares.push({ index: i, pieceNum: board[i] } as SquareModel);
  }
}
