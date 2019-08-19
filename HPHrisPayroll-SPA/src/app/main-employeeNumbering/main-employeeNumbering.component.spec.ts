/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { MainEmployeeNumberingComponent } from './main-employeeNumbering.component';

describe('MainEmployeeNumberingComponent', () => {
  let component: MainEmployeeNumberingComponent;
  let fixture: ComponentFixture<MainEmployeeNumberingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MainEmployeeNumberingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MainEmployeeNumberingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
