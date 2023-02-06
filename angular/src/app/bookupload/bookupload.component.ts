import { Category } from './../models/categories';
import { BooksService } from './../books.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSelect } from '@angular/material/select';
import { Router } from '@angular/router';
@Component({
  selector: 'app-book-upload',
  templateUrl: './bookupload.component.html',
  styleUrls: ['./bookupload.component.css']
})
export class BookUploadComponent implements OnInit {
  @ViewChild('categorySelect') categorySelect!: MatSelect;
  uploadForm!: FormGroup;
  categories: Category[] = [];
  otherCate: Category = {
    categoryId: '0',
    categoryName: 'Other'
  }
  constructor(private router: Router, private snackBar: MatSnackBar, private fb: FormBuilder, private bookService: BooksService) { }

  ngOnInit() {
    this.createForm();
  }

  createForm() {
    this.uploadForm = this.fb.group({
      title: [
        '',
        Validators.compose([
          Validators.required,
          Validators.pattern(/\S/)
        ]),
      ],
      author: [
        '',
        Validators.compose([
          Validators.required,
          Validators.pattern(/\S/)
        ]),
      ],
      category: ['', Validators.required],
      file: new FormControl(null, Validators.required)
    });
    this.bookService.getCategories().subscribe(x => this.categories = x.concat(this.otherCate));
  }

  onSubmit(): void {
    var form = this.uploadForm.value;
    var formData = new FormData();
    formData.append('Title', (form.title as string).trim());
    formData.append('Author', (form.author as string).trim());
    formData.append('File', form.file);
    formData.append('CategoryId', form.category);
    if (form.customCategory) formData.append('CategoryName', (form.customCategory as string).trim());
    this.snackBar.open('File sent!', 'Close', {
      verticalPosition: 'top',
      duration: 3000
    });
    this.bookService.addBook(formData);
    this.uploadForm.reset();
    const controls = this.uploadForm.getRawValue();
    Object.keys(controls).forEach(key => {
      this.uploadForm.get(key)!.markAsPending();
    });
  }
  onCategoryChange() {
    if (this.uploadForm.value.category === '0')
      this.uploadForm.addControl('customCategory', new FormControl('', Validators.compose([
        Validators.required,
        Validators.pattern(/\S/g),
      ])));
    else this.uploadForm.removeControl('customCategory');
  }
}
