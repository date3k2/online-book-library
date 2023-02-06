import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookUploadComponent } from './bookupload.component';

describe('BookUploadComponent', () => {
  let component: BookUploadComponent;
  let fixture: ComponentFixture<BookUploadComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BookUploadComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BookUploadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
