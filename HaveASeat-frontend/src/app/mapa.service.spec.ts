import { TestBed } from '@angular/core/testing';

import { MapaService } from './services/mapa.service';

describe('MapaService', () => {
  let service: MapaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MapaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
