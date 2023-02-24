import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GrindingCompetitionRoutes } from './pages/grindingcompetition/grindingcompetition.routes';

const routes: Routes = [
  ...GrindingCompetitionRoutes,
  {
    path: '**',
    redirectTo: 'grindingcompetition'
  }
];

const isIframe = window !== window.parent && !window.opener;

@NgModule({
  imports: [RouterModule.forRoot(routes,  {
    // Don't perform initial navigation in iframes
    initialNavigation: !isIframe ? 'enabled' : 'disabled'
  })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
