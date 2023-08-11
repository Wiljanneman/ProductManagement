import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistrationDetailsComponentComponent } from './registration-details-component.component';

describe('RegistrationDetailsComponentComponent', () => {
  let component: RegistrationDetailsComponentComponent;
  let fixture: ComponentFixture<RegistrationDetailsComponentComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RegistrationDetailsComponentComponent]
    });
    fixture = TestBed.createComponent(RegistrationDetailsComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
