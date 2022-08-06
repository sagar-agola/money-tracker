export interface TableButton<T> {
  iconClass?: string;
  text?: string;
  color?: "white" | "red"
  ngIfCallBack?: (item: T) => boolean;
  clickCallBack: (item: T) => void;
};