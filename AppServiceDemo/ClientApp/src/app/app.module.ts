import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ClipboardModule } from '@angular/cdk/clipboard';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { StartGameComponent } from './components/start-game/start-game.component';
import { GameStageComponent } from './components/game-stage/game-stage.component';
import { UserCardsComponent } from './components/game-stage/user-cards/user-cards.component';
import { UniversalAppInterceptor } from './services/interceptors/universal-app-interceptor.service';

@NgModule({
  declarations: [
    AppComponent,
    StartGameComponent,
    GameStageComponent,
    UserCardsComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    NgbModule,
    FormsModule,
    ReactiveFormsModule,
    FontAwesomeModule,
    ClipboardModule
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: UniversalAppInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
