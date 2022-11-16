import { ITokenClaim, Token } from 'hooks';

import { IPadlockState } from '.';

export interface IPadlockHook extends IPadlockState {
  hasClaim: (claims: ITokenClaim | Array<ITokenClaim>) => boolean;
  hasRole: (roles: string | Array<string>) => boolean;
  decode: () => Token | undefined;
}
