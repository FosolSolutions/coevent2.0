import React from 'react';

import { IAccountModel, IPaging, useBase } from '.';

/**
 * Hook with Admin Account API endpoints.
 * @returns Component with method to make requests to admin account API endpoints.
 */
export const useAccounts = () => {
  const api = useBase();

  return React.useMemo(
    () => ({
      getPage: async (page: number, quantity = 20): Promise<IPaging<IAccountModel>> => {
        try {
          const response = await api.get(`/admin/accounts?page=${page}&qty=${quantity}`);
          return response.data as IPaging<IAccountModel>;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      get: async (id: number): Promise<IAccountModel> => {
        try {
          const response = await api.get(`/admin/accounts/${id}`);
          return response.data as IAccountModel;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      add: async (model: IAccountModel): Promise<IAccountModel> => {
        try {
          const response = await api.post('/admin/accounts', model);
          return response.data as IAccountModel;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      update: async (model: IAccountModel): Promise<IAccountModel> => {
        try {
          const response = await api.put(`/admin/accounts/${model.id}`, model);
          return response.data as IAccountModel;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      remove: async (model: IAccountModel): Promise<IAccountModel> => {
        try {
          const response = await api.delete(`/admin/accounts/${model.id}`, { data: model });
          return response.data as IAccountModel;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
    }),
    [api],
  );
};

export default useAccounts;
