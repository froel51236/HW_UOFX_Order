import { TestBed } from '@angular/core/testing';

import { CuseromerService } from './cuseromer.service';

describe('CuseromerService', () => {
  let service: CuseromerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CuseromerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
