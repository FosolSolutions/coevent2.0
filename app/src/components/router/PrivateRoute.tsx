import axios from 'axios';
import { ITokenClaim, usePadlock } from 'hooks';
import React from 'react';
import { Navigate } from 'react-router-dom';

interface IPrivateRouteProps {
  /**
   * The path to redirect to if user is unauthorized.
   */
  loginPath?: string;
  /**
   * A role the user belongs to.
   */
  roles?: string | Array<string>;
  /**
   * A claim the user has.
   */
  claims?: ITokenClaim | Array<ITokenClaim>;
  /**
   * The element to load if authorized.
   */
  element?: React.ReactElement | null;
  /**
   * The children elements to load if authorized.
   */
  children?: React.ReactNode;
}

/**
 * PrivateRoute provides a way to only show menu items for authenticated users.
 * @param param0 Route element attributes.
 * @returns PrivateRoute component.
 */
export const PrivateRoute = ({
  loginPath = '/login',
  claims,
  roles,
  element,
  children,
}: IPrivateRouteProps) => {
  const padlock = usePadlock();

  if (!padlock.authReady) <></>;

  if (
    !padlock.authenticated ||
    (!!claims && !padlock.hasClaim(claims)) ||
    (!!roles && !padlock.hasRole(roles))
  ) {
    const query = {
      redirect_uri: window.location.pathname,
    };
    const path = axios.getUri({ url: loginPath, params: query });
    return <Navigate to={path} />;
  }

  return element || <>{children}</>;
};
