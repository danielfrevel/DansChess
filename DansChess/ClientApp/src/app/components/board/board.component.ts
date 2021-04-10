import { Observable, ReplaySubject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { SquareModel } from 'app/models/SquareModel';
import { DataService } from 'app/services/data.service';

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.scss'],
  providers: [DataService],
})
export class BoardComponent implements OnInit, OnDestroy {
  public squares!: SquareModel[];

  public board$: Observable<SquareModel[]> = this.data.board$;

  public unsubscriber: ReplaySubject<boolean> = new ReplaySubject<boolean>();

  constructor(private data: DataService) {}
  ngOnDestroy(): void {
    this.unsubscriber.next(true);
  }

  ngOnInit(): void {
    this.data.initBoard();

    // this.board$ = this.data.board$;
    // this.data.board$
    //   .pipe(takeUntil(this.unsubscriber))
    //   .subscribe((currentBoard) => {
    //     this.squares = currentBoard;
    //   });
    console.log(this);
  }
}
