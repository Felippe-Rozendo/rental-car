/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { CarroService } from './carro.service';

describe('Service: Carro', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CarroService]
    });
  });

  it('should ...', inject([CarroService], (service: CarroService) => {
    expect(service).toBeTruthy();
  }));
});
