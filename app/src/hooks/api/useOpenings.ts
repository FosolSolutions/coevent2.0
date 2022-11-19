import React from 'react';

import { IOpeningMessageModel, useBase } from '.';

/**
 * Hook with Admin Opening API endpoints.
 * @returns Component with method to make requests to application API endpoints.
 */
export const useOpenings = () => {
  const api = useBase();

  return React.useMemo(
    () => ({
      get: async (id: number): Promise<IOpeningMessageModel> => {
        try {
          const response = await api.get<IOpeningMessageModel>(
            `/schedules/events/activities/openings/${id}`,
          );
          return response.data;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      add: async (model: IOpeningMessageModel): Promise<IOpeningMessageModel> => {
        try {
          const response = await api.post<IOpeningMessageModel>(
            '/schedules/events/activities/openings',
            model,
          );
          return response.data;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      update: async (model: IOpeningMessageModel): Promise<IOpeningMessageModel> => {
        try {
          const response = await api.put<IOpeningMessageModel>(
            `/schedules/events/activities/openings/${model.id}`,
            model,
          );
          return response.data;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      remove: async (model: IOpeningMessageModel): Promise<IOpeningMessageModel> => {
        try {
          const response = await api.delete<IOpeningMessageModel>(
            `/schedules/events/activities/openings/${model.id}`,
            { data: model },
          );
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

export default useOpenings;
