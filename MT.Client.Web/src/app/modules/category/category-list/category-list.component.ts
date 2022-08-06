import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { CategoryService } from '../category.service';
import { Category } from '../models/category.model';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.scss']
})
export class CategoryListComponent implements OnInit {

  categories: Category[] = [];

  constructor(
    private spinner: NgxSpinnerService,
    private _categoryService: CategoryService
  ) { }

  ngOnInit(): void {
    this.getAllCategories();
  }

  getAllCategories(): void {
    this.categories = [];
    
    this.spinner.show();
    this._categoryService.GetAll().subscribe(response => {
      this.spinner.hide();

      if (response && response.length > 0) {
        this.categories = response;
      }
    });
  }

}
