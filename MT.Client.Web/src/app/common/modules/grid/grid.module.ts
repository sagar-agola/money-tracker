import { NgModule } from '@angular/core';
import { GridComponent } from './grid/grid.component';
import { SharedModule } from '../shared.module';

@NgModule({
  declarations: [
    GridComponent
  ],
  imports: [
    SharedModule
  ],
  exports: [
    GridComponent
  ]
})
export class GridModule { }
