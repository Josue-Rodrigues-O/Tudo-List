import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { TranslateModule } from '@ngx-translate/core';
import { AuthService } from '../../services/auth/auth.service';
import { UserImageService } from '../../services/user-image/user-image.service';

@Component({
  selector: 'app-toolbar',
  standalone: true,
  templateUrl: './toolbar.component.html',
  styleUrl: './toolbar.component.scss',
  imports: [
    MatToolbarModule,
    MatIconModule,
    TranslateModule
  ]
})
export class ToolbarComponent implements OnInit, OnDestroy {
  protected user = this.authService.getCurrentUser();
  protected imgProfile: string | null = null;
  constructor(private authService: AuthService, private userImgService: UserImageService) { }

  async ngOnInit(): Promise<void> {
    this.imgProfile = await this.userImgService.getLocalImg();
  }

  ngOnDestroy(): void {
    if (this.imgProfile) {
      URL.revokeObjectURL(this.imgProfile);
    }
  }
}
