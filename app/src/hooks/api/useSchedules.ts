import React from 'react';

import { IPaging, IScheduleModel, useBase } from '.';

/**
 * Hook with Admin Schedule API endpoints.
 * @returns Component with method to make requests to admin schedule API endpoints.
 */
export const useSchedules = () => {
  const api = useBase();

  return React.useMemo(
    () => ({
      getPage: async (page: number, quantity = 20): Promise<IPaging<IScheduleModel>> => {
        try {
          const response = await api.get(`/schedules?page=${page}&qty=${quantity}`);
          return response.data as IPaging<IScheduleModel>;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      get: async (id: number): Promise<IScheduleModel> => {
        try {
          const response = await api.get(`/schedules/${id}`);
          return response.data as IScheduleModel;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      add: async (model: IScheduleModel): Promise<IScheduleModel> => {
        try {
          const response = await api.post('/schedules', model);
          return response.data as IScheduleModel;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      update: async (model: IScheduleModel): Promise<IScheduleModel> => {
        try {
          const response = await api.put(`/schedules/${model.id}`, model);
          return response.data as IScheduleModel;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      remove: async (model: IScheduleModel): Promise<IScheduleModel> => {
        try {
          const response = await api.delete(`/schedules/${model.id}`, { data: model });
          return response.data as IScheduleModel;
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
