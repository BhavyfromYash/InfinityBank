import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SessionExpiredPopupComponent } from './sessionexpireddialog.component';

describe('SessionexpireddialogComponent', () => {
  let component: SessionExpiredPopupComponent;
  let fixture: ComponentFixture<SessionExpiredPopupComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SessionExpiredPopupComponent]
    });
    fixture = TestBed.createComponent(SessionExpiredPopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
