import { IScheduleEventModel } from 'hooks';
import moment from 'moment';

import { EventActivity } from './EventActivity';
import * as styled from './ScheduleEventStyled';

export interface IScheduleEventProps {
  event: IScheduleEventModel;
}

export const ScheduleEvent: React.FC<IScheduleEventProps> = ({ event }) => {
  return (
    <styled.ScheduleEvent>
      <div>{moment(event.startOn).date()}</div>
      <div>{event.name}</div>
      {event.activities.map((a) => (
        <EventActivity key={a.id} activity={a} />
      ))}
    </styled.ScheduleEvent>
  );
};
