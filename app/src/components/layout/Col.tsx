import { HTMLAttributes } from 'react';

import * as styled from './styled';

export const Col: React.FC<HTMLAttributes<HTMLDivElement>> = (props) => {
  return <styled.Col {...props}></styled.Col>;
};
