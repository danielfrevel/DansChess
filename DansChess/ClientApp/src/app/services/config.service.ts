import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ConfigService {
  public isFlipped: boolean = false;

  constructor() {}
}
