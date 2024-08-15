import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NarBarComponent } from './nar-bar/nar-bar.component';
import { RouterModule } from '@angular/router';
import { TestErrorsComponent } from './test-errors/test-errors.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { ServerErrorComponent } from './server-error/server-error.component';
import { ToastrModule } from 'ngx-toastr';



@NgModule({
  declarations: [
    NarBarComponent,
    TestErrorsComponent,
    NotFoundComponent,
    ServerErrorComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    ToastrModule.forRoot({
      positionClass:'toast-bottom-right',
      preventDuplicates:true
    })
  ],
  exports:[
    NarBarComponent
  ]
})
export class CoreModule { }
