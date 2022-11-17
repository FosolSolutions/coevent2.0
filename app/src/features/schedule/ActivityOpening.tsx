// eslint-disable-next-line @typescript-eslint/no-unused-vars
import { AxiosError } from 'axios';
import { Button, Show, Spinner, Text, TextVariant } from 'components';
import { IActivityOpeningModel, IApplicationModel, useApi, usePadlock } from 'hooks';
import React from 'react';
import { FaArrowAltCircleRight } from 'react-icons/fa';
import { toast } from 'react-toastify';
import { useSchedules as useStore } from 'store/slices';

import { ApplyButton } from './ApplyButton';
import * as styled from './styled';

export interface IActivityOpeningProps {
  name?: string;
  opening: IActivityOpeningModel;
}

export const ActivityOpening: React.FC<IActivityOpeningProps> = ({
  opening,
  name = opening.name,
}) => {
  const api = useApi();
  const padlock = usePadlock();
  const [state, store] = useStore();

  const [answer, setAnswer] = React.useState('');
  const [loading, setLoading] = React.useState(false);

  const userId = padlock.identity?.uid;
  const application = opening.applications.find((a) => a.userId === userId);
  const alreadyFilled = opening.limit <= opening.applications.length || !!application;
  const canApply =
    !alreadyFilled &&
    padlock.hasClaim(opening.requirements.map((r) => ({ name: r.name, value: r.value })));

  React.useEffect(() => {
    setAnswer(application?.message ?? '');
  }, [application]);

  const handleApplication = async () => {
    try {
      setLoading(true);
      if (!state.schedule) throw new Error('The schedule was not found');
      const open = { ...opening };

      if (!application) {
        const model: IApplicationModel = {
          id: 0,
          userId: padlock.identity!.uid,
          openingId: opening.id,
          message: answer,
        };
        const res = await api.applications.add(model);
        open.applications = [...open.applications, res];
      } else {
        await api.applications.remove(application);
        open.applications = open.applications.filter((a) => a.id !== application!.id);
      }
      store.updateOpening(open);
    } catch (ex: any | AxiosError) {
      toast.error(ex.response.data.error);
      if (ex.response.data.error === 'Opening has already been filled') {
        api.events
          .getPage(state.schedule!.id, { quantity: 1000 })
          .then((res) => {
            store.storeSchedule({ ...state.schedule!, events: res.items });
          })
          .catch((error) => {
            toast.error(error.message);
          });
      }
    } finally {
      setLoading(false);
    }
  };

  const handleUpdateApplication = async (application: IApplicationModel) => {
    try {
      setLoading(true);
      const open = { ...opening };
      application = await api.applications.update({ ...application, message: answer });
      open.applications = [
        ...open.applications.filter((a) => a.id !== application.id),
        application,
      ];
      store.updateOpening(open);
    } catch (ex: any | AxiosError) {
      toast.error(ex.response.data.error);
    } finally {
      setLoading(false);
    }
  };

  return (
    <styled.ActivityOpening className="opening">
      <Show on={!!name}>
        <div>
          <span>{name}</span>
        </div>
      </Show>
      {opening.applications.map((a) => (
        <div key={a.id} className="applicant">
          <Show on={loading && a.userId === userId}>
            <Spinner />
          </Show>
          <Show on={!loading || a.userId !== userId}>
            <span>{a.user?.displayName}</span>
            <Show on={a.userId === padlock.identity?.uid}>
              <Show on={!!opening.question}>
                <div className="question">
                  <label htmlFor={`message-${opening.id}`}>{opening.question}</label>
                  <Text
                    id={`message-${opening.id}`}
                    name="message"
                    variant={TextVariant.primary}
                    onChange={(e) => setAnswer(e.currentTarget.value)}
                    value={answer}
                  ></Text>
                  <Button
                    title="update"
                    className="icon"
                    onClick={() => handleUpdateApplication(a)}
                  >
                    <FaArrowAltCircleRight size={30} />
                  </Button>
                </div>
              </Show>
              <ApplyButton active={true} onClick={handleApplication} />
            </Show>
            <Show on={a.userId !== padlock.identity?.uid && !!a.message}>
              <p>{a.message}</p>
            </Show>
          </Show>
        </div>
      ))}
      <Show on={canApply}>
        <div className="applicant">
          <Show on={loading}>
            <Spinner />
          </Show>
          <Show on={!loading}>
            <Show on={!!opening.question}>
              <div className="question">
                <label htmlFor={`message-${opening.id}`}>{opening.question}</label>
                <Text
                  id={`message-${opening.id}`}
                  name="message"
                  variant={TextVariant.primary}
                  onChange={(e) => setAnswer(e.currentTarget.value)}
                  value={answer}
                />
              </div>
            </Show>
            <ApplyButton onClick={handleApplication} />
          </Show>
        </div>
      </Show>
    </styled.ActivityOpening>
  );
};
