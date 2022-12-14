import React from 'react';

import {
  useAccounts,
  useApplications,
  useAuth,
  useBase,
  useClaims,
  useMail,
  useOpenings,
  useRoles,
  useScheduleEvents,
  useUserClaims,
  useUsers,
} from '.';
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
  const roles = useRoles();
  const claims = useClaims();
  const userClaims = useUserClaims();
  const mail = useMail();
  const openings = useOpenings();

  return React.useMemo(
    () => ({
      ...base,
      auth,
      accounts,
      schedules,
      events,
      users,
      applications,
      roles,
      claims,
      userClaims,
      mail,
      openings,
    }),
    [
      base,
      auth,
      schedules,
      events,
      accounts,
      users,
      applications,
      roles,
      claims,
      userClaims,
      mail,
      openings,
    ],
  );
};

export default useApi;
