import { IUserModel } from 'hooks/api';
import { Column } from 'react-table';

const enabledColumn = ({ value }: { value: boolean }) => (
  <div>
    <input type="checkbox" defaultChecked={value} value={value ? 'true' : 'false'} />
  </div>
);

const ellipsis = ({ value }: { value: string }) => <span className="ellipsis">{value}</span>;

export const columns: Column<IUserModel>[] = [
  {
    Header: 'Username',
    accessor: 'username',
    Cell: ellipsis,
  },
  {
    Header: 'email',
    accessor: 'email',
    Cell: ellipsis,
  },
  {
    Header: 'First Name',
    accessor: 'firstName',
    Cell: ellipsis,
  },
  {
    Header: 'Last Name',
    accessor: 'lastName',
    Cell: ellipsis,
  },
  {
    Header: 'Type',
    accessor: 'userType',
  },
  {
    Header: 'Verified',
    accessor: 'emailVerified',
    Cell: enabledColumn,
  },
  {
    Header: 'Enabled',
    accessor: 'isEnabled',
    Cell: enabledColumn,
  },
];
