import { IActivityOpeningModel, IAuditColumnsModel } from '.';

export interface IOpeningRequirementModel extends IAuditColumnsModel {
  openingId: number;
  opening?: IActivityOpeningModel;
  name: string;
  value: string;
}
