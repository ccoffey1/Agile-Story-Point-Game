import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { GameStageComponent } from './components/game-stage/game-stage.component';
import { AuthorizeGuard } from './services/guards/authorize-guard.service';
import { StartGameComponent } from './components/start-game/start-game.component';


const routes: Routes = [
  { path: 'start', component: StartGameComponent },
  { path: 'game', component: GameStageComponent, canActivate: [AuthorizeGuard] },
  { path: '**', redirectTo: 'start', pathMatch: 'full' } // 404 redirects to start for now
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
