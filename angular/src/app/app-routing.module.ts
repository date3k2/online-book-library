import { BookUploadComponent } from './bookupload/bookupload.component';
import { ReaderComponent } from './reader/reader.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DetailsComponent } from './details/details.component';

const routes: Routes = [
  {
    path: 'category/:cat',
    title: 'Reading',
    component: ReaderComponent,
  },
  {
    path: 'book/:bookId',
    component: DetailsComponent
  },
  {
    path: 'upload-file',
    title: 'Contributing',
    component: BookUploadComponent,
  },
  {
    path: '',
    redirectTo: 'category/all-categories',
    pathMatch: 'full',
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
