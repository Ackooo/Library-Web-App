import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InsertBookComponent } from './insert-book.component';

describe('InsertBookComponent', () => {
  let component: InsertBookComponent;
  let fixture: ComponentFixture<InsertBookComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InsertBookComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InsertBookComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
