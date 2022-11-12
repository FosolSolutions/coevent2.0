import { Button, ButtonVariant, GridTable } from 'components';
import { IUserModel, useApi } from 'hooks';
import React from 'react';
import { useNavigate } from 'react-router-dom';
import { Row } from 'react-table';

import * as styled from './styled';
import { columns } from './userColumns';

/**
 * Users component provides a way to list and filter users.
 * @returns User administrative component.
 */
export const Users = () => {
  const api = useApi();
  const navigate = useNavigate();
  const [users, setUsers] = React.useState<IUserModel[]>([]);

  React.useEffect(() => {
    api.users.getPage(1).then((results) => setUsers(results));
  }, [api]);

  return (
    <styled.Users>
      <div>
        <h1>Users</h1>
        <div>
          <Button variant={ButtonVariant.success} onClick={() => navigate('/admin/users/0')}>
            Add New
          </Button>
        </div>
      </div>
      <div>
        <GridTable
          columns={columns}
          data={users}
          onRowClick={(row: Row<IUserModel>) => navigate(`/admin/users/${row.original.id}`)}
        ></GridTable>
      </div>
    </styled.Users>
  );
};
