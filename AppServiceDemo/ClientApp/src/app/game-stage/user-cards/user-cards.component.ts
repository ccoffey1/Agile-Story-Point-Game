import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-user-cards',
  templateUrl: './user-cards.component.html',
  styleUrls: ['./user-cards.component.scss']
})
export class UserCardsComponent implements OnInit {

  users = [{
    name: "CC"
  },{
    name: "BG"
  },{
    name: "RE"
  },{
    name: "DD"
  },{
    name: "MJ"
  },{
    name: "JT"
  },{
    name: "ML"
  },{
    name: "JG"
  },{
    name: "RG"
  }]

  constructor() { }

  ngOnInit(): void {
  }

}
