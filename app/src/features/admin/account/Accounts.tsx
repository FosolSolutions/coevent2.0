import { Button, ButtonVariant, GridTable } from 'components';
import { IAccountModel, useApi } from 'hooks';
import React from 'react';
import { useNavigate } from 'react-router-dom';
import { Row } from 'react-table';

import { columns } from './accountColumns';
import * as styled from './styled';

/**
 * Accounts component provides a way to list and filter accounts.
 * @returns Account administrative component.
 */
export const Accounts = () => {
  const api = useApi();
  const navigate = useNavigate();
  const [accounts, setAccounts] = React.useState<IAccountModel[]>([]);

  React.useEffect(() => {
    api.accounts.getPage({ page: 1 }).then((results) => setAccounts(results.items));
  }, [api]);

  return (
    <styled.Accounts>
      <div>
        <h1>Accounts</h1>
        <div>
          <Button variant={ButtonVariant.success} onClick={() => navigate('/admin/accounts/0')}>
            Add New
          </Button>
        </div>
      </div>
      <div>
        <GridTable
          columns={columns}
          data={accounts}
          onRowClick={(row: Row<IAccountModel>) => navigate(`/admin/accounts/${row.original.id}`)}
        ></GridTable>
      </div>
    </styled.Accounts>
  );
};
