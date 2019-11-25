import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ForgetchangepasswordComponent } from './forgetchangepassword.component';

describe('ForgetchangepasswordComponent', () => {
  let component: ForgetchangepasswordComponent;
  let fixture: ComponentFixture<ForgetchangepasswordComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ForgetchangepasswordComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ForgetchangepasswordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
