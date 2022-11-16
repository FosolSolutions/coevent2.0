import { IAccountModel, IAuditColumnsModel } from '.';

export interface IUserClaimModel extends IAuditColumnsModel {
  userId: number;
  accountId: number;
  account?: IAccountModel;
  name: string;
  value: string;
}
