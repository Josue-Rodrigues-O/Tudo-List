import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthComponent } from './pages/auth/auth.component';
import { UiKitComponent } from './pages/ui-kit/ui-kit.component';

const routes: Routes = [
  { path: '', component: AuthComponent },
  { path: 'ui-kit', component: UiKitComponent },
  // { path: '**', component: NotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
