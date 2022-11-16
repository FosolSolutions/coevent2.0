import { IAccountModel, IRoleModel, IUserClaimModel, UserStatus, UserType } from 'hooks/api';

export interface IUser {
  id: number;
  username: string;
  key?: string;
  email: string;
  emailVerified: boolean;
  displayName: string;
  firstName: string;
  middleName: string;
  lastName: string;
  userType: UserType | '';
  status: UserStatus | '';
  isEnabled: boolean;
  verifiedOn?: Date | string;
  failedLogins: number | '';
  accounts: IAccountModel[];
  roles: IRoleModel[];
  claims: IUserClaimModel[];
  version?: string;
  createdOn?: string | Date;
  createdBy?: string;
  updatedOn?: string | Date;
  updatedBy?: string;
}
