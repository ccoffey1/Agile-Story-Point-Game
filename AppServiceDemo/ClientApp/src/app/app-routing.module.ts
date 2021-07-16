import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { StartGameComponent } from './start-game/start-game.component';


const routes: Routes = [
  { path: 'start', component: StartGameComponent },
  { path: '**', redirectTo: 'start', pathMatch: 'full' } // 404 redirects to start for now
  //{ path: 'game', component: GameStageComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
