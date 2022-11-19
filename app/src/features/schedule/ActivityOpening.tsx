import { Dialog } from '@headlessui/react';
// eslint-disable-next-line @typescript-eslint/no-unused-vars
import { AxiosError } from 'axios';
import {
  Button,
  ButtonVariant,
  Col,
  Dropdown,
  Row,
  Show,
  Spinner,
  TextArea,
  TextVariant,
} from 'components';
import {
  IActivityOpeningModel,
  IApplicationModel,
  IOpeningMessageModel,
  useApi,
  usePadlock,
} from 'hooks';
import moment from 'moment-timezone';
import React from 'react';
import { FaEdit, FaRegLightbulb } from 'react-icons/fa';
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

  const [loading, setLoading] = React.useState(false);
  const [answer, setAnswer] = React.useState('');
  const [showQuestion, setShowQuestion] = React.useState(false);
  const [request, setRequest] = React.useState('');
  const [showRequestMessage, setShowRequestMessage] = React.useState(false);
  const [showMessage, setShowMessage] = React.useState(false);
  const [message, setMessage] = React.useState<IOpeningMessageModel>();

  const userId = padlock.identity?.uid;
  const application = opening.applications.find((a) => a.userId === userId);
  const alreadyFilled = opening.limit <= opening.applications.length || !!application;
  const canApply =
    !alreadyFilled &&
    padlock.hasClaim(opening.requirements.map((r) => ({ name: r.name, value: r.value })));

  React.useEffect(() => {
    setAnswer(application?.message ?? '');
  }, [application]);

  const onApply = async () => {
    if (!!opening.question && !application) {
      setShowQuestion(true);
    } else {
      await handleApplication();
    }
  };

  const onWithdraw = async () => {
    await handleApplication();
  };

  const onEdit = async () => {
    setShowQuestion(true);
  };

  const handleApplication = async () => {
    try {
      setShowQuestion(false);
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
        toast.success(
          `Thank you for volunteering to ${opening.name} on ${moment(
            opening.activity!.startOn,
          ).format('MMM DD')}`,
        );
      } else {
        await api.applications.remove(application);
        open.applications = open.applications.filter((a) => a.id !== application!.id);
        toast.info(
          `Your name has been removed from ${opening.name} on ${moment(
            opening.activity!.startOn,
          ).format('MMM DD')}`,
        );
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
      setShowQuestion(false);
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

  const onSendRequestMessage = async () => {
    try {
      setShowRequestMessage(false);
      setLoading(true);
      const model: IOpeningMessageModel = {
        id: 0,
        openingId: opening.id,
        ownerId: userId ?? 0,
        message: request,
      };
      await api.openings.add(model);
      toast.success('Your message has been sent');
    } catch (ex: any | AxiosError) {
      toast.error(ex.response.data.error);
    } finally {
      setLoading(false);
    }
  };

  return (
    <styled.ActivityOpening className="opening">
      <Col>
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
                  <Row>
                    <p>{!!a.message ? `"${a.message}"` : 'TBA'}</p>
                    <FaEdit size={25} className="btn edit" onClick={onEdit} />
                  </Row>
                </Show>
                <ApplyButton active={true} onClick={onWithdraw} />
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
              <ApplyButton onClick={onApply} />
            </Show>
          </div>
        </Show>
      </Col>
      <Show
        on={
          !!opening.question &&
          (!opening.applications.length || application?.userId !== padlock.identity?.uid)
        }
      >
        <div className="message">
          <Show on={loading}>
            <div>
              <Spinner />
            </div>
          </Show>
          <Show on={!loading}>
            <Button
              className="btn request"
              variant={ButtonVariant.secondary}
              onClick={() => setShowRequestMessage(true)}
            >
              Make Request
              <FaRegLightbulb size={25} />
            </Button>
          </Show>
        </div>
      </Show>
      <Show on={showQuestion}>
        <Dialog open={true} onClose={() => setShowQuestion(false)} className="dialog">
          <div className="panel">
            <Dialog.Panel>
              <Dialog.Title>{opening.activity?.name}</Dialog.Title>
              <Show on={!!opening.activity?.event?.series}>
                <p>This opening is part of a series "{opening.activity?.event?.series?.name}".</p>
                <p>Choose a subject that is related.</p>
                <p>Lead the whole series, or share it with others.</p>
              </Show>
              <div>
                <Col>
                  <Show on={!!opening.messages?.length}>
                    <label>View Requests From:</label>
                    <Row style={{ gap: 0 }}>
                      <Dropdown
                        name="messages"
                        options={[{ id: 0 } as IOpeningMessageModel, ...opening.messages]}
                        optionValue="id"
                        optionText={(v) => `${v.owner?.displayName ?? ''}`}
                        onChange={(e) => {
                          const id = parseInt(e.currentTarget.value);
                          const message = opening.messages.find((m) => m.id === id);
                          setMessage(message);
                        }}
                      ></Dropdown>
                      <Button onClick={() => setShowMessage(true)} disabled={!message?.id}>
                        View
                      </Button>
                    </Row>
                  </Show>
                  <label htmlFor="answer">{opening.question}</label>
                  <TextArea
                    id={`answer-${opening.id}`}
                    name="answer"
                    variant={TextVariant.primary}
                    onChange={(e) => setAnswer(e.currentTarget.value)}
                    value={answer}
                  />
                  <Button
                    onClick={() =>
                      !!application ? handleUpdateApplication(application) : handleApplication()
                    }
                  >
                    Apply
                  </Button>
                </Col>
              </div>
            </Dialog.Panel>
          </div>
        </Dialog>
      </Show>
      <Show on={showRequestMessage}>
        <Dialog open={true} onClose={() => setShowRequestMessage(false)} className="dialog">
          <div className="panel">
            <Dialog.Panel>
              <Dialog.Title>Send a Message</Dialog.Title>
              <p>Interested in a subject?</p>
              <p>Have a question?</p>
              <p>Send a request to the volunteer.</p>
              <div>
                <Col>
                  <label htmlFor="request">Message:</label>
                  <TextArea
                    id={`request-${opening.id}`}
                    name="request"
                    variant={TextVariant.primary}
                    onChange={(e) => setRequest(e.currentTarget.value)}
                    value={request}
                  />
                  <Button onClick={onSendRequestMessage}>Send</Button>
                </Col>
              </div>
            </Dialog.Panel>
          </div>
        </Dialog>
      </Show>
      <Show on={showMessage}>
        <Dialog open={true} onClose={() => setShowMessage(false)} className="dialog">
          <div className="panel">
            <Dialog.Panel>
              <Dialog.Title>Message</Dialog.Title>
              <p>From: {message?.owner?.displayName}</p>
              <div>
                <Col>
                  <label htmlFor="message">Message:</label>
                  <TextArea
                    name="message"
                    variant={TextVariant.primary}
                    value={message?.message}
                    readOnly
                  />
                  <Button onClick={() => setShowMessage(false)}>Close</Button>
                </Col>
              </div>
            </Dialog.Panel>
          </div>
        </Dialog>
      </Show>
    </styled.ActivityOpening>
  );
};
