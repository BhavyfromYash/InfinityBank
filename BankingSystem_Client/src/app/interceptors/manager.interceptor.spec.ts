import { TestBed } from '@angular/core/testing';

import { ManagerInterceptor } from './manager.interceptor';

describe('ManagerInterceptor', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      ManagerInterceptor
      ]
  }));

  it('should be created', () => {
    const interceptor: ManagerInterceptor = TestBed.inject(ManagerInterceptor);
    expect(interceptor).toBeTruthy();
  });
});
