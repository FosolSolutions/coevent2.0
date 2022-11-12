import { IAccountModel } from 'hooks/api';
import { Column } from 'react-table';

const disabledColumn = ({ value }: { value: boolean }) => (
  <input type="checkbox" defaultChecked={value} value={value ? 'true' : 'false'} />
);

export const columns: Column<IAccountModel>[] = [
  {
    Header: 'Name',
    accessor: 'name',
  },
  {
    Header: 'Description',
    accessor: 'description',
  },
  {
    Header: 'Type',
    accessor: 'accountType',
  },
  {
    Header: 'Disabled',
    accessor: 'isDisabled',
    Cell: disabledColumn,
  },
];
