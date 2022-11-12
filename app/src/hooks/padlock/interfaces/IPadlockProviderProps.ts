import React from 'react';

import { IOIDCEndpoints, IPadlockProps, IToken, IUserInfo } from '.';

export interface IPadlockProviderProps extends IPadlockProps, React.HTMLAttributes<HTMLElement> {
  /**
   * Open ID Connect endpoint configuration.
   */
  oidc?: IOIDCEndpoints;
  /**
   * Token state which will contain access token and refresh token.
   */
  token?: IToken | null;
  /**
   * Whether authentication is initialized and ready.
   */
  authReady?: boolean; // TODO: Should probably be removed as it is set by the context, not the initial tag.
  /**
   * Whether the current user is authenticated.
   */
  authenticated?: boolean; // TODO: Should probably be removed as it is set by the context, not the initial tag.
  /**
   * User information.
   */
  userInfo?: IUserInfo;
}
