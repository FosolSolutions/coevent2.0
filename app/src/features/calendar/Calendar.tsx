import * as styled from './CalendarStyled';

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ICalendarProps {}

export const Calendar: React.FC<ICalendarProps> = () => {
  return (
    <styled.Calendar>
      <div className="c-header c-week">
        <div className="c-day">Sunday</div>
        <div className="c-day">Monday</div>
        <div className="c-day">Tuesday</div>
        <div className="c-day">Wednesday</div>
        <div className="c-day">Thursday</div>
        <div className="c-day">Friday</div>
        <div className="c-day">Saturday</div>
      </div>
      <div className="c-week">
        <div className="c-day">1</div>
        <div className="c-day">2</div>
        <div className="c-day">3</div>
        <div className="c-day">4</div>
        <div className="c-day">5</div>
        <div className="c-day">6</div>
        <div className="c-day">7</div>
      </div>
      <div className="c-week">
        <div className="c-day">1</div>
        <div className="c-day">2</div>
        <div className="c-day">3</div>
        <div className="c-day">4</div>
        <div className="c-day">5</div>
        <div className="c-day">6</div>
        <div className="c-day">7</div>
      </div>
      <div className="c-week">
        <div className="c-day">1</div>
        <div className="c-day">2</div>
        <div className="c-day">3</div>
        <div className="c-day">4</div>
        <div className="c-day">5</div>
        <div className="c-day">6</div>
        <div className="c-day">7</div>
      </div>
      <div className="c-week">
        <div className="c-day">1</div>
        <div className="c-day">2</div>
        <div className="c-day">3</div>
        <div className="c-day">4</div>
        <div className="c-day">5</div>
        <div className="c-day">6</div>
        <div className="c-day">7</div>
      </div>
    </styled.Calendar>
  );
};
