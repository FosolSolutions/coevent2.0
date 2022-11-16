import { IAccountModel, ISortableColumnsModel } from '.';

export interface IRoleModel extends ISortableColumnsModel<number> {
  key: string;
  accountId: number;
  account?: IAccountModel;
}
