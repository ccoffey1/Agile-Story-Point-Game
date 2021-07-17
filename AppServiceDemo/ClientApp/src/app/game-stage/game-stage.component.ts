import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-game-stage',
  templateUrl: './game-stage.component.html',
  styleUrls: ['./game-stage.component.scss']
})
export class GameStageComponent implements OnInit {

  reasons: any[] = [
    { title: '¯\\_(ツ)_/¯', description: "I have no idea.", selected: false },
    { title: 'New Territory', description: "We're unfamiliar with this area.", selected: false },
    { title: 'Minimal Dev Work', description: "Won't take too long.", selected: false },
    { title: 'Medium Dev Work', description: "Quite a bit of work.", selected: false },
    { title: 'Tons of Dev Work', description: "Hopefully we'll finish by retirement.", selected: false },
    { title: 'QA Buffer', description: "More time for QE work.", selected: false },
  ]

  constructor() { }

  ngOnInit(): void {
  }

  /**
   * (For debugging) Print out reasons array.
   */
  printReasons() {
    console.table(this.reasons)
  }
}
