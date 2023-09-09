import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FriendCardPanelComponent } from './friend-card-panel.component';

describe('FriendCardPanelComponent', () => {
  let component: FriendCardPanelComponent;
  let fixture: ComponentFixture<FriendCardPanelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ FriendCardPanelComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FriendCardPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
