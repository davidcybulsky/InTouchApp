import { TestBed } from '@angular/core/testing';

import { CanActivateUserPageGuard } from './can-activate-user-page.guard';

describe('CanActivateUserPageGuard', () => {
  let guard: CanActivateUserPageGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(CanActivateUserPageGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
