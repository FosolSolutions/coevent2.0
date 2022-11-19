import { Row, Show } from 'components';
import { IScheduleEventModel } from 'hooks';
import { FaInfoCircle } from 'react-icons/fa';

import { EventActivity } from './EventActivity';
import * as styled from './styled';

export interface IScheduleEventProps {
  event: IScheduleEventModel;
}

export const ScheduleEvent: React.FC<IScheduleEventProps> = ({ event }) => {
  return (
    <styled.ScheduleEvent className={`event series-${event.seriesId ?? '0'}`}>
      <div className="header">
        <h2>{event.name}</h2>
        <Show on={!!event.series}>
          <Row title={event.series?.description}>
            <h3>{event.series?.name}</h3>
            <FaInfoCircle />
          </Row>
        </Show>
      </div>
      <div className="activities">
        {event.activities.map((a) => (
          <EventActivity key={a.id} activity={{ ...a, event }} />
        ))}
      </div>
    </styled.ScheduleEvent>
  );
};
