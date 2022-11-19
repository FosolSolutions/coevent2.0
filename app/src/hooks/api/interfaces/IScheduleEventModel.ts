import { IEventActivityModel, IEventSeriesModel, IScheduleModel, ISortableColumnsModel } from '.';

export interface IScheduleEventModel extends ISortableColumnsModel<number> {
  scheduleId: number;
  schedule?: IScheduleModel;
  seriesId?: number;
  series?: IEventSeriesModel;
  startOn: Date | string;
  endOn: Date | string;
  activities: IEventActivityModel[];
}
