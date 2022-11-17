import { Show } from 'components';
import { IScheduleModel } from 'hooks';
import _ from 'lodash';
import moment from 'moment';
import React from 'react';
import { FaChevronDown, FaChevronUp } from 'react-icons/fa';

import { Day } from './Day';
import { ScheduleFilter } from './Months';
import * as styled from './styled';

export interface IMonthProps {
  schedule?: IScheduleModel;
  filter: ScheduleFilter;
}

/**
 * Displays a single month from the specified 'schedule' and the 'filter'.
 * @param param0 Component properties
 * @returns Component
 */
export const Month: React.FC<IMonthProps> = ({ schedule, filter }) => {
  const [show, setShow] = React.useState(true);

  const date = schedule?.startOn ? moment(schedule.startOn) : moment(new Date());
  date.month(filter.month - 1);
  const events =
    schedule?.events.filter(
      (e) =>
        moment(e.startOn).month() === filter.month &&
        moment(e.startOn).format('dddd') === filter.dayOfWeek,
    ) ?? [];

  const handleResizeMonth = () => {
    setShow(!show);
  };

  return (
    <styled.Month className="month">
      <div className="header" onClick={handleResizeMonth} title={show ? 'hide' : 'show'}>
        <div title={show ? 'hide' : 'show'}>{show ? <FaChevronUp /> : <FaChevronDown />}</div>
        <h1>{date.format('MMMM YYYY')}</h1>
      </div>
      <Show on={!show}>
        <hr />
      </Show>
      <Show on={show}>
        <div className="events">
          {_.chain(events)
            .groupBy((e) => moment(e.startOn).startOf('day').format('YYYY-MM-DD'))
            .map((e, date) => <Day key={date} date={date} events={e} />)
            .value()}
        </div>
      </Show>
    </styled.Month>
  );
};
