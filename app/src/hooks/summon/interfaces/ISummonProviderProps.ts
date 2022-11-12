import { CancelToken } from 'axios';
import { IPadlockProps } from 'hooks';
import React from 'react';

import { ILifecycleToasts } from '.';

export const defaultEnvelope = (x: any) => ({ data: { records: x } });

export interface ISummonProviderProps extends IPadlockProps, React.HTMLAttributes<HTMLElement> {
  lifecycleToasts?: ILifecycleToasts;
  selector?: ({ token }: { token: CancelToken }) => void;
  envelope?: typeof defaultEnvelope;
}
