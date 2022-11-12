import axios from 'axios';
import { Dialog } from 'components';
import {
  calcRefreshInterval,
  IResponseError,
  tokenExpired,
  tokenExpiring,
  usePadlock,
} from 'hooks';
import { isEmpty, isFunction } from 'lodash';
import React from 'react';
import { toast } from 'react-toastify';

import { defaultEnvelope, ISummonProviderProps, ISummonState } from '.';
import * as styled from './SummonStyled';

/**
 * SummonContext, provides shared state between AJAX requests.
 */
export const SummonContext = React.createContext<ISummonState>({ summon: axios.create() });

/**
 * SummonProvider, provides a way to initialize context.
 * @param param0 SummonProvider initialization properties.
 * @returns
 */
export const SummonProvider: React.FC<ISummonProviderProps> = ({
  baseApiUrl: initBaseApiUrl,
  lifecycleToasts,
  selector,
  envelope = defaultEnvelope,
  loginPath = '/login',
  autoRefreshToken = true,
  children,
}) => {
  const auth = usePadlock();
  const [loadingToastId, setLoadingToastId] = React.useState<React.ReactText | undefined>();
  const [isLoading, setLoading] = React.useState(false); // TODO: Handle multiple requests.
  const [baseApiUrl] = React.useState(initBaseApiUrl);
  const [showError, setShowError] = React.useState(false);
  const [error, setError] = React.useState<IResponseError | undefined>(undefined);

  const { accessToken, refreshToken } = auth?.token ?? {};
  const { token: tokenUrl } = auth?.oidc ?? {};
  const { authenticated, login, logout } = auth;
  const interval = calcRefreshInterval(accessToken);

  /**
   * Redirect to login page.
   */
  const redirectToLogin = React.useCallback(() => {
    const url = `${window.location.href.replace(window.location.pathname, loginPath)}${
      window.location.search ? '&' : '?'
    }redirect_uri=${window.location.pathname}`;
    window.location.replace(url);
  }, [loginPath]);

  /**
   * Create an axios instance with interceptors to handle authorization, loading, and errors.
   */
  const instance = React.useMemo(() => {
    const instance = axios.create({
      baseURL: baseApiUrl,
      headers: {
        'Access-Control-Allow-Origin': '*',
      },
    });

    instance.interceptors.request.use((config) => {
      if (accessToken && !config?.headers?.Authorization) {
        config!.headers!.Authorization = `Bearer ${accessToken}`;
      }
      const cancelTokenSource = axios.CancelToken.source();
      // axios.get('', { cancelToken: cancelTokenSource.token });
      // cancelTokenSource.cancel();

      // TODO: Figure out what this part is all about.
      if (selector !== undefined) {
        const cancelToken = selector({
          token: cancelTokenSource.token,
        });

        if (!isEmpty(cancelToken)) {
          throw new axios.Cancel(JSON.stringify(envelope(cancelToken)));
        }
      }
      if (lifecycleToasts?.loadingToast) {
        setLoadingToastId(lifecycleToasts.loadingToast());
      }
      setLoading(true);
      return config;
    });

    instance.interceptors.response.use(
      (response) => {
        if (lifecycleToasts?.successToast && response.status < 300) {
          loadingToastId && toast.dismiss(loadingToastId);
          lifecycleToasts.successToast();
        } else if (lifecycleToasts?.errorToast && response.status >= 300) {
          lifecycleToasts.errorToast();
        }
        setLoading(false);
        return response;
      },
      (error) => {
        setLoading(false);
        if (axios.isCancel(error)) {
          return Promise.resolve(error.message);
        }
        if (lifecycleToasts?.errorToast) {
          loadingToastId && toast.dismiss(loadingToastId);
          lifecycleToasts.errorToast();
        }

        // TODO: Handle when requests fail due to authentication.  This should redirect user to login page.
        // TODO: Provide popup dialog to indicate a need to login to access route.
        // TODO: Resolve uncaught promise error resulting from this code.
        if (error!.response!.status === 401) {
          redirectToLogin();
        }

        const data = error!.response!.data;
        if (data?.type === 'https://tools.ietf.org/html/rfc7231#section-6.5.1') {
          const validationError = data as IResponseError;
          setError(validationError);
          setShowError(true);
        }

        // TODO: This is not returning the error to an async/await try/catch implementation...
        // const errorMessage =
        //  errorToastMessage || (error.response && error.response.data.message) || String.ERROR;
        return Promise.reject(error);
      },
    );

    return instance;
  }, [
    accessToken,
    baseApiUrl,
    envelope,
    lifecycleToasts,
    loadingToastId,
    redirectToLogin,
    selector,
  ]);

  /**
   * If the token has expired and there is a valid refresh token, make a request to refresh the access token.
   * Then update padlock state with login.
   * If unable to refresh the token, logout.
   */
  const handleRefresh = React.useCallback(async () => {
    if (authenticated && accessToken && tokenExpiring(accessToken, interval)) {
      let expired = true;
      if (tokenUrl && refreshToken && !tokenExpired(refreshToken)) {
        const response = await instance.post(
          tokenUrl,
          {
            grant_type: 'refresh_token',
            refresh_token: refreshToken,
          },
          {
            headers: {
              Authorization: `Bearer ${refreshToken}`,
            },
          },
        );
        if (response.status === 200) {
          expired = false;
          login(response.data);
        }
      }

      if (expired) {
        logout();
      }
    }
  }, [authenticated, accessToken, refreshToken, tokenUrl, instance, login, logout, interval]);

  // Create an interval timer that will check to see if the access token needs to be refreshed.
  const refHandleRefresh = React.useRef(handleRefresh);
  refHandleRefresh.current = handleRefresh;
  React.useEffect(() => {
    // TODO: A refresh should only occur if the user is still on the page.  Need an isActive listener.
    if (autoRefreshToken) {
      const timer = setInterval(() => {
        refHandleRefresh.current();
      }, interval);

      return () => {
        clearInterval(timer);
      };
    }
  }, [interval, autoRefreshToken]);

  const props = { summon: instance };

  return (
    <SummonContext.Provider value={props}>
      {children
        ? isFunction(children)
          ? (children as (bag: ISummonProviderProps) => React.ReactNode)(
              props as ISummonProviderProps,
            )
          : children
        : null}
      <styled.Summon id="summon-overlay" className={isLoading ? 'show' : undefined}></styled.Summon>
      <Dialog title="Error" data={error} open={showError} onClose={setShowError} />
    </SummonContext.Provider>
  );
};

export const SummonConsumer = SummonContext.Consumer;
