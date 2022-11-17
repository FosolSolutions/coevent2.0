import { IScheduleEventModel } from 'hooks';
import moment, { Moment } from 'moment';

import { ScheduleEvent } from './ScheduleEvent';
import * as styled from './styled';

export interface IDayProps {
  date: Date | string | Moment;
  events: IScheduleEventModel[];
}

export const Day: React.FC<IDayProps> = ({ date, events }) => {
  return (
    <styled.Day>
      <div className="date">
        <h2>{moment(date).date()}</h2>
      </div>
      <div className="events">
        {events.map((e) => (
          <ScheduleEvent key={e.id} event={e} />
        ))}
      </div>
    </styled.Day>
  );
};
