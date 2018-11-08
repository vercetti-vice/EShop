import { TestBed } from '@angular/core/testing';

import { BrandService } from './brand.service';
import {beforeEach, describe, expect, it} from "@angular/core/testing/src/testing_internal";

describe('BrandService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: BrandService = TestBed.get(BrandService);
    expect(service).toBeTruthy();
  });
});
