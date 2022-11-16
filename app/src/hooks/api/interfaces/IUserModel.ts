import { UserStatus, UserType } from '..';
import { IAccountModel, IAuditColumnsModel, IRoleModel, IUserClaimModel } from '.';

/**
 * IUserModel interface, represents a CoEvent user object.
 */
export interface IUserModel extends IAuditColumnsModel {
  id: number;
  username: string;
  key?: string;
  email: string;
  emailVerified: boolean;
  displayName: string;
  firstName: string;
  middleName: string;
  lastName: string;
  userType: UserType;
  status: UserStatus;
  isEnabled: boolean;
  failedLogins: number;
  accounts: IAccountModel[];
  roles: IRoleModel[];
  claims: IUserClaimModel[];
}
