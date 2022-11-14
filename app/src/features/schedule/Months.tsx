import { IScheduleModel } from 'hooks';
import moment from 'moment';

import { Month } from './Month';

export class ScheduleFilter {
  dayOfWeek: string;
  month: number;

  constructor(dayOfWeek: string) {
    this.dayOfWeek = dayOfWeek;
    this.month = 1;
  }
}

export interface IMonthsProps {
  schedule?: IScheduleModel;
  filter: ScheduleFilter;
}

export const Months: React.FC<IMonthsProps> = ({ schedule, filter }) => {
  var count = schedule ? moment(schedule?.endOn).diff(moment(schedule?.startOn), 'months') : 12;
  var months = Array.from({ length: count }, (_, i) => i + 1);

  return (
    <>
      {months.map((i) => (
        <Month key={i} schedule={schedule} filter={{ ...filter, month: i }} />
      ))}
    </>
  );
};
