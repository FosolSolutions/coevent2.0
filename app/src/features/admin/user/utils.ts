import { IUserModel, UserStatus, UserType } from 'hooks/api';

import { IUser } from '.';

export const defaultUser: IUser = {
  id: 0,
  key: '',
  username: '',
  email: '',
  emailVerified: false,
  displayName: '',
  firstName: '',
  middleName: '',
  lastName: '',
  userType: UserType.User,
  status: UserStatus.Preapproved,
  isEnabled: false,
  failedLogins: 0,
  accounts: [],
  roles: [],
  claims: [],
  version: '',
  createdOn: new Date(),
  createdBy: '',
  updatedOn: new Date(),
  updatedBy: '',
};

/**
 * Casts an IUserModel object into an IUser object.
 * @param model IUserModel object to cast.
 * @returns A new instance of an IUser.
 */
export const toForm = (model: IUserModel): IUser => {
  return {
    ...model,
  };
};

/**
 * Casts an IUser object into an IUserModel object.
 * @param values IUser object to cast.
 * @returns A new instance of an IUserModel.
 */
export const toModel = (values: IUser): IUserModel => {
  const { failedLogins, key, userType, status, ...rest } = values;
  return {
    userType: userType as UserType,
    status: status as UserStatus,
    failedLogins: parseInt(`${failedLogins}`),
    key: key !== '' ? key : undefined,
    ...rest,
  };
};
