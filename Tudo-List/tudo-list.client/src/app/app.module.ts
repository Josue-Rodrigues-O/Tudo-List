import { HttpClient, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ngxLoadingAnimationTypes, NgxLoadingModule } from 'ngx-loading';
import { AuthComponent } from './pages/auth/auth.component';
import { UserComponent } from './pages/user/user.component';
import { TasksComponent } from './pages/tasks/tasks.component';
import { UiKitComponent } from './pages/ui-kit/ui-kit.component';
import { InputComponent } from './components/input/input.component';
import { ButtonComponent } from './components/button/button.component';

export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}

@NgModule({
  declarations: [AppComponent, AuthComponent, UserComponent, TasksComponent, UiKitComponent, InputComponent, ButtonComponent],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient],
      },
    }),
    NgxLoadingModule.forRoot({
      fullScreenBackdrop: true,
      animationType: ngxLoadingAnimationTypes.chasingDots,
      primaryColour: '#5C60F5',
      secondaryColour: '#5a43b0',
    }),
  ],
  providers: [
    // { provide: HTTP_INTERCEPTORS, useClass: authInterceptor, multi: true},
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
