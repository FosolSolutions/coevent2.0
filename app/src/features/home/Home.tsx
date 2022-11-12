import { Logo } from 'components';

import * as styled from './HomeStyled';

export const Home = () => {
  return (
    <styled.Home className="home">
      <Logo />
      <p>Scheduling for teams</p>
    </styled.Home>
  );
};
