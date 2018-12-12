import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BrandManagmentComponent } from './brand-managment.component';

describe('BrandManagmentComponent', () => {
  let component: BrandManagmentComponent;
  let fixture: ComponentFixture<BrandManagmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BrandManagmentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BrandManagmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
