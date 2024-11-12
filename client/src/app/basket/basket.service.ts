import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environment/environment';
import { Basket, BasketItem, BasketTotal } from '../shared/models/basket';
import { HttpClient } from '@angular/common/http';
import { Product } from '../shared/models/Product';

@Injectable({
  providedIn: 'root'
})
export class BasketService {
  baseUrl= environment.apiUrl;
  private basketsource=new BehaviorSubject<Basket | null>(null);
  basketsource$=this.basketsource.asObservable();
  private basketTotalSource=new BehaviorSubject<BasketTotal | null>(null);
  basketTotalSource$=this.basketTotalSource.asObservable();
  constructor(private http: HttpClient) { }
  getBasket(id:string){
    return this.http.get<Basket>(this.baseUrl+'basket?id='+id).subscribe({
      next:basket=>{
        this.basketsource.next(basket);
        this.calculateTotals();
      }
    });
  }
  setBasket(basket: Basket){
    return this.http.post<Basket>(this.baseUrl+'basket',basket).subscribe({
      next:basket=>{
        this.basketsource.next(basket);
        this.calculateTotals();
      }
    })
  }
  getCurrentBasketValue(){
   return this.basketsource.value;
  }
  addItemtoBasket(item : Product | BasketItem,quantity= 1){
    if(this.isProduct(item)) item=this.MapProductItemtoBasketItem(item);
    const basket=this.getCurrentBasketValue() ?? this.CreateBasket();
    basket.items=this.addOrUpdateItems(basket.items,item,quantity);
    this.setBasket(basket);
  }
  RemoveItemfromBasket(id: number,quantity =1){
    const basket=this.getCurrentBasketValue();
    if(!basket) return;
    const item=basket.items.find(x=>x.id===id);
    if(item){
      item.quantity-=quantity;
      if(item.quantity===0){
        basket.items=basket.items.filter(x=>x.id!==id);
      }
      if(basket.items.length>0) this.setBasket(basket);
      else this.deleteBasket(basket);
    }
  }
  deleteBasket(basket: Basket) {
    return this.http.delete(this.baseUrl+'basket?id='+ basket.id).subscribe({
      next: ()=>{
        this.basketTotalSource.next(null);
        this.basketTotalSource.next(null);
        localStorage.removeItem('basket_id');
      }
    })
  }
  addOrUpdateItems(items: BasketItem[], itemtoadd: BasketItem, quantity: number): BasketItem[] {
     const item=items.find(x=>x.id==itemtoadd.id);
     if(item) item.quantity+=quantity;
     else{
      itemtoadd.quantity=quantity;
      items.push(itemtoadd);
     }
     return items;
  }

  CreateBasket(): Basket  {
    const basket=new Basket();
    localStorage.setItem("basket_id",basket.id);
    return basket;
  }
  private MapProductItemtoBasketItem(item: Product) : BasketItem{
    return {
      id: item.id,
      productName: item.name,
      price:item.price,
      quantity:0,
      productUrl:item.pictureUrl,
      brand:item.productBrand,
      type:item.productType
    }
  }
  private calculateTotals(){
    const basket=this.getCurrentBasketValue();
    if(!basket) return;
    const shipping=0;
    const subtotal=basket.items.reduce((x,y)=>(y.price * y.quantity)+x,0);
    const total=subtotal+shipping;
    this.basketTotalSource.next({shipping,total,subtotal});
  }
  private isProduct(item : Product | BasketItem) : item is Product {
    return (item as Product).productBrand!==undefined;
  }
}
