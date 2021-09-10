import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-user-cards',
  templateUrl: './user-cards.component.html',
  styleUrls: ['./user-cards.component.scss']
})
export class UserCardsComponent implements OnInit {

  users = [{
    name: "hello"
  },{
    name: "hello"
  },{
    name: "hello"
  },{
    name: "hello"
  },{
    name: "hello"
  },{
    name: "hello"
  },{
    name: "hello"
  },{
    name: "hello"
  },{
    name: "hello"
  }]

  constructor() { }

  ngOnInit(): void {
  }

}
