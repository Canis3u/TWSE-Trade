import { Component } from '@angular/core';
import { Router } from '@angular/router';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'TWSE-Trade-Web';
  constructor(
    private router: Router,
  ){}
  RouteToHome(){
    if(this.router.url=='/')
      window.location.reload();
    else
      this.router.navigate(['/']);
  }
}
