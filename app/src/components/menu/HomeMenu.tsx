import '@szhsin/react-menu/dist/index.css';
import '@szhsin/react-menu/dist/transitions/slide.css';

import { Menu } from '@szhsin/react-menu';
import { usePadlock } from 'hooks';
import { useNavigate } from 'react-router-dom';

import { MenuLink } from '.';
import * as styled from './styled';

export const HomeMenu = () => {
  const account = usePadlock();
  const navigate = useNavigate();

  return account.authenticated ? (
    <styled.Menu>
      <styled.MenuButton onClick={() => navigate('/calendar')}>Calendar</styled.MenuButton>
      <Menu menuButton={<styled.MenuButton>Schedules</styled.MenuButton>} transition>
        <MenuLink to="/schedule">Victoria Ecclesia</MenuLink>
      </Menu>
      <Menu menuButton={<styled.MenuButton>Administration</styled.MenuButton>} transition>
        <MenuLink to="/admin/calendars">Calendars</MenuLink>
        <MenuLink to="/admin/accounts">Accounts</MenuLink>
        <MenuLink to="/admin/users">Users</MenuLink>
        <MenuLink to="/admin/roles">Roles</MenuLink>
      </Menu>
    </styled.Menu>
  ) : null;
};
