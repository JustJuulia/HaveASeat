import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminDatesComponent } from './admin-dates.component';

describe('AdminDatesComponent', () => {
  let component: AdminDatesComponent;
  let fixture: ComponentFixture<AdminDatesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminDatesComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AdminDatesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
