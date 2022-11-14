import React from 'react';

import { useAccounts, useApplications, useAuth, useBase, useScheduleEvents, useUsers } from '.';
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
  const applications = useApplications();

  return React.useMemo(
    () => ({
      ...base,
      auth,
      accounts,
      schedules,
      events,
      users,
      applications,
    }),
    [base, auth, schedules, events, accounts, users, applications],
  );
};

export default useApi;
