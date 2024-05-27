import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Product } from '../shared/models/Product';
import { ShopService } from './shop.service';
import { Type } from '../shared/models/type';
import { Brand } from '../shared/models/brand';
import { ShopParams } from '../shared/models/ShopParam';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  @ViewChild('search') searchTerm?: ElementRef; 
  products: Product[] = [];
  types: Type[] = [];
  brands: Brand[] = [];
  shopParams=new ShopParams();
  sortOption=[
   {name:'Alphabetical',value:'name'}, 
   {name:'Price: Low to high',value:'priceAsc'},
   {name:'Price: High to low',value:'priceDesc'} 
  ];
  totalCount=0;

  constructor(private Shopservie: ShopService) { }
  ngOnInit(): void {
    this.getProducts(),
    this.getBrands(),
    this.getTypes()
  }
  getProducts() {
    this.Shopservie.getProduct(this.shopParams).subscribe({
      next: response => {
        this.products = response.data;
        this.shopParams.pageNumber=response.pageIndex;
        this.shopParams.pageSize=response.pageSize;
        this.totalCount=response.count;
      },
      error: error => console.log(error)
    })
  }
  getBrands() {
    this.Shopservie.getBrands().subscribe({
      next: response => this.brands = [{ id: 0, name: 'All' }, ...response],
      error: error => console.log(error)
    })
  }
  getTypes() {
    this.Shopservie.getTypes().subscribe({
      next: response => this.types = [{ id: 0, name: 'All' }, ...response],
      error: error => console.log(error)
    })
  }
  OnBrandSelected(brandId:number){
    this.shopParams.brandId=brandId;
    this.shopParams.pageNumber=1;
    this.getProducts();
  }
  OnTypeSelected(typeId:number){
    this.shopParams.typeId=typeId;
    this.shopParams.pageNumber=1;
    this.getProducts();
  }
  onSortSelected(event:any){
    this.shopParams.sort=event.target.value;
    this.getProducts();
  }
  onPageChange(event: any){
    if(this.shopParams.pageNumber!==event){
      this.shopParams.pageNumber=event;;
      this.getProducts()
    }  
  }
  onSearch(){
    this.shopParams.search=this.searchTerm?.nativeElement.value;
    this.shopParams.pageNumber=1;
    this.getProducts()
  }
  onReset(){
    if(this.searchTerm) this.searchTerm.nativeElement.value='';
    this.shopParams=new ShopParams();
    this.getProducts();
    
  }
}
