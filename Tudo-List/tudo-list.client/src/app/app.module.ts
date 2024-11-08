import { HTTP_INTERCEPTORS, HttpClient, HttpClientModule } from '@angular/common/http';
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
import { ToastComponent } from './features/components/toast/toast.component';
import { MessageBoxComponent } from './features/components/message-box/message-box.component';
import { InputComponent } from './features/components/input/input.component';
import { ngxLoadingAnimationTypes, NgxLoadingModule } from 'ngx-loading';
import { authInterceptor } from './core/interceptor/auth/auth.interceptor';
import { loadingInterceptor } from './core/interceptor/loading/loading.interceptor';
import { LoaderComponent } from './features/components/loader/loader.component';

export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}

@NgModule({
  declarations: [
    AppComponent,
    SignInSignUpComponent,
    TudoListComponent,
    NotFoundComponent,
    ToastComponent,
    MessageBoxComponent,
    InputComponent,
    LoaderComponent,
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
    NgxLoadingModule.forRoot({
      fullScreenBackdrop: true,
      animationType: ngxLoadingAnimationTypes.chasingDots,
      primaryColour: "#5C60F5",
      secondaryColour: "#5a43b0"
    })
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: authInterceptor, multi: true},
    { provide: HTTP_INTERCEPTORS, useClass: loadingInterceptor, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
