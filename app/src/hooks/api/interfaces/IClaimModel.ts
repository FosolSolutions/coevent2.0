import { IAccountModel, ISortableColumnsModel } from '.';

export interface IClaimModel extends ISortableColumnsModel<number> {
  accountId: number;
  account?: IAccountModel;
}
