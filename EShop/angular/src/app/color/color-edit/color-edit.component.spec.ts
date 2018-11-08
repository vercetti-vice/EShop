import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ColorEditComponent } from './color-edit.component';
import {beforeEach, describe, expect, it} from "@angular/core/testing/src/testing_internal";

describe('ColorEditComponent', () => {
  let component: ColorEditComponent;
  let fixture: ComponentFixture<ColorEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ColorEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ColorEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
