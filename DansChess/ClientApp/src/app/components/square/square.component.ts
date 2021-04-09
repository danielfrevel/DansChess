import { SquareModel } from 'app/models/SquareModel';
import { Component, Input, OnInit } from '@angular/core';
import { ConfigService } from '../../services/config.service';
import { NONE_TYPE } from '@angular/compiler';
SquareModel;
@Component({
  selector: 'app-square',
  templateUrl: './square.component.html',
  styleUrls: ['./square.component.scss'],
  providers: [ConfigService],
})
export class SquareComponent implements OnInit {
  @Input()
  public model!: SquareModel;

  public colorClass: string = 'black';

  constructor(private config: ConfigService) {}

  ngOnInit(): void {}

  pieceImage() {
    switch (this.model.pieceNum) {
      case 0:
        break;
      case 9:
        break;
      case 10:
        break;
      case 11:
        break;
      case 13:
        break;
      case 14:
        break;
      case 15:
        break;
      case 17:
        break;
      case 18:
        break;
      case 19:
        break;
      case 21:
        break;
      case 22:
        break;
      case 23:
        break;

      default:
        return NONE_TYPE;
        break;
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
