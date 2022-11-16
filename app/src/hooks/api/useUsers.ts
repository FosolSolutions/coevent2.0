import React from 'react';
import { toQueryString } from 'utils';

import { IPaging, IPagingFilter, IUserModel, useBase } from '.';

/**
 * Hook with Admin User API endpoints.
 * @returns Component with method to make requests to admin user API endpoints.
 */
export const useUsers = () => {
  const api = useBase();

  return React.useMemo(
    () => ({
      getPage: async (filter: IPagingFilter): Promise<IPaging<IUserModel>> => {
        try {
          const response = await api.get<IPaging<IUserModel>>(
            `/admin/users?${toQueryString(filter)}`,
          );
          return response.data;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      get: async (id: number): Promise<IUserModel> => {
        try {
          const response = await api.get<IUserModel>(`/admin/users/${id}`);
          return response.data;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      add: async (model: IUserModel): Promise<IUserModel> => {
        try {
          const response = await api.post<IUserModel>('/admin/users', model);
          return response.data;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      update: async (model: IUserModel): Promise<IUserModel> => {
        try {
          const response = await api.put<IUserModel>(`/admin/users/${model.id}`, model);
          return response.data;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      remove: async (model: IUserModel): Promise<IUserModel> => {
        try {
          const response = await api.delete<IUserModel>(`/admin/users/${model.id}`, {
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

export default useUsers;
