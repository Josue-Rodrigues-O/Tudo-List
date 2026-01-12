import { Component } from '@angular/core';
import { UserEditImgComponent } from "./components/user-edit-img/user-edit-img.component";
import { UserEditNameComponent } from "./components/user-edit-name/user-edit-name.component";
import { UserEditEmailComponent } from "./components/user-edit-email/user-edit-email.component";
import { UserEditPasswordComponent } from "./components/user-edit-password/user-edit-password.component";

@Component({
  selector: 'app-user',
  imports: [UserEditImgComponent, UserEditNameComponent, UserEditEmailComponent, UserEditPasswordComponent],
  templateUrl: './user.component.html',
  styleUrl: './user.component.scss',
})
export class UserComponent {

}
