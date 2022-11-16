import { Text, TextVariant } from 'components';
import { IActivityOpeningModel, IApplicationModel, useApplications, usePadlock } from 'hooks';
import React from 'react';
import { useSchedules as useStore } from 'store/slices';

import { ApplyButton } from './ApplyButton';
import * as styled from './styled';

export interface IActivityOpeningProps {
  showName?: boolean;
  opening: IActivityOpeningModel;
}

export const ActivityOpening: React.FC<IActivityOpeningProps> = ({ opening, showName = true }) => {
  const api = useApplications();
  const padlock = usePadlock();
  const [state, store] = useStore();

  const [answer, setAnswer] = React.useState('');

  const userId = padlock.identity?.uid;
  const alreadyFilled =
    opening.limit <= opening.applications.length ||
    opening.applications.some((a) => a.userId === padlock.identity?.uid);
  const canApply =
    !alreadyFilled &&
    padlock.hasClaim(opening.requirements.map((r) => ({ name: r.name, value: r.value })));

  const handleApplication = async () => {
    try {
      let application = opening.applications.find((a) => a.userId === userId);
      if (!state.schedule) throw new Error('The schedule was not found');
      const open = { ...opening };

      if (!application) {
        const model: IApplicationModel = {
          id: 0,
          userId: padlock.identity!.uid,
          openingId: opening.id,
          message: answer,
        };
        application = await api.add(model);
        open.applications = [...opening.applications, application];
      } else {
        await api.remove(application);
        open.applications = opening.applications.filter((a) => a.id !== application!.id);
      }
      store.updateOpening(open);
    } catch (ex) {
      console.error(ex);
    }
  };

  const handleAnswerQuestion = (e: React.ChangeEvent<HTMLInputElement>) => {
    setAnswer(e.currentTarget.value);
  };

  return (
    <styled.ActivityOpening className="opening">
      {showName && (
        <div className="header">
          <span>{opening.name}</span>
        </div>
      )}
      {opening.applications.map((a) => (
        <div key={a.id} className="applicant">
          <span>{a.user?.displayName}</span>
          {!!a.message && <p>{a.message}</p>}
          {a.userId === padlock.identity?.uid && (
            <ApplyButton active={true} onClick={handleApplication} />
          )}
        </div>
      ))}
      {canApply && (
        <div className="applicant">
          {opening.question && (
            <div className="question">
              <label htmlFor={`message-${opening.id}`}>{opening.question}</label>
              <Text
                id={`message-${opening.id}`}
                name="message"
                variant={TextVariant.primary}
                onChange={handleAnswerQuestion}
                value={answer}
              />
            </div>
          )}
          <ApplyButton onClick={handleApplication} />
        </div>
      )}
    </styled.ActivityOpening>
  );
};
