import { TestBed } from '@angular/core/testing';

import { IsLibrarianGuard } from './is-librarian.guard';

describe('IsLibrarianGuard', () => {
  let guard: IsLibrarianGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(IsLibrarianGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
