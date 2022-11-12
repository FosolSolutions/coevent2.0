import React from 'react';

import { IEventModel, useBase } from '.';

/**
 * Hook with Event API endpoints.
 * @returns Component with method to make requests to event API endpoints.
 */
export const useEvents = () => {
  const api = useBase();

  return React.useMemo(
    () => ({
      get: async (id: number): Promise<IEventModel> => {
        try {
          const response = await api.get(`/admin/events/${id}`);
          return response.data as IEventModel;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
    }),
    [api],
  );
};

export default useEvents;
