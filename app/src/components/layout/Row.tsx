import { HTMLAttributes } from 'react';

import * as styled from './styled';

export const Row: React.FC<HTMLAttributes<HTMLDivElement>> = (props) => {
  return <styled.Row {...props}></styled.Row>;
};
