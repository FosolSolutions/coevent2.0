import { SummonContext } from 'hooks';
import React from 'react';

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ISummonProps {}

/**
 * Wrapper for axios to include authentication token and error handling.
 * @param param0 Axios parameters.
 */
export const useSummon = () => {
  const context = React.useContext(SummonContext);

  return context;
};

export default useSummon;
