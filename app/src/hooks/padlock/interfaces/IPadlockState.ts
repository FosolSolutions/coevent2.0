import { IToken, IUserInfo } from '.';
import { IOIDCEndpoints } from './IOIDCEndpoints';

export interface IPadlockState {
  oidc?: IOIDCEndpoints;
  authReady: boolean;
  authenticated: boolean;
  token?: IToken | null;
  userInfo?: IUserInfo;
  state: {
    setAuthReady: React.Dispatch<React.SetStateAction<boolean>>;
    setAuthenticated: React.Dispatch<React.SetStateAction<boolean>>;
    setToken: React.Dispatch<React.SetStateAction<IToken | null | undefined>>;
    setUserInfo: React.Dispatch<React.SetStateAction<IUserInfo | undefined>>;
  };
  login: (token: IToken) => void;
  logout: () => void;
}
