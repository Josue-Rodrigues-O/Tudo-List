import { HttpClient, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SignInSignUpComponent } from './features/pages/sign-in-sign-up/sign-in-sign-up.component';
import { TudoListComponent } from './features/pages/tudo-list/tudo-list.component';
import { NotFoundComponent } from './features/pages/not-found/not-found.component';
import { TesteComponent } from './pages-teste/teste/teste.component';
import { MessageBoxComponent } from './features/fragments/message-box/message-box.component';

export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}

@NgModule({
  declarations: [
    AppComponent,
    SignInSignUpComponent,
    TudoListComponent,
    NotFoundComponent,
    TesteComponent,
    MessageBoxComponent,
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    }),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
