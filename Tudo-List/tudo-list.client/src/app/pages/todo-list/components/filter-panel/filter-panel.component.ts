import { MatCardModule } from '@angular/material/card';
import { Component } from '@angular/core';
import {
  ReactiveFormsModule,
  FormControl,
  FormsModule,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { TranslateModule } from '@ngx-translate/core';
import { PriorityEnum } from '../../../../core/enums/priority-enum';
import { StatusEnum } from '../../../../core/enums/status-enum';
import { MatSelectModule } from '@angular/material/select';
import { MatRadioModule } from '@angular/material/radio';
import { A11yModule } from "@angular/cdk/a11y";
import { Router } from '@angular/router';

@Component({
  selector: 'app-filter-panel',
  standalone: true,
  templateUrl: './filter-panel.component.html',
  styleUrl: './filter-panel.component.scss',
  imports: [
    MatCardModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    MatButtonModule,
    TranslateModule,
    ReactiveFormsModule,
    MatSelectModule,
    MatRadioModule,
    A11yModule
  ],
})
export class FilterPanelComponent {
  title = new FormControl('');
  priority = new FormControl<PriorityEnum | string>('');
  status = new FormControl<StatusEnum | string>('');

  priorityEnum = PriorityEnum;
  statusEnum = StatusEnum;

  constructor(private router: Router) { }

  onClickApplyFilters() {
    let priorityValue = this.priority.value === ''
      ? undefined
      : Number(this.priority.value);

    let statusValue = this.status.value === ''
      ? undefined
      : Number(this.status.value);

    this.router.navigate([], {
      queryParams: {
        title: this.title.value || undefined,
        priority: priorityValue,
        status: statusValue
      }
    });
  }
}
