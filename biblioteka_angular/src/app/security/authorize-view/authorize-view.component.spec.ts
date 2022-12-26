import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthorizeViewComponent } from './authorize-view.component';

describe('AuthorizeViewComponent', () => {
  let component: AuthorizeViewComponent;
  let fixture: ComponentFixture<AuthorizeViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AuthorizeViewComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AuthorizeViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
