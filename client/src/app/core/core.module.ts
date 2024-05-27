import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NarBarComponent } from './nar-bar/nar-bar.component';



@NgModule({
  declarations: [
    NarBarComponent
  ],
  imports: [
    CommonModule
  ],
  exports:[
    NarBarComponent
  ]
})
export class CoreModule { }
