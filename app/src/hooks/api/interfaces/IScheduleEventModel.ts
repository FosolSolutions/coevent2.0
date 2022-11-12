import { ISortableColumnsModel } from '.';

export interface IScheduleEventModel extends ISortableColumnsModel<number> {
  scheduleId: number;
  startOn: Date | string;
  endOn: Date | string;
}
