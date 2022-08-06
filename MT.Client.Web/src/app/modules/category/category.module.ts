import { NgModule } from '@angular/core';
import { SharedModule } from 'src/app/common/modules/shared.module';

import { CategoryRoutingModule } from './category-routing.module';
import { CategoryListComponent } from './category-list/category-list.component';

@NgModule({
  declarations: [
    CategoryListComponent
  ],
  imports: [
    SharedModule,
    CategoryRoutingModule
  ]
})
export class CategoryModule { }
