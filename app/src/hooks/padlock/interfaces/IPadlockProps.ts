export interface IPadlockProps {
  /**
   * The API URL path.
   */
  baseApiUrl?: string;
  /**
   * The login path if the user is unauthenticated or unauthorized.
   */
  loginPath?: string;
  /**
   * Whether to automatically refresh the access token.
   */
  autoRefreshToken?: boolean;
}
