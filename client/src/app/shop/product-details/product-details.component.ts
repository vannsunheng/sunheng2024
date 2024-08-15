import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/shared/models/Product';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit{
  product?: Product;
  constructor(private shopService : ShopService,private activedRoute: ActivatedRoute){

  }
  ngOnInit(): void {
    this.LoadProduct();
  }
  LoadProduct(){
    const id=this.activedRoute.snapshot.paramMap.get('id');
    if (id) this.shopService.getProduct(+id).subscribe({
      next: product => this.product= product,
      error: error=> console.log(error)
    });
  }

}
