import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditorMapComponent } from './editor-map.component';

describe('EditorMapComponent', () => {
  let component: EditorMapComponent;
  let fixture: ComponentFixture<EditorMapComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditorMapComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditorMapComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
