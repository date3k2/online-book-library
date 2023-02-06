import { ActivatedRoute, Router } from '@angular/router';
import { BookInfo } from './../models/bookInfo';
import { BooksService } from './../books.service';
import { Component, NgZone, OnInit } from '@angular/core';
import { bufferTime, concatMap, delay, filter, interval, isEmpty, map, Observable, of, scan, switchMap, take, timer } from 'rxjs';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { animate, style, transition, trigger } from '@angular/animations';
import { PageEvent } from '@angular/material/paginator';
import { Category } from '../models/categories';
@Component({
  selector: 'app-book-reader',
  templateUrl: './reader.component.html',
  styleUrls: ['./reader.component.css'],
  animations: [
    trigger('fade', [
      transition('void => *', [
        style({ opacity: 0 }),
        animate(500, style({ opacity: 1 }))
      ])
    ])
  ]
})
export class ReaderComponent implements OnInit {
  booksInfo: BookInfo[] = [];
  categories: Category[] = [
    {
      categoryId: '',
      categoryName: 'All Categories'
    }
  ];
  category!: Category;
  numberOfBooks: number = 0;
  isEmpty:boolean=false;
  constructor(private bookService: BooksService, private router: Router, private route: ActivatedRoute) {
  }
  async ngOnInit() {
    this.bookService.getCategories().subscribe(x => {
      this.categories.push(...x);
      this.loadBookTags();
    });
  }
  loadBookTags(event?: PageEvent,keyword?:string) {
    this.booksInfo = [];
    // console.log(event,keyword);
    
    const categoryName = (this.route.snapshot.params['cat'] as string).split('-').join(" ");

    const categoryId = this.categories.find(x => x.categoryName.toLowerCase() == categoryName)?.categoryId;
    this.bookService.getBooksInfo(keyword || '',event?.pageIndex || 0, categoryId || '')
      .subscribe(data => {
        data.forEach((val, index) => setTimeout(() => {
          this.booksInfo.push(val);
        }, 100 * index));
        if (data.length==0) this.isEmpty=true;
        else this.isEmpty=false;
      });
    this.bookService.getNumberOfBooksByCatOrKeyword(keyword|| '',categoryId || '').subscribe(num => this.numberOfBooks = num);
  }
  getRandomImage() {
    return ('https://picsum.photos/200/200/?random=' + Math.floor(Math.random() * 1000));
  }
  checkInput(event:KeyboardEvent,val:string) {
      if (event.key=='Enter' && val) this.loadBookTags(undefined,val);
      if (event.key!='Enter' && val.length==0) this.loadBookTags(undefined,val);
  }
  simplifyBookCategory(category: string) {
    return category.toLowerCase().split(/\s+/g).join("-");
  }
  canLoad(event: boolean, categoryName: string) {
    if (event) {
      this.router.navigate(['/category', this.simplifyBookCategory(categoryName)]).then(
        () =>
          this.loadBookTags()
      );
    }
  }
}
