import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserEditImgComponent } from './user-edit-img.component';

describe('UserEditImgComponent', () => {
  let component: UserEditImgComponent;
  let fixture: ComponentFixture<UserEditImgComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UserEditImgComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserEditImgComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
