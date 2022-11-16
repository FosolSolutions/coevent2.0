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
  let application = opening.applications.find((a) => a.userId === userId);
  const canApply = padlock.hasClaim(
    opening.requirements.map((r) => ({ name: r.name, value: r.value })),
  );

  const handleApplication = async () => {
    try {
      if (!state.schedule) throw new Error('The schedule was not found');
      const open = { ...opening };

      if (!application) {
        const model: IApplicationModel = {
          id: 0,
          userId: 1,
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
      <div className="applicant">
        {application?.user?.displayName && <span>{application.user.displayName}</span>}
        {opening.question && (
          <div className="question">
            <label htmlFor={`message-${opening.id}`}>{opening.question}</label>
            {!application && (
              <Text
                id={`message-${opening.id}`}
                name="message"
                variant={TextVariant.primary}
                onChange={handleAnswerQuestion}
                value={answer}
              />
            )}
            {application?.message && <p>{application.message}</p>}
          </div>
        )}
        {canApply && <ApplyButton canApply={!application} onClick={handleApplication} />}
      </div>
    </styled.ActivityOpening>
  );
};
