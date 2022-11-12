import { IScheduleEventModel, ISortableColumnsModel } from '.';

export interface IScheduleModel extends ISortableColumnsModel<number> {
  accountId: number;
  startOn: Date | string;
  endOn: Date | string;
  events: IScheduleEventModel[];
}
