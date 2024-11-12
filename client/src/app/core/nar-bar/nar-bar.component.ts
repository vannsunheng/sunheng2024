import { Component } from '@angular/core';
import { BasketService } from 'src/app/basket/basket.service';
import { BasketItem } from 'src/app/shared/models/basket';


@Component({
  selector: 'app-nar-bar',
  templateUrl: './nar-bar.component.html',
  styleUrls: ['./nar-bar.component.scss']
})
export class NarBarComponent {
  constructor(public basketService: BasketService){

  }
  getcount(items: BasketItem[]){
    return items.reduce((sum,item) => sum+item.quantity,0);
  }
}
