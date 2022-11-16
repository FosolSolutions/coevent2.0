import React from 'react';
import { toQueryString } from 'utils';

import { IPaging, IPagingFilter, IScheduleModel, useBase } from '.';

/**
 * Hook with Admin Schedule API endpoints.
 * @returns Component with method to make requests to admin schedule API endpoints.
 */
export const useSchedules = () => {
  const api = useBase();

  return React.useMemo(
    () => ({
      getPage: async (filter: IPagingFilter): Promise<IPaging<IScheduleModel>> => {
        try {
          const response = await api.get<IPaging<IScheduleModel>>(
            `/schedules?${toQueryString(filter)}`,
          );
          return response.data;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      get: async (id: number): Promise<IScheduleModel> => {
        try {
          const response = await api.get<IScheduleModel>(`/schedules/${id}`);
          return response.data;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      add: async (model: IScheduleModel): Promise<IScheduleModel> => {
        try {
          const response = await api.post<IScheduleModel>('/schedules', model);
          return response.data;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      update: async (model: IScheduleModel): Promise<IScheduleModel> => {
        try {
          const response = await api.put<IScheduleModel>(`/schedules/${model.id}`, model);
          return response.data;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      remove: async (model: IScheduleModel): Promise<IScheduleModel> => {
        try {
          const response = await api.delete<IScheduleModel>(`/schedules/${model.id}`, {
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

export default useSchedules;
