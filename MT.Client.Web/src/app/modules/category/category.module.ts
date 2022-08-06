import { NgModule } from '@angular/core';
import { SharedModule } from 'src/app/common/modules/shared.module';

import { CategoryRoutingModule } from './category-routing.module';
import { CategoryListComponent } from './category-list/category-list.component';
import { GridModule } from 'src/app/common/modules/grid/grid.module';

@NgModule({
  declarations: [
    CategoryListComponent
  ],
  imports: [
    SharedModule,
    GridModule,
    CategoryRoutingModule
  ]
})
export class CategoryModule { }
