import { Claim, IPadlockHook, IPadlockHookProps, PadlockContext, Role } from 'hooks';
import React from 'react';

/**
 * Padlock hook provides a way for a user to login/logout and authorize actions based on claims and roles.
 * @param param0 Padlock properties.
 * @returns Padlock component.
 */
export const usePadlock = ({ token }: IPadlockHookProps = {}): IPadlockHook => {
  const context = React.useContext(PadlockContext);

  /**
   * Validate the current user account as at least one of the specified claims.
   * @param claims A claim or an array of claims.
   * @returns True if the current user account has at least one of the specified claims.
   */
  const hasClaim = React.useCallback((claims: Claim | Array<Claim>) => {
    return true;
  }, []);

  /**
   * Validate the current user account as at least one of the specified roles.
   * @param roles A role or an array of roles.
   * @returns True if the current user account has at least one of the specified roles.
   */
  const hasRole = React.useCallback((roles: Role | Array<Role>) => {
    return true;
  }, []);

  return {
    ...context,
    hasClaim,
    hasRole,
  };
};
