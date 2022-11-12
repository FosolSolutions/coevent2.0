import CoEventLogo from 'components/assets/coEventLogoWh.svg';
import SVG from 'react-inlinesvg';

import * as styled from './LogoStyled';

export const Logo = () => {
  return (
    <styled.Logo className="logo">
      <SVG src={CoEventLogo} title="CoEvent" />
    </styled.Logo>
  );
};
