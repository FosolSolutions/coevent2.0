import React from 'react';

import { IApplicationModel, useBase } from '.';

/**
 * Hook with Admin Application API endpoints.
 * @returns Component with method to make requests to application API endpoints.
 */
export const useApplications = () => {
  const api = useBase();

  return React.useMemo(
    () => ({
      get: async (id: number): Promise<IApplicationModel> => {
        try {
          const response = await api.get(`/schedules/activities/openings/applications/${id}`);
          return response.data as IApplicationModel;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      add: async (model: IApplicationModel): Promise<IApplicationModel> => {
        try {
          const response = await api.post('/schedules/activities/openings/applications', model);
          return response.data as IApplicationModel;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      update: async (model: IApplicationModel): Promise<IApplicationModel> => {
        try {
          const response = await api.put(
            `/schedules/activities/openings/applications/${model.id}`,
            model,
          );
          return response.data as IApplicationModel;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      remove: async (model: IApplicationModel): Promise<IApplicationModel> => {
        try {
          const response = await api.delete(
            `/schedules/activities/openings/applications/${model.id}`,
            { data: model },
          );
          return response.data as IApplicationModel;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
    }),
    [api],
  );
};

export default useApplications;
