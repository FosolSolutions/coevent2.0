import { IScheduleModel, IUserModel } from '..';

export interface IInvitationModel {
  url: string;
  schedule: IScheduleModel;
  from: IUserModel;
  to: IUserModel;
}
