import { HTMLAttributes } from 'react';

import { Spinner } from '..';
import * as styled from './LoadingStyled';

export interface ILoadingProps extends HTMLAttributes<HTMLDivElement> {}

/**
 * Loading provides an overlay with a spinner to indicate to the user something is loading.
 * @param props Div element attributes.
 * @returns Loading component.
 */
export const Loading: React.FC<ILoadingProps> = (props) => {
  return (
    <styled.Loading>
      <Spinner size="4rem" />
      {props.children}
    </styled.Loading>
  );
};
