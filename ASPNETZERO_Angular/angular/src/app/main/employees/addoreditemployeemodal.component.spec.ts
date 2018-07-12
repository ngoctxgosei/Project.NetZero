import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddoreditemployeemodalComponent } from './addoreditemployeemodal.component';

describe('AddoreditemployeemodalComponent', () => {
  let component: AddoreditemployeemodalComponent;
  let fixture: ComponentFixture<AddoreditemployeemodalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddoreditemployeemodalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddoreditemployeemodalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
