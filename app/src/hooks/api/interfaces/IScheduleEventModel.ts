import { IEventActivityModel, IScheduleModel, ISortableColumnsModel } from '.';

export interface IScheduleEventModel extends ISortableColumnsModel<number> {
  scheduleId: number;
  schedule?: IScheduleModel;
  startOn: Date | string;
  endOn: Date | string;
  activities: IEventActivityModel[];
}
