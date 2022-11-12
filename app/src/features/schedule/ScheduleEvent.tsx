import { IScheduleEventModel } from 'hooks';
import moment from 'moment';

import * as styled from './ScheduleEventStyled';

export interface IScheduleEventProps {
  event: IScheduleEventModel;
}

export const ScheduleEvent: React.FC<IScheduleEventProps> = ({ event }) => {
  return (
    <styled.ScheduleEvent>
      <div>{moment(event.startOn).date()}</div>
      <div>{event.name}</div>
    </styled.ScheduleEvent>
  );
};
