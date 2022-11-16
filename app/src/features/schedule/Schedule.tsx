import { Button, ButtonVariant } from 'components';
import { useApi } from 'hooks';
import React from 'react';
import { useSchedules as useStore } from 'store/slices';

import { Months, ScheduleFilter } from './Months';
import * as styled from './styled';

export interface IScheduleProps {}

export const Schedule: React.FC<IScheduleProps> = () => {
  const api = useApi();
  const [{ schedule }, { storeSchedule }] = useStore();

  const [filter, setFilter] = React.useState<string>('Sunday');

  React.useEffect(() => {
    if (!schedule) {
      api.schedules
        .getPage({ page: 1 })
        .then(async (res) => {
          const schedule = res.items[0];
          storeSchedule(schedule);
          const res_1 = await api.events.getPage(schedule.id, { page: 1, quantity: 1000 });
          return storeSchedule({ ...schedule, events: res_1.items });
        })
        .catch();
    }
  }, [api.events, api.schedules, schedule, storeSchedule]);

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
        <Months schedule={schedule} filter={new ScheduleFilter(filter)} />
      </div>
    </styled.Schedule>
  );
};
