import { loadingBarReducer } from 'react-redux-loading-bar';

import { jwtSlice, navSlice } from './slices';
import { schedulesSlice } from './slices/schedules';

export const reducer = {
  loadingBar: loadingBarReducer,
  [jwtSlice.name]: jwtSlice.reducer,
  [navSlice.name]: navSlice.reducer,
  [schedulesSlice.name]: schedulesSlice.reducer,
};
