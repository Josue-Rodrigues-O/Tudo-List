import { Component } from '@angular/core';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { RouterOutlet } from '@angular/router';
import { LoadingState } from './states/loading-state';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
  imports: [RouterOutlet, TranslateModule, MatProgressSpinnerModule]
})
export class AppComponent {
  loadinState: LoadingState;
  constructor(_loadingState: LoadingState, private translate: TranslateService) {
    this.loadinState = _loadingState;
    this.translate.addLangs(['en', 'pt']);
    this.translate.setDefaultLang('en');

    const browserLang = this.translate.getBrowserLang();
    this.translate.use(
      browserLang && browserLang.match(/en|pt/) ? browserLang : 'en'
    );
  }
}
