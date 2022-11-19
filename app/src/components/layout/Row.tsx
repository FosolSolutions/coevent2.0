import { HTMLAttributes } from 'react';

import * as styled from './styled';

export const Row: React.FC<HTMLAttributes<HTMLDivElement>> = ({ className, ...rest }) => {
  return <styled.Row className={`row${className ? ` ${className}` : ''}`} {...rest}></styled.Row>;
};
