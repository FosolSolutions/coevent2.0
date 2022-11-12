import { IAuditColumnsModel } from '.';

export interface ICommonColumnsModel<T> extends IAuditColumnsModel {
  id: T;
  name: string;
  description: string;
  isEnabled: boolean;
}
