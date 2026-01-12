import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserEditNameComponent } from './user-edit-name.component';

describe('UserEditNameComponent', () => {
  let component: UserEditNameComponent;
  let fixture: ComponentFixture<UserEditNameComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UserEditNameComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserEditNameComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
