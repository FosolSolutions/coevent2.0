import React from 'react';
import { toQueryString } from 'utils';

import { IAccountModel, IPaging, IPagingFilter, useBase } from '.';

/**
 * Hook with Admin Account API endpoints.
 * @returns Component with method to make requests to admin account API endpoints.
 */
export const useAccounts = () => {
  const api = useBase();

  return React.useMemo(
    () => ({
      getPage: async (filter: IPagingFilter): Promise<IPaging<IAccountModel>> => {
        try {
          const response = await api.get<IPaging<IAccountModel>>(
            `/admin/accounts?${toQueryString(filter)}`,
          );
          return response.data;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      get: async (id: number): Promise<IAccountModel> => {
        try {
          const response = await api.get<IAccountModel>(`/admin/accounts/${id}`);
          return response.data;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      add: async (model: IAccountModel): Promise<IAccountModel> => {
        try {
          const response = await api.post<IAccountModel>('/admin/accounts', model);
          return response.data;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      update: async (model: IAccountModel): Promise<IAccountModel> => {
        try {
          const response = await api.put<IAccountModel>(`/admin/accounts/${model.id}`, model);
          return response.data;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      remove: async (model: IAccountModel): Promise<IAccountModel> => {
        try {
          const response = await api.delete<IAccountModel>(`/admin/accounts/${model.id}`, {
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

export default useAccounts;
