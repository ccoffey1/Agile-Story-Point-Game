import { trigger, state, style, transition, animate } from '@angular/animations';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-user-cards',
  templateUrl: './user-cards.component.html',
  styleUrls: ['./user-cards.component.scss'],
  animations: [
    trigger('cardFlip', [
      state('default', style({
        transform: 'none'
      })),
      state('flipped', style({
        transform: 'rotateY(180deg)'
      })),
      transition('default => flipped', [
        animate('400ms')
      ]),
      transition('flipped => default', [
        animate('200ms')
      ])
    ])
  ]
})
export class UserCardsComponent implements OnInit {

  cardsState: 'default' | 'flipped' | 'matched' = 'default';

  users = [{
    name: "CC",
    pointsVote: 8,
    reasons: []
  },{
    name: "BG",
    pointsVote: 13,
    reasons: [
      "New Territory",
      "Minimal Dev Work",
      "QA Buffer"
    ]
  },{
    name: "RE",
    pointsVote: 21,
    reasons: [
      "New Territory",
      "Medium Dev Work"
    ]
  },{
    name: "DD",
    pointsVote: 8,
    reasons: [
      "Tons of Dev Work",
      "QA Buffer"
    ]
  },{
    name: "MJ",
    pointsVote: 13,
    reasons: [
      "¯\\_(ツ)_/¯",
      "QA Buffer"
    ]
  },{
    name: "JT",
    pointsVote: 13,
    reasons: [
      "New Territory",
      "Medium Dev Work"
    ]
  },{
    name: "ML",
    pointsVote: 5,
    reasons: [
      "Medium Dev Work",
      "QA Buffer"
    ]
  },{
    name: "JG",
    pointsVote: 21,
    reasons: []
  },{
    name: "RG",
    pointsVote: 13,
    reasons: [
      "Medium Dev Work",
      "QA Buffer"
    ]
  }]

  constructor() { }

  ngOnInit(): void {
  }

  flipCards() {
    if (this.cardsState === "default") {
      this.cardsState = "flipped";
    } else {
      this.cardsState = "default";
    }  
  }

}
