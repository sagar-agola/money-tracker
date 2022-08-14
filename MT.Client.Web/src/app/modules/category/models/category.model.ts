import { TransactionTypeEnum } from "../../transaction/models/transaction-type.enum";

export interface Category {
  hashId?: number;
  type?: TransactionTypeEnum;
  title?: string;
};
