import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SignInSignUpComponent } from './features/pages/sign-in-sign-up/sign-in-sign-up.component';
import { TudoListComponent } from './features/pages/tudo-list/tudo-list.component';
import { NotFoundComponent } from './features/pages/not-found/not-found.component';

const routes: Routes = [
  { path: '', component: SignInSignUpComponent },
  { path: 'tudo-list', component: TudoListComponent },
  { path: '**', component: NotFoundComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
