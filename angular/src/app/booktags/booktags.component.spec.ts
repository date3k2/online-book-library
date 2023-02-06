import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BooktagsComponent } from './booktags.component';

describe('BooktagsComponent', () => {
  let component: BooktagsComponent;
  let fixture: ComponentFixture<BooktagsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BooktagsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BooktagsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
