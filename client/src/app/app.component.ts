import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Product } from './models/Product';
import { Pagination } from './models/Pagination';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  title = 'E-commerce';
  products: Product[]=[];
  constructor(private http: HttpClient) {

  }
  ngOnInit(): void {
    this.http.get<Pagination<Product[]>>("https://localhost:5001/api/products?Pagesize=50").subscribe({
      next: response => this.products=response.data,
      error: error => console.log(error),
      complete: () => {
        console.log("require complate");
        console.log("extra statement");
      }
    })
  }
}
