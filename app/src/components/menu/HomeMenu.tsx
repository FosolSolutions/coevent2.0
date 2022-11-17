import '@szhsin/react-menu/dist/index.css';
import '@szhsin/react-menu/dist/transitions/slide.css';

import { Menu } from '@szhsin/react-menu';
import { Role, usePadlock } from 'hooks';
import { FaHome } from 'react-icons/fa';
import { useNavigate } from 'react-router-dom';

import { MenuLink } from '.';
import * as styled from './styled';

export const HomeMenu = () => {
  const padlock = usePadlock();
  const navigate = useNavigate();

  const isAdmin = padlock.hasRole(Role.administrator);

  return padlock.authenticated ? (
    <styled.Menu>
      <styled.HomeButton onClick={() => navigate('/')} title="home">
        <FaHome />
      </styled.HomeButton>
      <styled.MenuButton onClick={() => navigate('/calendars')}>Calendar</styled.MenuButton>
      <Menu menuButton={<styled.MenuButton>Schedules</styled.MenuButton>} transition>
        <MenuLink to="/schedules">Victoria Ecclesia</MenuLink>
      </Menu>
      {isAdmin && (
        <Menu menuButton={<styled.MenuButton>Administration</styled.MenuButton>} transition>
          {/* <MenuLink to="/admin/calendars">Calendars</MenuLink>
          <MenuLink to="/admin/accounts">Accounts</MenuLink> */}
          <MenuLink to="/admin/users">Users</MenuLink>
          {/* <MenuLink to="/admin/roles">Roles</MenuLink> */}
        </Menu>
      )}
    </styled.Menu>
  ) : null;
};
