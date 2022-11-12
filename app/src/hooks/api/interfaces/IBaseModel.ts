export interface IBaseModel {
  rowVersion: string;
  createdOn: string | Date;
  createdBy: string;
  updatedOn: string | Date;
  updatedBy: string;
}
