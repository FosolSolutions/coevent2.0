import { IBaseModel, UserTypes } from '..';

/**
 * IUserModel interface, represents a CoEvent user object.
 */
export interface IUserModel extends IBaseModel {
  id: number;
  username: string;
  email: string;
  key?: string;
  displayName: string;
  firstName: string;
  middleName: string;
  lastName: string;
  userType: UserTypes;
  isDisabled: boolean;
  isVerified: boolean;
  verifiedOn?: Date | string;
  failedLogins: number;
  roles: any[]; // TODO: Create IRole
  claims: any[]; // TODO: Create IClaim
}
