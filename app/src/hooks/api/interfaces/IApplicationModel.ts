import { IActivityOpeningModel, IAuditColumnsModel, IUserModel } from '.';

export interface IApplicationModel extends IAuditColumnsModel {
  id: number;
  userId: number;
  user?: IUserModel;
  openingId: number;
  opening?: IActivityOpeningModel;
  message: string;
}
