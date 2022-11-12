import React from 'react';

import { ILoginModel, IParticipantLoginModel, ITokenModel, useBase } from '.';

/**
 * Hook with Authentication API endpoints.
 * @returns Component with method to make requests to authentication API endpoints.
 */
export const useAuth = () => {
  const api = useBase();

  return React.useMemo(
    () => ({
      login: async (model: ILoginModel): Promise<ITokenModel> => {
        try {
          const response = await api.post('/auth/login', model);
          return response.data as ITokenModel;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      loginAsParticipant: async (model: IParticipantLoginModel): Promise<ITokenModel> => {
        try {
          const response = await api.post('/auth/participants/login', model);
          return response.data as ITokenModel;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
      refreshToken: async (refreshToken: string): Promise<ITokenModel> => {
        try {
          const response = await api.post('/auth/token', {
            grant_type: 'refresh_token',
            refresh_token: refreshToken,
          });
          return response.data as ITokenModel;
        } catch (error) {
          // Handle error;
          return Promise.reject(error);
        }
      },
    }),
    [api],
  );
};

export default useAuth;
