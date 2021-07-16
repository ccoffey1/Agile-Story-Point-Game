import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { CreateGameSessionResponse } from '../models/response/create-game-session-request'
import { CreateGameSessionRequest } from '../models/request/create-game-session-request';
import { JoinGameSessionRequest } from '../models/request/join-game-session-request';
import { JoinGameSessionResponse } from '../models/response/join-game-session-response';

@Injectable({
  providedIn: 'root'
})
export class GameSessionService {

  constructor(private http: HttpClient) { }

  /** POST: adds a new game session to the database and gets a JWT for the user */
  createGameSession(request: CreateGameSessionRequest): Observable<CreateGameSessionResponse> {
    return this.http.post<CreateGameSessionResponse>(`/api/game/create`, request).pipe(
      tap(
        res => this.setSession(res.playerJWT),
        err => console.error(err)
      )
    )
  }

  /** POST: joins an existing game session and gets a JWT for the user */
  joinGameSession(request: JoinGameSessionRequest): Observable<JoinGameSessionResponse> {
    return this.http.post<JoinGameSessionResponse>(`/api/game/join`, request).pipe(
      tap(
        res => this.setSession(res.playerJWT),
        err => console.error(err)
      )
    )
  }

  private setSession(authResult) {
    localStorage.setItem('id_token', authResult.playerJWT);
  }
}

/*
private handleError(error: HttpErrorResponse) {
  if (error.status === 0) {
    // A client-side or network error occurred. Handle it accordingly.
    console.error('An error occurred:', error.error);
  } else {
    // The backend returned an unsuccessful response code.
    // The response body may contain clues as to what went wrong.
    console.error(
      `Backend returned code ${error.status}, body was: `, error.error);
  }
  // Return an observable with a user-facing error message.
  return throwError(
    'Something bad happened; please try again later.');
}

...

getConfig() {
  return this.http.get<Config>(this.configUrl)
    .pipe(
      retry(3), // retry a failed request up to 3 times
      catchError(this.handleError)
    );
}
*/