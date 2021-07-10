import { Component } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  showMainMenu = true
  showJoinTeam = false
  showCreateTeam = false

  constructor(private modalService: NgbModal) {}

  openBackDropCustomClass(content) {
    this.modalService.open(content, { backdropClass: 'blur-backdrop' });
  }

  joinTeamClicked() {
    this.showMainMenu = false;
    this.showJoinTeam = true;
  }

  createTeamClicked() {
    this.showMainMenu = false;
    this.showCreateTeam = true;
  }

  backClicked() {
    this.showCreateTeam = false;
    this.showJoinTeam = false;
    this.showMainMenu = true;
  }
}
