import React from 'react';

import * as styled from './FooterStyled';

type IFooterProps = React.HTMLAttributes<HTMLElement>;

/**
 * Footer provides a footer element to place at the bottom of the page.
 * By default includes links to 'Disclaimer, Privacy, Assessibility, Copyright, and Contact Us'.
 * @param param0 Footer element attributes.
 * @returns Footer component.
 */
export const Footer: React.FC<IFooterProps> = ({ children, ...rest }) => {
  return <styled.Footer {...rest}>{children}</styled.Footer>;
};
