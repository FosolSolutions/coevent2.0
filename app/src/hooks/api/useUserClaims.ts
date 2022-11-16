import React from 'react';
import { toQueryString } from 'utils';

import { IPaging, IPagingFilter, IUserClaimModel, useBase } from '.';

/**
 * Hook with Admin UserClaim API endpoints.
 * @returns Component with method to make requests to admin claim API endpoints.
 */
export const useUserClaims = () => {
  const api = useBase();

  return React.useMemo(
    () => ({
      getPage: async (filter: IPagingFilter): Promise<IPaging<IUserClaimModel>> => {
        try {
          const response = await api.get<IPaging<IUserClaimModel>>(
            `/admin/user/claims?${toQueryString(filter)}`,
          );
          return response.data;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      get: async (
        userId: number,
        accountId: number,
        name: string,
        value: string,
      ): Promise<IUserClaimModel> => {
        try {
          const response = await api.get<IUserClaimModel>(
            `/admin/claims/${userId}/${accountId}/${name}/${value}`,
          );
          return response.data;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      add: async (model: IUserClaimModel): Promise<IUserClaimModel> => {
        try {
          const response = await api.post<IUserClaimModel>('/admin/user/claims', model);
          return response.data;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      update: async (model: IUserClaimModel): Promise<IUserClaimModel> => {
        try {
          const response = await api.put<IUserClaimModel>(
            `/admin/user/claims/${model.userId}/${model.accountId}/${model.name}/${model.value}`,
            model,
          );
          return response.data;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      remove: async (model: IUserClaimModel): Promise<IUserClaimModel> => {
        try {
          const response = await api.delete<IUserClaimModel>(
            `/admin/user/claims/${model.userId}/${model.accountId}/${model.name}/${model.value}`,
            {
              data: model,
            },
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

export default useUserClaims;
