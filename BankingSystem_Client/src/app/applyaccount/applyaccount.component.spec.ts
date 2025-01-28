import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApplyaccountComponent } from './applyaccount.component';

describe('ApplyaccountComponent', () => {
  let component: ApplyaccountComponent;
  let fixture: ComponentFixture<ApplyaccountComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ApplyaccountComponent]
    });
    fixture = TestBed.createComponent(ApplyaccountComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
