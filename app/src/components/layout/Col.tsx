import { HTMLAttributes } from 'react';

import * as styled from './styled';

export const Col: React.FC<HTMLAttributes<HTMLDivElement>> = ({ className, ...rest }) => {
  return <styled.Col className={`col${className ? ` ${className}` : ''}`} {...rest}></styled.Col>;
};
