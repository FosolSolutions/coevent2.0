import React from 'react';
import { toQueryString } from 'utils';

import { IClaimModel, IPaging, IPagingFilter, useBase } from '.';

/**
 * Hook with Admin Claim API endpoints.
 * @returns Component with method to make requests to admin claim API endpoints.
 */
export const useClaims = () => {
  const api = useBase();

  return React.useMemo(
    () => ({
      getPage: async (filter: IPagingFilter): Promise<IPaging<IClaimModel>> => {
        try {
          const response = await api.get<IPaging<IClaimModel>>(
            `/admin/claims?${toQueryString(filter)}`,
          );
          return response.data;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      get: async (id: number): Promise<IClaimModel> => {
        try {
          const response = await api.get<IClaimModel>(`/admin/claims/${id}`);
          return response.data;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      add: async (model: IClaimModel): Promise<IClaimModel> => {
        try {
          const response = await api.post<IClaimModel>('/admin/claims', model);
          return response.data;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      update: async (model: IClaimModel): Promise<IClaimModel> => {
        try {
          const response = await api.put<IClaimModel>(`/admin/claims/${model.id}`, model);
          return response.data;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      remove: async (model: IClaimModel): Promise<IClaimModel> => {
        try {
          const response = await api.delete<IClaimModel>(`/admin/claims/${model.id}`, {
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

export default useClaims;
