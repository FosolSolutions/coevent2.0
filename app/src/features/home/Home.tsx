import { Dialog } from '@headlessui/react';
import { Button, ButtonVariant, Logo } from 'components';
import { usePadlock } from 'hooks';
import React from 'react';
import { useNavigate } from 'react-router-dom';

import * as styled from './HomeStyled';

export const Home: React.FC<React.HTMLAttributes<HTMLDivElement>> = ({ className, ...rest }) => {
  const padlock = usePadlock();
  const navigate = useNavigate();

  const [showModal, setShowModal] = React.useState(false);

  return (
    <styled.Home className={`home${!!className ? ` ${className}` : ''}`} {...rest}>
      <Logo onClick={() => setShowModal(true)} />
      <p>Scheduling for teams</p>
      {!padlock.authenticated && (
        <div>
          <Button variant={ButtonVariant.warning} onClick={() => navigate('/login')}>
            Login
          </Button>
        </div>
      )}
      <Dialog open={showModal} onClose={() => setShowModal(false)} className="dialog">
        <div className="panel">
          <Dialog.Panel>
            <Dialog.Title>Welcome to CoEvent</Dialog.Title>
            <Dialog.Description>View a schedule and start applying to openings!</Dialog.Description>
            <p>Click on the openings that are available and it will record your application.</p>
            <p>
              If you mistakenly apply to an opening, click again and it will withdraw your name.
            </p>
            <div>
              <Button onClick={() => navigate('/schedules')}>Schedules</Button>
            </div>
          </Dialog.Panel>
        </div>
      </Dialog>
    </styled.Home>
  );
};
