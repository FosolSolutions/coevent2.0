import { IAuditColumnsModel, UserTypes } from '..';

/**
 * IUserModel interface, represents a CoEvent user object.
 */
export interface IUserModel extends IAuditColumnsModel {
  id: number;
  username: string;
  email: string;
  key?: string;
  displayName: string;
  firstName: string;
  middleName: string;
  lastName: string;
  userType: UserTypes;
  isEnabled: boolean;
  emailVerified: boolean;
  verifiedOn?: Date | string;
  failedLogins: number;
  roles: any[]; // TODO: Create IRole
  claims: any[]; // TODO: Create IClaim
}
