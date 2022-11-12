import { Button, ButtonVariant, Text, TextVariant } from 'components';

import { IEventForm } from './interfaces';

export interface IMemorialProps {
  event: IEventForm;
}

export const Lecture: React.FC<IMemorialProps> = ({ event }) => {
  return (
    <div className="event">
      <span>Bible Talk</span>
      <div className="opening">
        <span>Presider</span>
        <Button variant={ButtonVariant.secondary}>apply</Button>
      </div>
      <div className="opening">
        <span>Pianist</span>
        <Button variant={ButtonVariant.secondary}>apply</Button>
      </div>
      <div className="opening">
        <span>Speaker</span>
        <Button variant={ButtonVariant.secondary}>apply</Button>
      </div>
      <div className="opening">
        <span>Topic</span>
        <Text variant={TextVariant.secondary} />
      </div>
    </div>
  );
};
