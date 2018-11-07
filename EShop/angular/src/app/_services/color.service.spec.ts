import { TestBed } from '@angular/core/testing';

import { ColorService } from './color.service';
import {beforeEach, describe, expect, it} from "@angular/core/testing/src/testing_internal";

describe('ColorService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ColorService = TestBed.get(ColorService);
    expect(service).toBeTruthy();
  });
});
