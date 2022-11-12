import { IUserModel } from 'hooks/api';
import { Column } from 'react-table';

const enabledColumn = ({ value }: { value: boolean }) => (
  <input type="checkbox" defaultChecked={value} value={value ? 'true' : 'false'} />
);

export const columns: Column<IUserModel>[] = [
  {
    Header: 'Username',
    accessor: 'username',
  },
  {
    Header: 'email',
    accessor: 'email',
  },
  {
    Header: 'First Name',
    accessor: 'firstName',
  },
  {
    Header: 'Last Name',
    accessor: 'lastName',
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
