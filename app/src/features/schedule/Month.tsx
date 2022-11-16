import { IScheduleModel } from 'hooks';
import moment from 'moment';
import React from 'react';
import { FaChevronDown, FaChevronUp } from 'react-icons/fa';

import { ScheduleFilter } from './Months';
import { ScheduleEvent } from './ScheduleEvent';
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

  const date = schedule?.startOn ? new Date(schedule.startOn) : new Date();
  date.setMonth(filter.month - 1);

  const handleResizeMonth = () => {
    setShow(!show);
  };

  return (
    <styled.Month className="month">
      <div className="header" onClick={handleResizeMonth}>
        <div title={show ? 'hide' : 'show'}>{show ? <FaChevronUp /> : <FaChevronDown />}</div>
        <h1>{date.toLocaleString('en-US', { month: 'long' })}</h1>
      </div>
      {!show && <hr />}
      <div className={`events ${show ? 'show' : 'hide'}`}>
        {schedule?.events
          .filter(
            (e) =>
              moment(e.startOn).month() === filter.month &&
              moment(e.startOn).format('dddd') === filter.dayOfWeek,
          )
          .map((e) => (
            <ScheduleEvent key={e.id} event={e} />
          ))}
      </div>
    </styled.Month>
  );
};
