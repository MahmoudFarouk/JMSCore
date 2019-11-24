import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { JourneyDriverComponent } from './journey-driver.component';

describe('JourneyDriverComponent', () => {
  let component: JourneyDriverComponent;
  let fixture: ComponentFixture<JourneyDriverComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ JourneyDriverComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(JourneyDriverComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
