import { TextVariant } from 'components';
import { IActivityOpeningModel, IApplicationModel, useApplications, usePadlock } from 'hooks';
import React from 'react';
import { useSchedules as useStore } from 'store/slices';

import { Text } from './../../components/form/text/TextStyled';
import { ApplyButton } from './ApplyButton';

export interface IActivityOpeningProps {
  showName?: boolean;
  opening: IActivityOpeningModel;
}

export const ActivityOpening: React.FC<IActivityOpeningProps> = ({ opening, showName = true }) => {
  const api = useApplications();
  const padlock = usePadlock();
  const [state, store] = useStore();

  const [answer, setAnswer] = React.useState('');

  const userId = parseInt(padlock.decode().uid ?? '');
  let application = opening.applications.find((a) => a.userId === userId);

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
    <div>
      {showName && <span>{opening.name}</span>}
      <div>
        {application?.user?.displayName && <p>{application.user.displayName}</p>}
        <ApplyButton canApply={!application} onClick={handleApplication} />
      </div>
      <div>
        {opening.question && <p>{opening.question}</p>}
        {opening.question && !application && (
          <Text
            name="message"
            variant={TextVariant.primary}
            onChange={handleAnswerQuestion}
            value={answer}
          />
        )}
        {application?.message && <p>{application.message}</p>}
      </div>
    </div>
  );
};
