import { AccountTypes, IAuditColumnsModel } from '..';

/**
 * IAccountModel interface, represents a CoEvent account object.
 */
export interface IAccountModel extends IAuditColumnsModel {
  id: number;
  name: string;
  description: string;
  accountType: AccountTypes;
  isEnabled: boolean;
  ownerId: number;
  owner?: any; // TODO: Create IUserModel
  calendars: any[]; // TODO: Create ICalendarModel
  events: any[]; // TODO: Create IEventModel
  schedules: any[]; // TODO: Create IScheduleModel
  traits: any[]; // TODO: Create ITraitModel
  criterias: any[]; // TODO: Create ICriteriaModel
  surveys: any[]; // TODO: Create ISurveyModel
  users: any[]; // TODO: Create IUserModel
  roles: any[]; // TODO: Create IRoleModel
  claims: any[]; // TODO: Create IClaimModel
  userClaims: any[]; // TODO: Create IUserClaimModel
}
