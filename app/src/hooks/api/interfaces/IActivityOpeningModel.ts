import {
  IApplicationModel,
  IEventActivityModel,
  IOpeningRequirementModel,
  ISortableColumnsModel,
} from '.';

export interface IActivityOpeningModel extends ISortableColumnsModel<number> {
  activityId: number;
  activity?: IEventActivityModel;
  limit: number;
  question: string;
  responseRequired: boolean;
  requirements: IOpeningRequirementModel[];
  applications: IApplicationModel[];
}
