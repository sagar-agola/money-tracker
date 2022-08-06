import { TableButton } from "./table-button.model";
import { TableColumn } from "./table-column.model";

export interface TableDefinition<T> {
  columns: TableColumn[];
  buttons: TableButton<T>[];
  emptyTableText: string;
  data: T[]
};