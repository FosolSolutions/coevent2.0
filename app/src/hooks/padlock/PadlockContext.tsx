import { SummonProvider, tokenExpired } from 'hooks';
import moment from 'moment';
import React from 'react';
import { useCookies } from 'react-cookie';

import {
  IOIDCEndpoints,
  IPadlockProviderProps,
  IPadlockState,
  IToken,
  IUserInfo,
} from './interfaces';

const COOKIE_NAME = 'padlock';

/**
 * PadlockContext, provides a way to maintain a user's authenticated state across AJAX requests.
 */
export const PadlockContext = React.createContext<IPadlockState>({
  authReady: false,
  authenticated: false,
  state: {
    setAuthReady: () => {},
    setAuthenticated: () => {},
    setToken: () => {},
    setUserInfo: () => {},
  },
  login: () => {},
  logout: () => {},
});

/**
 * PadlockProvider, provides a way to initialize context which manages user authentication and authorization state.
 * Additionally, it is currently tightly coupled with 'summon' (a wrapper for axios).
 * Which enables AJAX requests to include JWT headers to authorize API endpoints.
 * @param param0 PadlockProvider initialization properties.
 * @returns PadlockProvider context component.
 */
export const PadlockProvider: React.FC<IPadlockProviderProps> = ({
  oidc: initOIDC = {},
  authReady: initAuthReady = false,
  authenticated: initAuthenticated = false,
  token: initToken,
  userInfo: initUserInfo,
  baseApiUrl = 'api/',
  autoRefreshToken,
  loginPath,
  children,
}) => {
  const [oidc] = React.useState<IOIDCEndpoints>(initOIDC);
  const [authReady, setAuthReady] = React.useState<boolean>(initAuthReady);
  const [token, setToken] = React.useState<IToken | null | undefined>(initToken);
  const [authenticated, setAuthenticated] = React.useState<boolean>(
    initAuthenticated
      ? initAuthenticated
      : moment.unix(token?.expiresIn ?? 0).isAfter(moment.now()),
  );
  const [userInfo, setUserInfo] = React.useState<IUserInfo | undefined>(initUserInfo);
  const [cookies, setCookies, removeCookie] = useCookies();

  /**
   * Store the token and user authenticated state.
   */
  const storeToken = React.useCallback(
    (token: IToken | null) => {
      setCookies(COOKIE_NAME, token, { path: '/' });
      setToken(token);
      setAuthenticated(moment.unix(token?.expiresIn ?? 0).isAfter(moment.now()));
    },
    [setCookies],
  );

  /**
   * Store the token and update state to indicate the user is authenticated.
   */
  const login = React.useCallback(
    (token: IToken) => {
      storeToken(token);
    },
    [storeToken],
  );

  /**
   * Remove state to indicate the user is unauthenticated.
   */
  const logout = React.useCallback(() => {
    removeCookie(COOKIE_NAME);
    storeToken(null);
    setToken(null);
    setAuthenticated(false);
  }, [storeToken, removeCookie]);

  // Logout if the token expires.
  React.useEffect(() => {
    if (token && tokenExpired(token?.accessToken)) {
      logout();
    }
  }, [token, login, logout]);

  // If a token wasn't provided check the cookie.
  React.useEffect(() => {
    if (!token) {
      const token = cookies[COOKIE_NAME] as IToken;
      if (token && !tokenExpired(token?.accessToken)) {
        login(token);
      }
    }
    if (!authReady) {
      setAuthReady(true);
    }
  }, [authReady, setAuthReady, cookies, token, login]);

  return (
    <PadlockContext.Provider
      value={{
        oidc,
        authReady,
        authenticated,
        token,
        userInfo,
        state: {
          setAuthReady,
          setAuthenticated,
          setToken,
          setUserInfo,
        },
        login,
        logout,
      }}
    >
      <SummonProvider
        baseApiUrl={baseApiUrl}
        autoRefreshToken={autoRefreshToken}
        loginPath={loginPath}
      >
        {children}
      </SummonProvider>
    </PadlockContext.Provider>
  );
};

export const PadlockConsumer = PadlockContext.Consumer;
