import { IApplicationModel, IEventActivityModel, ISortableColumnsModel } from '.';

export interface IActivityOpeningModel extends ISortableColumnsModel<number> {
  activityId: number;
  activity?: IEventActivityModel;
  limit: number;
  question: string;
  responseRequired: boolean;
  applications: IApplicationModel[];
}
