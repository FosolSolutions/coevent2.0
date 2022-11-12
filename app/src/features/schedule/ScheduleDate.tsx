import { IEventForm } from './interfaces';
import { Lecture } from './Lecture';
import { Memorial } from './Memorial';

export interface IScheduleDateProps {
  date: Date;
}

const defaultEvents: IEventForm[] = [
  {
    id: 1,
    name: 'Memorial',
  },
  {
    id: 2,
    name: 'Bible Talk',
  },
];

export const ScheduleDate: React.FC<IScheduleDateProps> = ({ date }) => {
  const events = defaultEvents;
  return (
    <div className="date">
      <span className="date">{date.getDate()}</span>
      <div className="events">
        {events.map((event) => {
          switch (event.name) {
            case 'Memorial':
              return <Memorial key={event.id} event={event} />;
            case 'Bible Talk':
              return <Lecture key={event.id} event={event} />;
            case 'Bible Class':
              return <Lecture key={event.id} event={event} />;
            case 'Hall Cleaning':
              return <Lecture key={event.id} event={event} />;
            default:
              return null;
          }
        })}
      </div>
    </div>
  );
};
