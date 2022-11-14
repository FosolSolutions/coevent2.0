import { IEventActivityModel } from 'hooks';

import { ActivityOpening } from './ActivityOpening';

export interface IEventActivityProps {
  activity: IEventActivityModel;
}

export const EventActivity: React.FC<IEventActivityProps> = ({ activity }) => {
  return (
    <div>
      <span>{activity.name}</span>
      <div>
        {activity.openings.map((o) => (
          <ActivityOpening key={o.id} opening={o} showName={activity.openings.length > 1} />
        ))}
      </div>
    </div>
  );
};
