import { Pipe, PipeTransform } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { PriorityEnum } from '../../core/enums/priority-enum';

@Pipe({
  name: 'priority',
  standalone: true
})
export class PriorityPipe implements PipeTransform {
  constructor(private translate: TranslateService) {

  }

  transform(value: number): string {
    switch (value) {
      case PriorityEnum.Low:
        return this.translate.instant('enums.priority.low');

      case PriorityEnum.Medium:
        return this.translate.instant('enums.priority.medium');

      case PriorityEnum.High:
        return this.translate.instant('enums.priority.high');

      default:
        return 'VERIFICAR TRADUÇÃO DO SITE';
    }
  }
}
