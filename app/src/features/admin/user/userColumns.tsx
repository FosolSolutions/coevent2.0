// eslint-disable-next-line @typescript-eslint/no-unused-vars
import { AxiosError } from 'axios';
import { Button, ButtonVariant, Show, Spinner } from 'components';
import { IUserModel, useApi } from 'hooks';
import React from 'react';
import { useNavigate } from 'react-router-dom';
import { CellProps, Column } from 'react-table';
import { toast } from 'react-toastify';

import { toModel } from './utils';

const EnabledColumn = ({ value }: { value: boolean }) => (
  <div>
    <input type="checkbox" defaultChecked={value} value={value ? 'true' : 'false'} />
  </div>
);

const EllipsisColumn = ({ value }: { value: string }) => <span className="ellipsis">{value}</span>;

const GoToUserColumn = (cell: CellProps<IUserModel, string>) => {
  const navigate = useNavigate();

  return (
    <div>
      <Button
        variant={ButtonVariant.link}
        onClick={() => navigate(`/admin/users/${cell.row.original.id}`)}
      >
        {cell.value}
      </Button>
    </div>
  );
};

const InviteColumn = (cell: CellProps<IUserModel, string>) => {
  const api = useApi();

  const [loading, setLoading] = React.useState(false);

  const invite = async () => {
    try {
      setLoading(true);
      const user = await api.users.get(cell.row.original.id);
      const res = await api.mail.invite(toModel(user));
      toast.success(`Invitation sent to ${res.to.email}`);
    } catch (ex: any | AxiosError) {
      toast.error(ex.message);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div>
      <Button variant={ButtonVariant.warning} onClick={invite} disabled={loading}>
        Invite
        <Show on={loading}>
          <Spinner />
        </Show>
      </Button>
    </div>
  );
};

export const columns: Column<IUserModel>[] = [
  {
    Header: 'Username',
    accessor: 'username',
    Cell: GoToUserColumn,
  },
  {
    Header: 'email',
    accessor: 'email',
    Cell: EllipsisColumn,
  },
  {
    Header: 'First Name',
    accessor: 'firstName',
    Cell: EllipsisColumn,
  },
  {
    Header: 'Last Name',
    accessor: 'lastName',
    Cell: EllipsisColumn,
  },
  {
    Header: 'Type',
    accessor: 'userType',
  },
  {
    Header: 'Enabled',
    accessor: 'isEnabled',
    Cell: EnabledColumn,
  },
  {
    Header: 'Invite',
    Cell: InviteColumn,
  },
];
