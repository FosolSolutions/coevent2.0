import { usePadlock } from 'hooks';
import { useNavigate } from 'react-router-dom';

import { Button, ButtonVariant, LogoutButton } from '..';
import * as styled from './styled';

/**
 * UserMenu provides information and actions for the current user.
 * Unauthenticated:
 *  - login button.
 * Authenticated:
 *  - user's identity.
 *  - logout button
 * @returns UserMenu component.
 */
export const UserMenu = () => {
  const auth = usePadlock();
  const navigate = useNavigate();
  const isLoginPage = window.location.pathname === '/login';

  return auth.authenticated ? (
    <styled.UserMenu>
      <div>{auth.userInfo?.displayName}</div>
      <LogoutButton onClick={() => auth.logout()} size={20} title="Logout" />
    </styled.UserMenu>
  ) : !isLoginPage ? (
    <Button variant={ButtonVariant.warning} onClick={() => navigate('/login')}>
      Login
    </Button>
  ) : null;
};
