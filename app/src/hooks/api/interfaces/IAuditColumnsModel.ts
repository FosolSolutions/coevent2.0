export interface IAuditColumnsModel {
  rowVersion: string;
  createdOn: string | Date;
  createdBy: string;
  updatedOn: string | Date;
  updatedBy: string;
}
