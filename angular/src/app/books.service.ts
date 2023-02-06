import { BookInfo } from './models/bookInfo';
import { HttpClient, HttpEventType } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Category } from './models/categories';

@Injectable({
  providedIn: 'root'
})
export class BooksService {
  private booksUrl = 'https://localhost:7165/api/Books';
  constructor(private http: HttpClient) { }

  getBooksInfo(keyword: string, pageNumber: number, categoryId: string) {
    return this.http.get<BookInfo[]>(this.booksUrl, { params: { keyword, pageNumber, categoryId } });
  }

  getCategories() {
    return this.http.get<Category[]>(this.booksUrl + '/Categories')
  }

  addBook(formData: FormData) {
    this.http.post(this.booksUrl, formData, {
      reportProgress: true,
      observe: 'events'
    }).subscribe(event => {
      if (event.type === HttpEventType.UploadProgress) {
        // This is an upload progress event. Compute and show the percentage complete:
        const percentDone = Math.round(100 * event.loaded / event.total!);
        console.log(`File is ${percentDone}% uploaded.`);
      } else if (event.type === HttpEventType.Response) {
        console.log(`File is completely uploaded!`);
      }
    });
  }

  getBookById(bookId: string) {
    return this.http.get<BookInfo>(this.booksUrl + '/BookInfoById', { params: { bookId } })
  }
  getNumberOfBooksByCatOrKeyword(keyword: string, categoryId: string) {
    return this.http.get<number>(this.booksUrl + '/TotalBooksByCategory', { params: { keyword, categoryId } })
  }

  getPage(bookId: string, pageNumber: number) {
    return this.http.get<Blob>(this.booksUrl + '/GetPage', { params: { bookId, pageNumber } })
  }
  toggleBookMark(bookId: string, pageNumber: number) {
    return this.http.put<boolean>(this.booksUrl + '/ToggleBookMark', {}, { params: { bookId, pageNumber } });
  }
  getMarkedPages(bookId: string) {
    return this.http.get<number[]>(this.booksUrl + '/BookMarks', { params: { bookId } });
  }
}
