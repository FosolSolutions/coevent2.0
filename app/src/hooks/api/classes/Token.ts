import { UserType } from '../constants';
import { IToken, ITokenClaim } from '../interfaces';

export class Token {
  nbf: number;
  exp: number;
  iat: number;
  iss: string;
  aud: string;
  nameid: string;
  uid: number;
  accessType: UserType;
  unique_name: string;
  email: string;
  given_name: string;
  family_name: string;
  roles: string[];
  claims: ITokenClaim[];

  constructor(token: IToken) {
    this.nbf = token.nbf;
    this.exp = token.exp;
    this.iat = token.iat;
    this.iss = token.iss;
    this.aud = token.aud;
    this.nameid = token.nameid;
    this.uid = parseInt(token.uid);
    this.accessType = token.accessType;
    this.unique_name = token.unique_name;
    this.email = token.email;
    this.given_name = token.given_name;
    this.family_name = token.family_name;
    this.roles = token.role ? (Array.isArray(token.role) ? token.role : [token.role]) : [];
    this.claims = token.attribute
      ? Array.isArray(token.attribute)
        ? token.attribute.map((a) => ({
            name: 'attribute',
            value: a,
          }))
        : [{ name: 'attribute', value: token.attribute }]
      : [];
  }
}
