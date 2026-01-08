import { Component } from '@angular/core';
import { UserEditingFormComponent } from "./components/user-editing-form/user-editing-form.component";

@Component({
  selector: 'app-user',
  imports: [UserEditingFormComponent],
  templateUrl: './user.component.html',
  styleUrl: './user.component.scss',
})
export class UserComponent {

}
