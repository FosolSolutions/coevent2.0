import { AccountType, IAccountModel } from 'hooks/api';

import { IAccount } from '.';

export const defaultAccount: IAccount = {
  id: 0,
  name: '',
  description: '',
  accountType: AccountType.Free,
  isEnabled: false,
  ownerId: '',
  calendars: [],
  events: [],
  schedules: [],
  traits: [],
  criterias: [],
  surveys: [],
  users: [],
  roles: [],
  claims: [],
  userClaims: [],
  version: '',
  createdOn: new Date(),
  createdBy: '',
  updatedOn: new Date(),
  updatedBy: '',
};

/**
 * Casts an IAccountModel object into an IAccount object.
 * @param values IAccountModel object to cast.
 * @returns A new instance of an IAccount.
 */
export const toForm = (model: IAccountModel): IAccount => {
  const { name, description, ownerId, ...rest } = model;
  return {
    name: name ?? '',
    description: description ?? '',
    ownerId: ownerId ?? '',
    ...rest,
  };
};

/**
 * Casts an IAccount object into an IAccountModel object.
 * @param values IAccount object to cast.
 * @returns A new instance of an IAccountModel.
 */
export const toModel = (values: IAccount): IAccountModel => {
  const { accountType, ownerId, ...rest } = values;
  return {
    accountType: accountType as AccountType,
    ownerId: parseInt(`${ownerId}`),
    ...rest,
  };
};
