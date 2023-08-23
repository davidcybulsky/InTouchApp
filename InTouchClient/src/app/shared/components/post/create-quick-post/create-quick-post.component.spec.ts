import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateQuickPostComponent } from './create-quick-post.component';

describe('CreateQuickPostComponent', () => {
  let component: CreateQuickPostComponent;
  let fixture: ComponentFixture<CreateQuickPostComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ CreateQuickPostComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateQuickPostComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
