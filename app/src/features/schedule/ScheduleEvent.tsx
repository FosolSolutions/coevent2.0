import { IScheduleEventModel } from 'hooks';
import moment from 'moment';
import { Fragment } from 'react';

import { EventActivity } from './EventActivity';
import * as styled from './ScheduleEventStyled';

export interface IScheduleEventProps {
  event: IScheduleEventModel;
}

export const ScheduleEvent: React.FC<IScheduleEventProps> = ({ event }) => {
  return (
    <styled.ScheduleEvent>
      <div className="date">{moment(event.startOn).date()}</div>
      <div className="event">
        <div className="header">
          <h2>{event.name}</h2>
        </div>
        <div className="activities">
          {event.activities.map((a) => {
            if (a.openings.length === 1) return <EventActivity key={a.id} activity={a} />;
            else
              return (
                <Fragment key={a.id}>
                  {a.openings.map((o, i) => (
                    <EventActivity
                      key={o.id}
                      activity={{ ...a, openings: [a.openings[i]] }}
                      showOpeningName={true}
                    />
                  ))}
                </Fragment>
              );
          })}
        </div>
      </div>
    </styled.ScheduleEvent>
  );
};
