import { UserTypes } from 'hooks/api';

export interface IUser {
  id: number;
  username: string;
  email: string;
  key?: string;
  displayName: string;
  firstName: string;
  middleName: string;
  lastName: string;
  userType: UserTypes | '';
  isEnabled: boolean;
  emailVerified: boolean;
  verifiedOn?: Date | string;
  failedLogins: number | '';
  roles: any[]; // TODO: Create IRole
  claims: any[]; // TODO: Create IClaim
  version?: string;
  createdOn?: string | Date;
  createdBy?: string;
  updatedOn?: string | Date;
  updatedBy?: string;
}
