import { IEventActivityModel } from 'hooks';

import { ActivityOpening } from './ActivityOpening';
import * as styled from './EventActivityStyled';

export interface IEventActivityProps {
  activity: IEventActivityModel;
  showOpeningName?: boolean;
}

export const EventActivity: React.FC<IEventActivityProps> = ({
  activity,
  showOpeningName = false,
}) => {
  return (
    <styled.EventActivityStyled className="activity">
      <div className="header">
        <span>{activity.name}</span>
      </div>
      {activity.openings.map((o) => (
        <ActivityOpening
          key={o.id}
          opening={{ ...o, activity }}
          showName={showOpeningName || activity.openings.length > 1}
        />
      ))}
    </styled.EventActivityStyled>
  );
};
