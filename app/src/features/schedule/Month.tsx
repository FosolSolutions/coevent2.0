import { IScheduleModel } from 'hooks';
import moment from 'moment';

import { ScheduleEvent } from './ScheduleEvent';

export interface IMonthProps {
  schedule?: IScheduleModel;
  month: number;
  filter: string;
}

export const Month: React.FC<IMonthProps> = ({ schedule, month, filter }) => {
  const date = schedule?.startOn ? new Date(schedule.startOn) : new Date();
  date.setMonth(month - 1);

  const Events = () => {
    return (
      <>
        {schedule?.events
          .filter(
            (e) =>
              moment(e.startOn).month() === month && moment(e.startOn).format('dddd') === filter,
          )
          .map((e) => (
            <ScheduleEvent key={e.id} event={e} />
          ))}
      </>
    );
  };

  return (
    <div className="month">
      <span>{date.toLocaleString('en-US', { month: 'long' })}</span>
      <Events />
    </div>
  );
};
