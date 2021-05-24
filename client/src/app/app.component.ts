import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Car Dealership Management';
  users: any;
  products :any;

  constructor(private http: HttpClient){}
  ngOnInit() {
    this.getUsers();
    this.getProducts();
  }
  
  getUsers() {
    this.http.get('https://localhost:5001/api/users').subscribe(response =>
    {
      this.users = response;
    }, error=>{
      console.log(error);
    })

  }
  getProducts() {
    this.http.get('https://localhost:5001/api/products').subscribe(response =>
    {
      this.products = response;
    }, error=>{
      console.log(error);
    })
}
}
