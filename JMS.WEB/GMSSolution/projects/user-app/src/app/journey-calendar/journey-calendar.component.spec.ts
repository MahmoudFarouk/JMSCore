import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { JourneyCalendarComponent } from './journey-calendar.component';

describe('JourneyCalendarComponent', () => {
  let component: JourneyCalendarComponent;
  let fixture: ComponentFixture<JourneyCalendarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ JourneyCalendarComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(JourneyCalendarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
