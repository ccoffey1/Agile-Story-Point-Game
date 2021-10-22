import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { CreateGameSessionResponse } from '../models/response/create-game-session-request'
import { CreateGameSessionRequest } from '../models/request/create-game-session-request';
import { JoinGameSessionRequest } from '../models/request/join-game-session-request';
import { JoinGameSessionResponse } from '../models/response/join-game-session-response';
import { AuthService } from './auth.service';
import { GameSessionDataResponse } from '../models/response/game-session-data-response';

@Injectable({
  providedIn: 'root'
})
export class GameSessionService {

  constructor(
    private http: HttpClient,
    private authService: AuthService) { }

  /** POST: adds a new game session to the database and gets a JWT for the user */
  createGameSession(request: CreateGameSessionRequest): Observable<CreateGameSessionResponse> {
    return this.http.post<CreateGameSessionResponse>(`/api/game/create`, request).pipe(
      tap(
        res => this.authService.setUserJwt(res.playerJWT),
        err => console.error(err)
      )
    )
  }

  /** POST: joins an existing game session and gets a JWT for the user */
  joinGameSession(request: JoinGameSessionRequest): Observable<JoinGameSessionResponse> {
    return this.http.post<JoinGameSessionResponse>(`/api/game/join`, request).pipe(
      tap(
        res => this.authService.setUserJwt(res.playerJWT),
        err => console.error(err)
      )
    )
  }

  /** GET: fetches all the players belonging to a game; server determines what game from JWT */
  getGameSessionData(): Observable<GameSessionDataResponse> {
    return this.http.get<GameSessionDataResponse>('/api/game/sessiondata');
  }
}