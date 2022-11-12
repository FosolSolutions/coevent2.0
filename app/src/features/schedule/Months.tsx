import { IScheduleModel } from 'hooks';

import { Month } from './Month';

export interface IMonthsProps {
  schedule?: IScheduleModel;
  filter: string;
}

export const Months: React.FC<IMonthsProps> = (props) => {
  return (
    <>
      {[1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12].map((i) => (
        <Month key={i} month={i} {...props} />
      ))}
    </>
  );
};
