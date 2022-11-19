import CoEventLogo from 'components/assets/coEventLogoWh.svg';
import SVG from 'react-inlinesvg';

import * as styled from './LogoStyled';

export const Logo: React.FC<React.HTMLAttributes<HTMLDivElement>> = ({ className, ...rest }) => {
  return (
    <styled.Logo className={`logo${!!className ? ` ${className}` : ''}`} {...rest}>
      <SVG src={CoEventLogo} title="CoEvent" />
    </styled.Logo>
  );
};
