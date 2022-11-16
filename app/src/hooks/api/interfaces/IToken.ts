import { UserType } from '../constants';

export interface IToken {
  nameid: string;
  uid: string;
  accessType: UserType;
  unique_name: string;
  email: string;
  given_name: string;
  family_name: string;
  role?: string | string[];
  attribute?: string | string[];
  nbf: number;
  exp: number;
  iat: number;
  iss: string;
  aud: string;
}
