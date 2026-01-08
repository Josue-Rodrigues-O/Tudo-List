import { Component } from '@angular/core';
import { Router, RouterOutlet } from "@angular/router";
import { ToolbarComponent } from "../../../components/toolbar/toolbar.component";
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatButton } from '@angular/material/button';
import { MatIcon } from "@angular/material/icon";
import { MatListModule } from '@angular/material/list';
import { TranslateModule } from '@ngx-translate/core';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { MatButtonModule } from '@angular/material/button';
import { AuthService } from '../../../services/auth/auth.service';


@Component({
  selector: 'app-main-layout',
  imports: [
    RouterOutlet,
    ToolbarComponent,
    MatSidenavModule,
    MatButton,
    MatIcon,
    MatListModule,
    TranslateModule,
    MatIconModule,
    MatDividerModule,
    MatButtonModule],
  templateUrl: './main-layout.component.html',
  styleUrl: './main-layout.component.scss',
})
export class MainLayoutComponent {

  constructor(private readonly authService: AuthService, private readonly router: Router) {

  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/auth/login']);
  }
}
