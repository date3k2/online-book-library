import { BooksService } from './../books.service';
import { BookInfo } from './../models/bookInfo';
import { MatDialog, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ChangeDetectorRef } from '@angular/core';
@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {
  public bookInfo!: BookInfo;
  markedPages: number[] = [];
  isLoading: boolean = true;
  constructor(private cd: ChangeDetectorRef, private route: ActivatedRoute, private dialog: MatDialog, private booksService: BooksService) { }

  ngOnInit() {
    this.booksService.getBookById(this.route.snapshot.params['bookId']).subscribe(
      bookInfo =>
        this.bookInfo = bookInfo
    );

    this.booksService.getMarkedPages(this.route.snapshot.params['bookId']).subscribe(
      data => {
        this.markedPages = data;
        this.isLoading = false;
      }
    )
  }
  openDialog(pageNum: number) {
    this.dialog.open(PdfViewerComponent, {
      data: {
        bookId: this.route.snapshot.params['bookId'], numberOfPages: this.bookInfo.numberOfPages,
        markedPages: this.markedPages, currentPage: pageNum
      },
      disableClose: true,
      width: '650px',
      height: '98%',
    });
  }
  simplifyBookCategory(category: string) {
    return category.toLowerCase().split(/\s+/g).join("-");
  }
}
@Component({
  selector: 'pdf-view',
  templateUrl: './pdf-viewer.html',
  styleUrls: ['./details.component.css']
})
export class PdfViewerComponent implements OnInit {
  zoom = 1;
  page = this.data.currentPage;
  marked: boolean[] = [false];
  pdfSrc: string = '';
  markedPages: number[] = (this.data.markedPages as number[]);
  constructor(@Inject(MAT_DIALOG_DATA) public data: any, private bookservice: BooksService) { }
  ngOnInit(): void {
    this.checkMarked(this.page);
  }
  checkMarked(page: number) {
    this.marked[page] = (this.markedPages.indexOf(page) != -1);
  }
  zoomIn() {
    if (this.zoom < 3) this.zoom += 0.1;
  }
  zoomOut() {
    if (this.zoom > 0.5) this.zoom -= 0.1;
  }
  firstPage() {
    this.page = 1;
    this.checkMarked(this.page);
  }
  nextPage() {
    if (this.page < this.data.numberOfPages) ++this.page;
    this.checkMarked(this.page);
  }
  prevPage() {
    if (this.page > 1) --this.page;
    this.checkMarked(this.page);
  }
  lastPage() {
    this.page = this.data.numberOfPages;
    this.checkMarked(this.page);
  }
  getPage(pageNumer: number) {
    return 'https://localhost:7165/api/Books/GetPage?bookId=' + this.data.bookId + '&pageNumber=' + pageNumer;
  }
  togglemark() {
    this.marked[this.page] = !this.marked[this.page];
    if (!this.marked[this.page]) {
      const index = this.markedPages.indexOf(this.page);
      this.markedPages.splice(index, 1);
    }
    else this.markedPages.push(this.page);
    console.log(this.markedPages);
    this.bookservice.toggleBookMark(this.data.bookId, this.page).subscribe(
      marked => {
        console.log(marked);
      }
    );
  }
}