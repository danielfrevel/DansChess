import { Component, OnInit, Input, OnChanges} from '@angular/core';

@Component({
  selector: 'app-square',
  templateUrl: './square.component.html',
  styleUrls: ['./square.component.css']
})
export class SquareComponent implements OnInit, OnChanges {
  @Input piecenumber: number;
  @Input index: number;
  constructor() { }

  ngOnInit() {
  }
  //

  ngOnChanges() {
    //piecenumber sollte sich verändern
  }


}
