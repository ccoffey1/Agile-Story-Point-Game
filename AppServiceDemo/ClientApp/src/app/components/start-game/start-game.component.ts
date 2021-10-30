import { Component } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AuthService } from 'src/app/services/auth.service';
import { GameSessionService } from '../../services/game-session.service';

@Component({
  selector: 'app-start-game',
  templateUrl: './start-game.component.html',
  styleUrls: ['./start-game.component.scss']
})
export class StartGameComponent {

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
    private gameSessionService: GameSessionService,
    private authService: AuthService,
    private router: Router) {}

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
      .subscribe(gameSessionResponse => { 
        this.router.navigate(['game']);
        console.log(gameSessionResponse);
      })
  }

  joinGameSession() {
    this.gameSessionService
      .joinGameSession(this.joinSessionForm.value)
      .subscribe(joinSessionResponse => {
        this.router.navigate(['game']);
        console.log(joinSessionResponse);
      })  }
}
