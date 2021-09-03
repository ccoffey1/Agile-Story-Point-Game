import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { GameStageComponent } from './game-stage/game-stage.component';
import { StartGameComponent } from './start-game/start-game.component';


const routes: Routes = [
  { path: 'start', component: StartGameComponent },
  { path: 'game', component: GameStageComponent },
  { path: '**', redirectTo: 'game', pathMatch: 'full' } // 404 redirects to start for now
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
