import { Component, Inject, Input } from '@angular/core';
import { BasketService } from 'src/app/basket/basket.service';
import { Product } from 'src/app/shared/models/Product';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.scss']
})
export class ProductItemComponent {
    @Input() product?: Product;
    constructor(private basketservice: BasketService){}
    addItemToBasket(){
      this.product && this.basketservice.addItemtoBasket(this.product);
    }

}
