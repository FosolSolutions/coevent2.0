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
  uniqueName: string;
  email: string;
  displayName: string;
  givenName: string;
  familyName: string;
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
    this.accessType = token.access_type;
    this.uniqueName = token.unique_name;
    this.displayName = token.display_name;
    this.email = token.email;
    this.givenName = token.given_name;
    this.familyName = token.family_name;
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
