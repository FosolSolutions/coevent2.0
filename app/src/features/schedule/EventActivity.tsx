import { Show } from 'components';
import { IEventActivityModel } from 'hooks';

import { ActivityOpening } from './ActivityOpening';
import * as styled from './styled';

export interface IEventActivityProps {
  activity: IEventActivityModel;
  name?: string;
}

export const EventActivity: React.FC<IEventActivityProps> = ({
  activity,
  name = activity.name,
}) => {
  return (
    <styled.EventActivityStyled className="activity">
      <Show on={!!name}>
        <div>
          <h3>{name}</h3>
        </div>
      </Show>
      {activity.openings.map((o) => (
        <ActivityOpening
          key={o.id}
          opening={{ ...o, activity }}
          name={activity.openings.length > 1 ? undefined : ''}
        />
      ))}
    </styled.EventActivityStyled>
  );
};
