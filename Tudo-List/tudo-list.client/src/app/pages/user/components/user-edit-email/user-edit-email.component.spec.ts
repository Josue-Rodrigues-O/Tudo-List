import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserEditEmailComponent } from './user-edit-email.component';

describe('UserEditEmailComponent', () => {
  let component: UserEditEmailComponent;
  let fixture: ComponentFixture<UserEditEmailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UserEditEmailComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserEditEmailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
