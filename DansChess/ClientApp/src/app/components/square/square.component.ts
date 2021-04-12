import { DataService } from './../../services/data.service';
import { SquareModel } from 'app/models/SquareModel';
import {
  ChangeDetectionStrategy,
  Component,
  Input,
  OnInit,
} from '@angular/core';
import { BehaviorSubject } from 'rxjs';
@Component({
  selector: 'app-square',
  templateUrl: './square.component.html',
  styleUrls: ['./square.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class SquareComponent implements OnInit {
  @Input()
  public model!: SquareModel;

  public isHigligheted$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(
    false
  );

  constructor(private data: DataService) {}

  ngOnInit(): void {
    this.data.highlighted$.subscribe((val) => {
      // this.isHighlighted = val.includes(this.model.index);
      this.isHigligheted$.next(val.includes(this.model.index));
      // this.cdr.detectChanges();
    });
  }

  showMoves() {
    if (this.isHigligheted$.getValue()) {
      this.data.makeMove(this.model);
    } else {
      this.data.setSelectedSquareModel(this.model);
    }
  }

  getPieceImage() {
    switch (this.model.pieceNum) {
      case 0:
        return '';
      case 9:
        return 'assets/images/wK.png';
      case 10:
        return 'assets/images/wP.png';
      case 11:
        return 'assets/images/wN.png';
      case 13:
        return 'assets/images/wB.png';
      case 14:
        return 'assets/images/wR.png';
      case 15:
        return 'assets/images/wQ.png';
      case 17:
        return 'assets/images/bK.png';
      case 18:
        return 'assets/images/bP.png';
      case 19:
        return 'assets/images/bN.png';
      case 21:
        return 'assets/images/bB.png';
      case 22:
        return 'assets/images/bR.png';
      case 23:
        return 'assets/images/bQ.png';

      default:
        return 'data:image/gif;base64,R0lGODlhAQABAIAAAAAAAP///yH5BAEAAAAALAAAAAABAAEAAAIBRAA7'; //1pixel image
    }
  }
  isLight() {
    let lightList = [
      1,
      3,
      5,
      7,
      8,
      10,
      12,
      14,
      17,
      19,
      21,
      23,
      24,
      26,
      28,
      30,
      33,
      35,
      37,
      39,
      40,
      42,
      44,
      46,
      49,
      51,
      53,
      55,
      56,
      58,
      60,
      62,
    ]; //every index in this list is a light square
    if (lightList.includes(this.model.index)) {
      return true;
    } else return false;
  }
}
