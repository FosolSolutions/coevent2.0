import React from 'react';
import { toQueryString } from 'utils';

import { IPaging, IPagingFilter, IScheduleEventModel, useBase } from '.';

/**
 * Hook with Admin ScheduleEvent API endpoints.
 * @returns Component with method to make requests to admin schedule API endpoints.
 */
export const useScheduleEvents = () => {
  const api = useBase();

  return React.useMemo(
    () => ({
      getPage: async (
        scheduleId: number,
        filter: IPagingFilter,
      ): Promise<IPaging<IScheduleEventModel>> => {
        try {
          const response = await api.get<IPaging<IScheduleEventModel>>(
            `/schedules/${scheduleId}/events?${toQueryString(filter)}`,
          );
          return response.data;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      get: async (id: number): Promise<IScheduleEventModel> => {
        try {
          const response = await api.get<IScheduleEventModel>(`/schedules/events/${id}`);
          return response.data;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      add: async (model: IScheduleEventModel): Promise<IScheduleEventModel> => {
        try {
          const response = await api.post<IScheduleEventModel>('/schedules/events', model);
          return response.data;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      update: async (model: IScheduleEventModel): Promise<IScheduleEventModel> => {
        try {
          const response = await api.put<IScheduleEventModel>(
            `/schedules/events/${model.id}`,
            model,
          );
          return response.data;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      remove: async (model: IScheduleEventModel): Promise<IScheduleEventModel> => {
        try {
          const response = await api.delete<IScheduleEventModel>(`/schedules/events/${model.id}`, {
            data: model,
          });
          return response.data;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
    }),
    [api],
  );
};

export default useScheduleEvents;
