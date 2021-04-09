import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
//Wie designt man einen Dataservice? Pro component?wahrscheinlich pro funktionilität? -> Bennung, ergibt sich vermutlich daraus?
export class DataService {
  constructor() {}

  getMoves() {}

  getBoard() {}

  getNewBoard() {
    let squares: number[] = [];
    for (let i = 0; i < 64; i++)
      squares.push(Math.floor(Math.random() * 16) + 1);
    return squares;
  }
}
