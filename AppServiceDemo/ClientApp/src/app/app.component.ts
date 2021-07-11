import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { GameSessionService } from './services/game-session.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  showMainMenu = true
  showJoinGameSession = false
  showCreateGameSession = false

  gameSessionForm = new FormGroup({
    playerName: new FormControl(''),
    gameSessionName: new FormControl('')
  })

  joinSessionForm = new FormGroup({
    playerName: new FormControl(''),
    joinCode: new FormControl('')
  })

  constructor(
    private modalService: NgbModal, 
    private gameSessionService: GameSessionService) {}

  openBackDropCustomClass(content) {
    this.modalService.open(content, { backdropClass: 'blur-backdrop' });
  }

  joinGameSessionClicked() {
    this.showMainMenu = false;
    this.showJoinGameSession = true;
  }

  createGameSessionClicked() {
    this.showMainMenu = false;
    this.showCreateGameSession = true;
  }

  backClicked() {
    this.showCreateGameSession = false;
    this.showJoinGameSession = false;
    this.showMainMenu = true;
  }

  createGameSession() {
    this.gameSessionService
      .createGameSession(this.gameSessionForm.value)
      .subscribe(gameSessionResponse => console.log(gameSessionResponse))
  }

  joinGameSession() {
    this.gameSessionService
      .joinGameSession(this.joinSessionForm.value)
      .subscribe(joinSessionResponse => console.log(joinSessionResponse))
  }
}
