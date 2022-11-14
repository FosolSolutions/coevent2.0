import { IUserModel, UserTypes } from 'hooks/api';

import { IUser } from '.';

export const defaultUser: IUser = {
  id: 0,
  username: '',
  email: '',
  key: '',
  displayName: '',
  firstName: '',
  middleName: '',
  lastName: '',
  userType: UserTypes.User,
  isEnabled: false,
  emailVerified: false,
  verifiedOn: '',
  failedLogins: 0,
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
 * @param values IUserModel object to cast.
 * @returns A new instance of an IUser.
 */
export const toForm = (model: IUserModel): IUser => {
  const { username, email, key, verifiedOn, ...rest } = model;
  return {
    username: username ?? '',
    email: email ?? '',
    key: key ?? '',
    verifiedOn: verifiedOn ?? '',
    ...rest,
  };
};

/**
 * Casts an IUser object into an IUserModel object.
 * @param values IUser object to cast.
 * @returns A new instance of an IUserModel.
 */
export const toModel = (values: IUser): IUserModel => {
  const { userType, failedLogins, key, verifiedOn, ...rest } = values;
  return {
    userType: userType as UserTypes,
    failedLogins: parseInt(`${failedLogins}`),
    key: key !== '' ? key : undefined,
    verifiedOn: key !== '' ? verifiedOn : undefined,
    ...rest,
  };
};
