import { TestBed } from '@angular/core/testing';

import { ViewbalanceService } from './viewbalance.service';

describe('ViewbalanceService', () => {
  let service: ViewbalanceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ViewbalanceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
