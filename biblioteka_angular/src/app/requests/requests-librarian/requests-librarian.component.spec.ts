import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RequestsLibrarianComponent } from './requests-librarian.component';

describe('RequestsLibrarianComponent', () => {
  let component: RequestsLibrarianComponent;
  let fixture: ComponentFixture<RequestsLibrarianComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RequestsLibrarianComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RequestsLibrarianComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
