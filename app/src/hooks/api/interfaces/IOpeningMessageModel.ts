import { IActivityOpeningModel, IAuditColumnsModel, IUserModel } from '.';

export interface IOpeningMessageModel extends IAuditColumnsModel {
  id: number;
  openingId: number;
  opening?: IActivityOpeningModel;
  ownerId: number;
  owner?: IUserModel;
  message: string;
}
