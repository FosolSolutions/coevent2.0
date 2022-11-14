import { IAccountModel, IScheduleEventModel, ISortableColumnsModel } from '.';

export interface IScheduleModel extends ISortableColumnsModel<number> {
  accountId: number;
  account?: IAccountModel;
  startOn: Date | string;
  endOn: Date | string;
  events: IScheduleEventModel[];
}
