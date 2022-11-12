import { Claim, Role } from 'hooks';

import { IPadlockState } from '.';

export interface IPadlockHook extends IPadlockState {
  hasClaim: (claims: Claim | Array<Claim>) => boolean;
  hasRole: (roles: Role | Array<Role>) => boolean;
}
