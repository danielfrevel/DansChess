import { Component } from "@angular/core";

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
})
export class HomeComponent {

  newGame() {
    //board von holen und an
    //service passen
  }

  loadGame() {
    //anderer Api call als newGame()
    //Board.LoadPosition() aufrufen //-> Get/LoadPosition oder so
  }
  
}
