import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MYDialogComponent } from './mydialog.component';

describe('MYDialogComponent', () => {
  let component: MYDialogComponent;
  let fixture: ComponentFixture<MYDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MYDialogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(MYDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
