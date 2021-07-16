import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-game-stage',
  templateUrl: './game-stage.component.html',
  styleUrls: ['./game-stage.component.scss']
})
export class GameStageComponent implements OnInit {

  reasons: any[] = [
    { title: '¯\\_(ツ)_/¯', description: "I have no idea."},
    { title: 'New Territory', description: "We're unfamiliar with this area."},
    { title: 'Minimal Dev Work', description: "Won't take too long."},
    { title: 'Medium Dev Work', description: "Quite a bit of work."},
    { title: 'Tons of Dev Work', description: "Hopefully we'll finish by retirement."},
    { title: 'QA Buffer', description: "More time for QE work."},
  ]

  constructor() { }

  ngOnInit(): void {
  }

}
