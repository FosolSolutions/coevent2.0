import '@szhsin/react-menu/dist/index.css';
import '@szhsin/react-menu/dist/transitions/slide.css';

import { Menu } from '@szhsin/react-menu';
import { Show } from 'components';
import { Role, usePadlock } from 'hooks';
import { FaHome } from 'react-icons/fa';
import { useResizeDetector } from 'react-resize-detector';
import { useNavigate } from 'react-router-dom';

import { MenuLink } from '.';
import * as styled from './styled';

export const HomeMenu = () => {
  const padlock = usePadlock();
  const navigate = useNavigate();
  const { ref, width } = useResizeDetector();

  const isAdmin = padlock.hasRole(Role.administrator);
  const narrow = width ? width <= 400 : false;

  return padlock.authenticated ? (
    <styled.Menu ref={ref}>
      <styled.HomeButton onClick={() => navigate('/')} title="home">
        <FaHome />
      </styled.HomeButton>
      <Show on={narrow}>
        <Menu menuButton={<styled.MenuButton>Menu</styled.MenuButton>} transition>
          <MenuLink to="/schedules">Schedule: Victoria Ecclesia</MenuLink>
          <Show on={isAdmin}>
            <MenuLink to="/admin/users">Admin: Users</MenuLink>
          </Show>
        </Menu>
      </Show>
      <Show on={!narrow}>
        <styled.MenuButton onClick={() => navigate('/calendars')}>Calendar</styled.MenuButton>
        <Menu menuButton={<styled.MenuButton>Schedules</styled.MenuButton>} transition>
          <MenuLink to="/schedules">Victoria Ecclesia</MenuLink>
        </Menu>
        <Show on={isAdmin}>
          <Menu menuButton={<styled.MenuButton>Administration</styled.MenuButton>} transition>
            {/* <MenuLink to="/admin/calendars">Calendars</MenuLink>
          <MenuLink to="/admin/accounts">Accounts</MenuLink> */}
            <MenuLink to="/admin/users">Users</MenuLink>
            {/* <MenuLink to="/admin/roles">Roles</MenuLink> */}
          </Menu>
        </Show>
      </Show>
    </styled.Menu>
  ) : null;
};
