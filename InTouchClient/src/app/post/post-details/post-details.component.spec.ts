import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PostpageComponent } from './post-details.component';

describe('PostpageComponent', () => {
  let component: PostpageComponent;
  let fixture: ComponentFixture<PostpageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ PostpageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PostpageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
