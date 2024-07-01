import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SignInSignUpComponent } from './pages/sign-in-sign-up/sign-in-sign-up.component';
import { TudoListComponent } from './pages/tudo-list/tudo-list.component';

@NgModule({
  declarations: [
    AppComponent,
    SignInSignUpComponent,
    TudoListComponent,
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
