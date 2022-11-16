import { RoleClaim, UserClaim } from 'hooks';

import { IPadlockState } from '.';

export interface IPadlockHook extends IPadlockState {
  hasClaim: (claims: UserClaim | Array<UserClaim>) => boolean;
  hasRole: (roles: RoleClaim | Array<RoleClaim>) => boolean;
  decode: <T = any>() => T | undefined;
}
