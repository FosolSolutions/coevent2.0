import React from 'react';

import { useAccounts, useAuth, useBase, useScheduleEvents, useUsers } from '.';
import { useSchedules } from './useSchedules';

/**
 * Common hook to make requests to the APi.
 * @returns CustomAxios object setup for the API.
 */
export const useApi = () => {
  const base = useBase();
  const auth = useAuth();
  const accounts = useAccounts();
  const users = useUsers();
  const schedules = useSchedules();
  const events = useScheduleEvents();

  return React.useMemo(
    () => ({
      ...base,
      auth,
      accounts,
      schedules,
      events,
      users,
    }),
    [base, auth, schedules, events, accounts, users],
  );
};

export default useApi;
