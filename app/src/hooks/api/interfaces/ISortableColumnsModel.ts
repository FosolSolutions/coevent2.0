import { ICommonColumnsModel } from '.';

export interface ISortableColumnsModel<T> extends ICommonColumnsModel<T> {
  sortOrder: number;
}
