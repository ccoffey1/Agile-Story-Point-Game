import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { of, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthService } from 'src/app/services/auth.service';
import { GameSessionService } from '../../services/game-session.service';
import { faChevronRight } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-start-game',
  templateUrl: './start-game.component.html',
  styleUrls: ['./start-game.component.scss']
})
export class StartGameComponent implements OnInit {

  faChevronRight = faChevronRight
  showMainMenu = true
  showJoinGameSession = false
  showCreateGameSession = false
  showRejoinGameSession = false
  
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

  ngOnInit(): void {
    this.getActiveGameSession();
  }

  rejoinGameSessionClicked() {
    this.router.navigate(['game']);
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
      });
  }

  joinGameSession() {
    this.gameSessionService
      .joinGameSession(this.joinSessionForm.value)
      .subscribe(joinSessionResponse => {
        this.router.navigate(['game']);
        console.log(joinSessionResponse);
      });
  }

  getActiveGameSession() {
    if (this.authService.getUserJwt() !== null) {
      this.gameSessionService
        .getGameSessionData()
        .pipe(
          catchError(err => {
            if (err.status === 404) {
              return of(null); // replace with null
            } else {
              return throwError(err); // yeet error as expected for something else
            }
          })
        )
        .subscribe(gameSessionResponse => {
          console.log(gameSessionResponse);
          this.showRejoinGameSession = gameSessionResponse !== null;
        });
    } else {
      this.showRejoinGameSession = false;
    }
  }
}