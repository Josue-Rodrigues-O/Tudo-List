import { Component } from '@angular/core';
import { LoadingService } from '../../../shared/services/loading/loading.service';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-loader',
  templateUrl: './loader.component.html',
  styleUrl: './loader.component.scss',
})
export class LoaderComponent {
  public isLoading: Subject<boolean> = this.loadingService.isLoading;

  constructor(private loadingService: LoadingService) {}
}
