import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BankserviceComponent } from './bankservice.component';

describe('BankserviceComponent', () => {
  let component: BankserviceComponent;
  let fixture: ComponentFixture<BankserviceComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BankserviceComponent]
    });
    fixture = TestBed.createComponent(BankserviceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
