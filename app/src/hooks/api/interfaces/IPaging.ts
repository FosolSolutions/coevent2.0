export interface IPaging<TItem> {
  page: number;
  quantity: number;
  items: TItem[];
  total?: number;
}
