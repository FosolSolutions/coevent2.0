import React from 'react';

import { HomeMenu, Loading, UserMenu } from '..';
import * as styled from './LayoutStyled';

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
  const [isLoading] = React.useState(false);

  return (
    <styled.Layout {...rest}>
      <header>
        <HomeMenu />
        <UserMenu />
      </header>
      <main>
        {children}
        {isLoading && <Loading />}
      </main>
    </styled.Layout>
  );
};
