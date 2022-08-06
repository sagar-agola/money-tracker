import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { TableDefinition } from 'src/app/common/modules/grid/models/table-definition.model';
import { CategoryService } from '../category.service';
import { Category } from '../models/category.model';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.scss']
})
export class CategoryListComponent implements OnInit {

  tableDefinition: TableDefinition<Category> = {
    columns: [
      { displayName: "Id", propertyName: "hashId", width: 50 },
      { displayName: "Title", propertyName: "title", width: 50 }
    ],
    emptyTableText: "no records found",
    data: []
  };

  constructor(
    private spinner: NgxSpinnerService,
    private _categoryService: CategoryService
  ) { }

  ngOnInit(): void {
    this.getAllCategories();
  }

  getAllCategories(): void {
    this.spinner.show();
    this._categoryService.GetAll().subscribe(response => {
      this.spinner.hide();

      if (response && response.length > 0) {
        this.tableDefinition.data = response;
      }
    });
  }

}
