import { Injectable } from '@angular/core';
import { AppCookieService } from './app-cookie.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private cookieService: AppCookieService) { }

  public getUserJwt() {
    return this.cookieService.get('user_jwt');
  }

  public setUserJwt(jwt: string) {
    this.cookieService.set('user_jwt', jwt);
  }

  public removeUserJwt() {
    this.cookieService.remove('user_jwt');
  }
}
