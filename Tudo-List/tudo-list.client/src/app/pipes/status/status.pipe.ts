import { Pipe, PipeTransform } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { StatusEnum } from '../../core/enums/status-enum';

@Pipe({
  name: 'status',
  standalone: true
})
export class StatusPipe implements PipeTransform {
  constructor(private translate: TranslateService) {

  }

  transform(value: number): string {
    switch (value) {
      case StatusEnum.NotStarted:
        return this.translate.instant('enums.status.notStarted');

      case StatusEnum.InProgress:
        return this.translate.instant('enums.status.inProgress');

      case StatusEnum.Completed:
        return this.translate.instant('enums.status.completed');

      default:
        return 'VERIFICAR TRADUÇÃO DO SITE';
    }
  }
}
