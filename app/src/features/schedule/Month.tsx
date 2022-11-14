import { IScheduleModel } from 'hooks';
import moment from 'moment';

import { ScheduleFilter } from './Months';
import { ScheduleEvent } from './ScheduleEvent';

export interface IMonthProps {
  schedule?: IScheduleModel;
  filter: ScheduleFilter;
}

export const Month: React.FC<IMonthProps> = ({ schedule, filter }) => {
  const date = schedule?.startOn ? new Date(schedule.startOn) : new Date();
  date.setMonth(filter.month - 1);

  const Events = () => {
    return (
      <>
        {schedule?.events
          .filter(
            (e) =>
              moment(e.startOn).month() === filter.month &&
              moment(e.startOn).format('dddd') === filter.dayOfWeek,
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
