import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccountPhotoCardComponent } from './account-photo-card.component';

describe('AccountPhotoCardComponent', () => {
  let component: AccountPhotoCardComponent;
  let fixture: ComponentFixture<AccountPhotoCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AccountPhotoCardComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AccountPhotoCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
