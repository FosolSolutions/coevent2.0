import React from 'react';
import { toQueryString } from 'utils';

import { IPaging, IPagingFilter, IRoleModel, useBase } from '.';

/**
 * Hook with Admin Role API endpoints.
 * @returns Component with method to make requests to admin role API endpoints.
 */
export const useRoles = () => {
  const api = useBase();

  return React.useMemo(
    () => ({
      getPage: async (filter: IPagingFilter): Promise<IPaging<IRoleModel>> => {
        try {
          const response = await api.get<IPaging<IRoleModel>>(
            `/admin/roles?${toQueryString(filter)}`,
          );
          return response.data;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      get: async (id: number): Promise<IRoleModel> => {
        try {
          const response = await api.get<IRoleModel>(`/admin/roles/${id}`);
          return response.data;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      add: async (model: IRoleModel): Promise<IRoleModel> => {
        try {
          const response = await api.post<IRoleModel>('/admin/roles', model);
          return response.data;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      update: async (model: IRoleModel): Promise<IRoleModel> => {
        try {
          const response = await api.put<IRoleModel>(`/admin/roles/${model.id}`, model);
          return response.data;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      remove: async (model: IRoleModel): Promise<IRoleModel> => {
        try {
          const response = await api.delete<IRoleModel>(`/admin/roles/${model.id}`, {
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

export default useRoles;
