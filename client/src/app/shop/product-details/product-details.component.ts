import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/shared/models/Product';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';
import { BasketService } from 'src/app/basket/basket.service';
import { take } from 'rxjs';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit{
  product?: Product;
  quantity= 1;
  quantityInbasket=0;

  constructor(private shopService : ShopService,private activedRoute: ActivatedRoute,
    private bcService: BreadcrumbService,private basketService: BasketService){
    this.bcService.set('@productDetails',' ');
  }
  ngOnInit(): void {
    this.LoadProduct();
  }
  LoadProduct(){
    const id=this.activedRoute.snapshot.paramMap.get('id');
    if (id) this.shopService.getProduct(+id).subscribe({
      next: product => {
        this.product= product;
        this.bcService.set('@productDetails',product.name);
        this.basketService.basketsource$.pipe(take(1)).subscribe({
          next: basket=>{
            const item = basket?.items.find(x=>x.id===+id);
            if(item){
              this.quantity=item.quantity;
              this.quantityInbasket=item.quantity;
            }
          }
        })
      },
      error: error=> console.log(error)
    });
  }
  incrementQuantity(){
    this.quantity++;
  }
  decrementQuantity(){
    this.quantity--;
  }
  UpdateBasket(){
    if(this.product){
      if(this.quantity> this.quantityInbasket){
        const itemToAdd=this.quantity-this.quantityInbasket;
        this.quantityInbasket+=itemToAdd;
        this.basketService.addItemtoBasket(this.product,itemToAdd);
      }else{
        const itemToremove=this.quantityInbasket-this.quantity;
        this.quantityInbasket-=itemToremove;
        this.basketService.RemoveItemfromBasket(this.product.id,itemToremove);
      }
    }
  }
  getTextBTN(){
    return this.quantityInbasket===0 ? 'Add to Basket': 'Update Basket';
  }

}
