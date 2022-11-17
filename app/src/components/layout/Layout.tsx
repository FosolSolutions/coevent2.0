import 'react-toastify/dist/ReactToastify.css';

import { usePadlock } from 'hooks';
import React from 'react';
import { ToastContainer } from 'react-toastify';

import { HomeMenu, Loading, UserMenu } from '..';
import * as styled from './styled/LayoutStyled';

interface ILayoutProps extends React.HTMLAttributes<HTMLDivElement> {
  /**
   * Site name to display in header.
   */
  name: string;
}

/**
 * Layout provides a div structure to organize the page.
 * @param param0 Div element attributes.
 * @returns Layout component.
 */
export const Layout: React.FC<ILayoutProps> = ({ name, children, ...rest }) => {
  const padlock = usePadlock();
  const [isLoading] = React.useState(false);

  return (
    <styled.Layout {...rest}>
      {padlock.authenticated && (
        <header>
          <HomeMenu />
          <UserMenu />
        </header>
      )}
      <main>
        {children}
        {isLoading && <Loading />}
        <ToastContainer />
      </main>
    </styled.Layout>
  );
};
