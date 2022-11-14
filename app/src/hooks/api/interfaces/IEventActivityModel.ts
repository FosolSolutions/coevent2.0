import { IActivityOpeningModel, IScheduleEventModel, ISortableColumnsModel } from '.';

export interface IEventActivityModel extends ISortableColumnsModel<number> {
  eventId: number;
  event?: IScheduleEventModel;
  startOn: Date | string;
  endOn: Date | string;
  openings: IActivityOpeningModel[];
}
