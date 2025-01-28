import { TestBed } from '@angular/core/testing';

import { AccountStatementService } from './accountstatement.service';

describe('AccountstatementService', () => {
  let service: AccountStatementService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AccountStatementService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
