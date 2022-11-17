import { IScheduleEventModel } from 'hooks';

import { EventActivity } from './EventActivity';
import * as styled from './styled';

export interface IScheduleEventProps {
  event: IScheduleEventModel;
}

export const ScheduleEvent: React.FC<IScheduleEventProps> = ({ event }) => {
  return (
    <styled.ScheduleEvent className="event">
      <div>
        <h2>{event.name}</h2>
      </div>
      <div className="activities">
        {event.activities.map((a) => (
          <EventActivity key={a.id} activity={a} />
        ))}
      </div>
    </styled.ScheduleEvent>
  );
};
