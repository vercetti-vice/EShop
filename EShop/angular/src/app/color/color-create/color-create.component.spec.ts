import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ColorCreateComponent } from './color-create.component';
import {beforeEach, describe, expect, it} from "@angular/core/testing/src/testing_internal";

describe('ColorCreateComponent', () => {
  let component: ColorCreateComponent;
  let fixture: ComponentFixture<ColorCreateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ColorCreateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ColorCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
