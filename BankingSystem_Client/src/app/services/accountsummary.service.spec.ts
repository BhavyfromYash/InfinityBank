import { TestBed } from '@angular/core/testing';

import { AccountSummaryService } from './accountsummary.service';

describe('AccountsummaryService', () => {
  let service: AccountSummaryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AccountSummaryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
