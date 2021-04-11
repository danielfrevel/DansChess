import { Observable, ReplaySubject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
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

  public unsubscriber: ReplaySubject<boolean> = new ReplaySubject<boolean>();

  constructor(public data: DataService, private cdr: ChangeDetectorRef) {}
  ngOnDestroy(): void {
    this.unsubscriber.next(true);
  }

  ngOnInit(): void {
    this.data.initBoard();

    // this.data.board$
    //   .pipe(takeUntil(this.unsubscriber))
    //   .subscribe((currentBoard) => {
    //     this.squares = currentBoard;
    //     this.cdr.detectChanges();
    //   });
    console.log(this);
  }
}
