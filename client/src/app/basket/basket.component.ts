import { Component } from '@angular/core';
import { BasketService } from './basket.service';
import { BasketItem } from '../shared/models/basket';
import { SharedModule } from "../shared/shared.module";
import { OrderTotalsComponent } from "../shared/order-totals/order-totals.component";

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss']
})
export class BasketComponent {
  constructor(public basketservice: BasketService){
    
  }
  incrementQuantity(item: BasketItem){
    this.basketservice.addItemtoBasket(item);
  }
  removeitem(id: number,quantity: number){
    this.basketservice.RemoveItemfromBasket(id,quantity);
  }
}
