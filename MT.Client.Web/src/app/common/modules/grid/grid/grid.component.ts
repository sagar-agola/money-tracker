import { Component, Input, OnInit } from '@angular/core';
import { Category } from 'src/app/modules/category/models/category.model';
import { TableDefinition } from '../models/table-definition.model';

@Component({
  selector: 'app-grid',
  templateUrl: './grid.component.html',
  styleUrls: ['./grid.component.scss']
})
export class GridComponent<T> implements OnInit {

  @Input() tableDefinition?: TableDefinition<T>;

  constructor() { }

  ngOnInit(): void {
  }

}
