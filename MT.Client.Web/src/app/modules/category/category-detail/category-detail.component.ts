import { Component, OnInit } from '@angular/core';
import { TransactionTypeEnum } from '../../transaction/models/transaction-type.enum';
import { Category } from '../models/category.model';

@Component({
  selector: 'app-category-detail',
  templateUrl: './category-detail.component.html',
  styleUrls: ['./category-detail.component.scss']
})
export class CategoryDetailComponent implements OnInit {

  typeEnum = TransactionTypeEnum;
  category: Category = {};
  iconClass: string = "fa fa-th-large";

  constructor() { }

  ngOnInit(): void {
  }

  onIconPickerSelect(icon:  string): void {
    this.iconClass = icon;
  }

}
