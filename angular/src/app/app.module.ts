import { MatButtonModule } from '@angular/material/button';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DetailsComponent, PdfViewerComponent } from './details/details.component';
import { BookUploadComponent} from './bookupload/bookupload.component';
import { ReaderComponent } from './reader/reader.component';
import { HttpClientModule } from '@angular/common/http';
import { PdfViewerModule} from 'ng2-pdf-viewer';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatCheckboxModule } from '@angular/material/checkbox';
import {MatSelectModule } from '@angular/material/select';
import { NgxMatFileInputModule } from '@angular-material-components/file-input';
import { BooktagsComponent } from './booktags/booktags.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatCardModule } from '@angular/material/card';
import {MatRippleModule} from '@angular/material/core';
import {MatTooltipModule} from '@angular/material/tooltip';
import {MatIconModule} from '@angular/material/icon';
import {MatPaginatorModule} from '@angular/material/paginator';
import { MatDialogModule } from '@angular/material/dialog';
import {MatDividerModule} from '@angular/material/divider';
import {MatListModule} from '@angular/material/list';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatMenuModule} from '@angular/material/menu';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
@NgModule({
  declarations: [
    AppComponent,
    DetailsComponent,
    BookUploadComponent,
    ReaderComponent,
    BooktagsComponent,
    DetailsComponent,
    PdfViewerComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    PdfViewerModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatCheckboxModule,
    MatSelectModule,
    NgxMatFileInputModule,
    MatSnackBarModule,
    MatCardModule,
    MatRippleModule,
    MatTooltipModule,
    MatIconModule,
    MatPaginatorModule,
    MatDialogModule,
    MatDividerModule,
    MatListModule,
    MatToolbarModule,
    MatMenuModule,
    MatProgressSpinnerModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
