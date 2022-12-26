import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RequestsLibrarianIssuedComponent } from './requests-librarian-issued.component';

describe('RequestsLibrarianIssuedComponent', () => {
  let component: RequestsLibrarianIssuedComponent;
  let fixture: ComponentFixture<RequestsLibrarianIssuedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RequestsLibrarianIssuedComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RequestsLibrarianIssuedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
