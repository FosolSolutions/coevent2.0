import { Button, ButtonVariant } from 'components';
import { IScheduleModel, useApi } from 'hooks';
import React from 'react';

import { Months } from './Months';
import * as styled from './ScheduleStyled';

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface IScheduleProps {}

export const Schedule: React.FC<IScheduleProps> = () => {
  const api = useApi();

  const [schedule, setSchedule] = React.useState<IScheduleModel>();
  const [filter, setFilter] = React.useState<string>('Sunday');

  React.useEffect(() => {
    api.schedules
      .getPage(1)
      .then((res) => {
        const schedule = res.items[0];
        setSchedule(schedule);
        return api.events
          .getPage(schedule.id, 1, 1000)
          .then((res) => setSchedule({ ...schedule, events: res.items }));
      })
      .catch();
  }, [api]);

  return (
    <styled.Schedule>
      <nav>
        <ul>
          <li>
            <Button variant={ButtonVariant.link} onClick={() => setFilter('Sunday')}>
              Sunday
            </Button>
          </li>
          <li>
            <Button variant={ButtonVariant.link} onClick={() => setFilter('Thursday')}>
              Thursday
            </Button>
          </li>
          <li>
            <Button variant={ButtonVariant.link} onClick={() => setFilter('Saturday')}>
              Cleaning
            </Button>
          </li>
        </ul>
      </nav>
      <div className="schedule">
        <Months schedule={schedule} filter={filter} />
      </div>
    </styled.Schedule>
  );
};
