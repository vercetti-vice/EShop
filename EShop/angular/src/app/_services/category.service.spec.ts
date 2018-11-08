import { TestBed } from '@angular/core/testing';

import { CategoryService } from './category.service';
import {beforeEach, describe, expect, it} from "@angular/core/testing/src/testing_internal";

describe('CategoryService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CategoryService = TestBed.get(CategoryService);
    expect(service).toBeTruthy();
  });
});
