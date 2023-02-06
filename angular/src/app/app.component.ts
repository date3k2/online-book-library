import { Router } from '@angular/router';
import { Component, OnChanges, OnInit, ViewChild } from '@angular/core';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent{
  title = "test";
  constructor(private router:Router) {}
  helo() {
    this.router.navigate(['']).then(()=> window.location.reload());
  }
}