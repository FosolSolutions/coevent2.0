import React from 'react';

import { IUserModel, useBase } from '.';
import { IInvitationModel } from './interfaces/mail';

/**
 * Hook with Admin User API endpoints.
 * @returns Component with method to make requests to admin mail API endpoints.
 */
export const useMail = () => {
  const api = useBase();

  return React.useMemo(
    () => ({
      invite: async (model: IUserModel): Promise<IInvitationModel> => {
        try {
          const response = await api.post<IInvitationModel>('/admin/mail/invite', model);
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

export default useMail;
