import { Button, ButtonVariant } from 'components';
import { useApi } from 'hooks';
import React from 'react';
import { useSchedules as useStore } from 'store/slices';

import { Months, ScheduleFilter } from './Months';
import * as styled from './ScheduleStyled';

export interface IScheduleProps {}

export const Schedule: React.FC<IScheduleProps> = () => {
  const api = useApi();
  const [state, store] = useStore();

  const [filter, setFilter] = React.useState<string>('Sunday');

  React.useEffect(() => {
    api.schedules
      .getPage({ page: 1 })
      .then(async (res) => {
        const schedule = res.items[0];
        store.storeSchedule(schedule);
        const res_1 = await api.events.getPage(schedule.id, { page: 1, quantity: 1000 });
        return store.storeSchedule({ ...schedule, events: res_1.items });
      })
      .catch();
  }, [api, store]);

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
        <Months schedule={state.schedule} filter={new ScheduleFilter(filter)} />
      </div>
    </styled.Schedule>
  );
};
