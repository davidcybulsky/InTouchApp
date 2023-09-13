import {ComponentFixture, TestBed} from '@angular/core/testing';

import {UserLinksCardComponent} from './user-links-card.component';

describe('UserLinksCardComponent', () => {
  let component: UserLinksCardComponent;
  let fixture: ComponentFixture<UserLinksCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UserLinksCardComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(UserLinksCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
