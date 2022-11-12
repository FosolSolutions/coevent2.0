import jwtDecode, { JwtPayload } from 'jwt-decode';

/**
 * Determines if the access token has expired.
 * @param token A JWT token string.
 * @returns True if the access token has expired.
 */
export const tokenExpired = (token?: string | null) => {
  return !(+tokenExpiresOn(token) > Date.now());
};

/**
 * Determine if the access token will expire before the next interval.
 * @param token A JWT token string.
 * @returns True if the access token will expire before the next interval.
 */
export const tokenExpiring = (token: string | null | undefined, interval: number) => {
  return !(+tokenExpiresOn(token) > Date.now() + interval);
};

/**
 * Decode the JWT token string.
 * @param token A JWT token string.
 * @returns A decoded token object.
 */
export const decodeToken = (token?: string | null): JwtPayload => {
  if (token) {
    const accessToken = jwtDecode<JwtPayload>(token);
    return accessToken;
  }
  return {};
};

/**
 * Extract the date and time when the token will expire.
 * @param token A JWT token string.
 * @returns The date and time when the token will expire, or a minimum date.
 */
export const tokenExpiresOn = (token?: string | null) => {
  if (token) {
    const accessToken = decodeToken(token);
    return accessToken?.exp ? new Date(accessToken.exp * 1000) : new Date(0);
  }
  return new Date(0);
};

/**
 * Calculates the number of milliseconds difference between now and the token expiry.
 * @param token A JWT token string.
 * @returns The number of milliseconds difference between now to the token expiry.
 */
export const tokenExpiresIn = (token?: string | null) => {
  const expiresOn = +tokenExpiresOn(token);
  return expiresOn - Date.now();
};

/**
 * Calculate a refresh interval of 1/2 the length of the token expiry time, or 15 seconds.
 * @param token A JWT token string.
 * @returns 1/4 the length of the token expiry time, or 15 seconds.
 */
export const calcRefreshInterval = (token?: string | null) => {
  const expiresIn = tokenExpiresIn(token);
  return expiresIn > 0 ? expiresIn / 4 : 15 * 1000;
};
