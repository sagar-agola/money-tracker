import { TableColumn } from "./table-column.model";

export interface TableDefinition<T> {
  columns: TableColumn[];
  emptyTableText: string;
  data: T[]
};