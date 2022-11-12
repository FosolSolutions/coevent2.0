import { Button, ButtonVariant } from 'components';

import { IEventForm } from './interfaces';

export interface IMemorialProps {
  event: IEventForm;
}

export const Memorial: React.FC<IMemorialProps> = ({ event }) => {
  return (
    <div className="event">
      <span>Memorial</span>
      <div className="opening">
        <span>Presider</span>
        <div>
          <span>name name</span>
          <Button variant={ButtonVariant.secondary}>apply</Button>
        </div>
      </div>
      <div className="opening">
        <span>Exhorter</span>
        <Button variant={ButtonVariant.secondary}>apply</Button>
      </div>
      <div className="opening">
        <span>Doorman</span>
        <Button variant={ButtonVariant.secondary}>apply</Button>
      </div>
      <div className="opening">
        <span>Pianist</span>
        <Button variant={ButtonVariant.secondary}>apply</Button>
      </div>
      <div className="opening">
        <span>Readers</span>
        <ul>
          <li>
            <span>#1</span>
            <Button variant={ButtonVariant.secondary}>apply</Button>
          </li>
          <li>
            <span>#2</span>
            <Button variant={ButtonVariant.secondary}>apply</Button>
          </li>
        </ul>
      </div>
      <div className="opening">
        <span>Prayers</span>
        <ul>
          <li>
            <span>Bread</span>
            <Button variant={ButtonVariant.secondary}>apply</Button>
          </li>
          <li>
            <span>Wine</span>
            <Button variant={ButtonVariant.secondary}>apply</Button>
          </li>
          <li>
            <span>Close</span>
            <Button variant={ButtonVariant.secondary}>apply</Button>
          </li>
        </ul>
      </div>
    </div>
  );
};
