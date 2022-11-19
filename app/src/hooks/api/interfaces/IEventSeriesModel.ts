import { IAccountModel, IScheduleEventModel, ISortableColumnsModel } from '.';

export interface IEventSeriesModel extends ISortableColumnsModel<number> {
  accountId: number;
  account?: IAccountModel;
  events: IScheduleEventModel[];
}
