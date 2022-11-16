import { IPadlockHook, IPadlockHookProps, ITokenClaim, PadlockContext, Token } from 'hooks';
import jwtDecode from 'jwt-decode';
import React from 'react';

/**
 * Padlock hook provides a way for a user to login/logout and authorize actions based on claims and roles.
 * @param param0 Padlock properties.
 * @returns Padlock component.
 */
export const usePadlock = ({ token }: IPadlockHookProps = {}): IPadlockHook => {
  const context = React.useContext(PadlockContext);

  /**
   * Decode the access token.
   */
  const decode = React.useCallback(() => {
    return context.token ? new Token(jwtDecode(context.token.accessToken)) : undefined;
  }, [context.token]);

  /**
   * Validate the current user account as at least one of the specified claims.
   * @param claims A claim or an array of claims.
   * @returns True if the current user account has at least one of the specified claims.
   */
  const hasClaim = React.useCallback(
    (claims: ITokenClaim | Array<ITokenClaim>) => {
      return (
        context.identity?.claims.some((c) => {
          return Array.isArray(claims)
            ? claims.some((c1) => c1.name === c.name && c1.value === c.value)
            : c.name === claims.name && c.value === claims.value;
        }) ?? false
      );
    },
    [context.identity],
  );

  /**
   * Validate the current user account as at least one of the specified roles.
   * @param roles A role or an array of roles.
   * @returns True if the current user account has at least one of the specified roles.
   */
  const hasRole = React.useCallback(
    (roles: string | Array<string>) => {
      return (
        context.identity?.roles.some((r) =>
          Array.isArray(roles) ? roles.some((r1) => r1 === r) : r === roles,
        ) ?? false
      );
    },
    [context.identity],
  );

  return {
    ...context,
    hasClaim,
    hasRole,
    decode,
  };
};
