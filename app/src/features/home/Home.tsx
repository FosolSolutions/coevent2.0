import { Button, ButtonVariant, Logo } from 'components';
import { usePadlock } from 'hooks';
import { useNavigate } from 'react-router-dom';

import * as styled from './HomeStyled';

export const Home = () => {
  const padlock = usePadlock();
  const navigate = useNavigate();

  return (
    <styled.Home className="home">
      <Logo />
      <p>Scheduling for teams</p>
      {!padlock.authenticated && (
        <div>
          <Button variant={ButtonVariant.warning} onClick={() => navigate('/login')}>
            Login
          </Button>
        </div>
      )}
    </styled.Home>
  );
};
