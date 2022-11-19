import { Button, ButtonVariant, Row } from 'components';
import { useApi } from 'hooks';
import React from 'react';
import { useParams } from 'react-router-dom';
import { useSchedules as useStore } from 'store/slices';

import { Months, ScheduleFilter } from './Months';
import * as styled from './styled';

export interface IScheduleProps {}

export const Schedule: React.FC<IScheduleProps> = () => {
  const api = useApi();
  const [{ schedule }, { storeSchedule }] = useStore();
  const { id } = useParams();

  const [filter, setFilter] = React.useState<string>('Sunday');

  const scheduleId = !!id ? parseInt(id) : 1;

  React.useEffect(() => {
    if (!schedule) {
      api.schedules
        .get(scheduleId)
        .then(async (res) => {
          storeSchedule(res);
          const res_1 = await api.events.getPage(res.id, { page: 1, quantity: 1000 });
          return storeSchedule({ ...res, events: res_1.items });
        })
        .catch();
    }
  }, [api.events, api.schedules, schedule, scheduleId, storeSchedule]);

  return (
    <styled.Schedule>
      <Row>
        <h3>Filter:</h3>
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
      </Row>
      <div className="schedule">
        {!!schedule && <Months schedule={schedule} filter={new ScheduleFilter(filter)} />}
      </div>
    </styled.Schedule>
  );
};
