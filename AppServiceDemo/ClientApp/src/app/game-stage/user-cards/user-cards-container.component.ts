import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-user-cards',
  templateUrl: './user-cards-container.component.html',
  styleUrls: ['./user-cards-container.component.scss']
})
export class UserCardsContainerComponent implements OnInit {

  cardsState: 'default' | 'flipped' | 'matched';

  users = [{
    name: "CC",
    pointsVote: 8
  },{
    name: "BG",
    pointsVote: 13
  },{
    name: "RE",
    pointsVote: 21
  },{
    name: "DD",
    pointsVote: 8
  },{
    name: "MJ",
    pointsVote: 13
  },{
    name: "JT",
    pointsVote: 13
  },{
    name: "ML",
    pointsVote: 5
  },{
    name: "JG",
    pointsVote: 21
  },{
    name: "RG",
    pointsVote: 13
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
