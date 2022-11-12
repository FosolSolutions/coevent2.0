import React from 'react';

import { IPaging, IUserModel, useBase } from '.';

/**
 * Hook with Admin User API endpoints.
 * @returns Component with method to make requests to admin user API endpoints.
 */
export const useUsers = () => {
  const api = useBase();

  return React.useMemo(
    () => ({
      getPage: async (page: number, quantity = 20): Promise<IPaging<IUserModel>> => {
        try {
          const response = await api.get(`/admin/users?page=${page}&qty=${quantity}`);
          return response.data as IPaging<IUserModel>;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      get: async (id: number): Promise<IUserModel> => {
        try {
          const response = await api.get(`/admin/users/${id}`);
          return response.data as IUserModel;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      add: async (model: IUserModel): Promise<IUserModel> => {
        try {
          const response = await api.post('/admin/users', model);
          return response.data as IUserModel;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      update: async (model: IUserModel): Promise<IUserModel> => {
        try {
          const response = await api.put(`/admin/users/${model.id}`, model);
          return response.data as IUserModel;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      remove: async (model: IUserModel): Promise<IUserModel> => {
        try {
          const response = await api.delete(`/admin/users/${model.id}`, { data: model });
          return response.data as IUserModel;
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
