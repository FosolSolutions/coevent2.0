import React from 'react';

import { IPaging, IScheduleEventModel, useBase } from '.';

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
        page: number,
        quantity = 20,
      ): Promise<IPaging<IScheduleEventModel>> => {
        try {
          const response = await api.get(
            `/schedules/${scheduleId}/events?page=${page}&qty=${quantity}`,
          );
          return response.data as IPaging<IScheduleEventModel>;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      get: async (id: number): Promise<IScheduleEventModel> => {
        try {
          const response = await api.get(`/schedules/events/${id}`);
          return response.data as IScheduleEventModel;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      add: async (model: IScheduleEventModel): Promise<IScheduleEventModel> => {
        try {
          const response = await api.post('/schedules/events', model);
          return response.data as IScheduleEventModel;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      update: async (model: IScheduleEventModel): Promise<IScheduleEventModel> => {
        try {
          const response = await api.put(`/schedules/events/${model.id}`, model);
          return response.data as IScheduleEventModel;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      remove: async (model: IScheduleEventModel): Promise<IScheduleEventModel> => {
        try {
          const response = await api.delete(`/schedules/events/${model.id}`, { data: model });
          return response.data as IScheduleEventModel;
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
