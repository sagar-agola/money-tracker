import { NgModule } from '@angular/core';
import { SharedModule } from 'src/app/common/modules/shared.module';
import { ButtonModule } from '@syncfusion/ej2-angular-buttons';
import { CategoryRoutingModule } from './category-routing.module';
import { GridModule } from 'src/app/common/modules/grid/grid.module';
import { IconPickerModule } from 'ngx-icon-picker';

import { CategoryListComponent } from './category-list/category-list.component';
import { CategoryDetailComponent } from './category-detail/category-detail.component';

@NgModule({
  declarations: [
    CategoryListComponent,
    CategoryDetailComponent
  ],
  imports: [
    SharedModule,
    IconPickerModule,
    ButtonModule,
    GridModule,
    CategoryRoutingModule
  ]
})
export class CategoryModule { }
