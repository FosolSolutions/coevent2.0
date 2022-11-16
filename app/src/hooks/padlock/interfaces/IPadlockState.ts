import { Token } from 'hooks';

import { IAuthToken, IUserInfo } from '.';
import { IOIDCEndpoints } from './IOIDCEndpoints';

export interface IPadlockState {
  oidc?: IOIDCEndpoints;
  authReady: boolean;
  authenticated: boolean;
  token?: IAuthToken | null;
  /**
   * Decoded token values.
   */
  identity?: Token;
  userInfo?: IUserInfo;
  state: {
    setAuthReady: React.Dispatch<React.SetStateAction<boolean>>;
    setAuthenticated: React.Dispatch<React.SetStateAction<boolean>>;
    setToken: React.Dispatch<React.SetStateAction<IAuthToken | null | undefined>>;
    setUserInfo: React.Dispatch<React.SetStateAction<IUserInfo | undefined>>;
  };
  login: (token: IAuthToken) => void;
  logout: () => void;
}
