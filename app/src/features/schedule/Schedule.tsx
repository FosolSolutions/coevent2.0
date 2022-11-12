import { ScheduleDate } from './ScheduleDate';
import * as styled from './ScheduleStyled';

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface IScheduleProps {}

export const Schedule: React.FC<IScheduleProps> = () => {
  return (
    <styled.Schedule>
      <nav>
        <ul>
          <li>Sunday</li>
          <li>Thursday</li>
          <li>Cleaning</li>
        </ul>
      </nav>
      <div className="schedule">
        <div className="month">
          <span>January</span>
          <ScheduleDate date={new Date(2022, 0, 1)} />
          <ScheduleDate date={new Date(2022, 0, 2)} />
          <ScheduleDate date={new Date(2022, 0, 6)} />
          <ScheduleDate date={new Date(2022, 0, 8)} />
        </div>
        <div className="month">
          <span>February</span>
          <div className="date"></div>
          <div className="date"></div>
          <div className="date"></div>
          <div className="date"></div>
        </div>
        <div className="month">
          <span>March</span>
          <div className="date"></div>
          <div className="date"></div>
          <div className="date"></div>
          <div className="date"></div>
        </div>
        <div className="month">
          <span>April</span>
          <div className="date"></div>
          <div className="date"></div>
          <div className="date"></div>
          <div className="date"></div>
        </div>
        <div className="month">
          <span>May</span>
          <div className="date"></div>
          <div className="date"></div>
          <div className="date"></div>
          <div className="date"></div>
        </div>
        <div className="month">
          <span>June</span>
          <div className="date"></div>
          <div className="date"></div>
          <div className="date"></div>
          <div className="date"></div>
        </div>
      </div>
    </styled.Schedule>
  );
};
