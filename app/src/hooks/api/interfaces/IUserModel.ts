import { Gender, UserStatus, UserType } from '..';
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
  emailVerifiedOn?: string | Date;
  displayName: string;
  firstName: string;
  middleName: string;
  lastName: string;
  gender?: Gender;
  userType: UserType;
  status: UserStatus;
  isEnabled: boolean;
  failedLogins: number;
  accounts: IAccountModel[];
  roles: IRoleModel[];
  claims: IUserClaimModel[];
}
