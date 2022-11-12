import { Button, FormikText } from 'components';
import { Formik } from 'formik';
import { useApi, usePadlock } from 'hooks';
import { useNavigate } from 'react-router-dom';

import { IParticipantLoginForm, IUserLoginForm } from '.';
import * as styled from './LoginStyled';

/**
 * Login will display content for an anonymous user.
 * If the user is already authenticated it will redirect to the home route.
 * @returns Login component.
 */
export const Login = () => {
  const auth = usePadlock();
  const api = useApi();
  const navigate = useNavigate();
  const redirect_uri = new URLSearchParams(window.location.search).get('redirect_uri');

  const defaultParticipantValues: IParticipantLoginForm = { key: '' };
  const defaultUserValues: IUserLoginForm = { username: '', password: '' };

  return (
    <styled.Login>
      <div>
        <div>
          <p>Scheduling for teams</p>
        </div>
        <div>
          <Formik
            initialValues={defaultParticipantValues}
            onSubmit={async (values) => {
              try {
                if (values.key) {
                  const token = await api.auth.loginAsParticipant({ key: values.key });
                  auth.login(token);
                  navigate(redirect_uri ?? '/');
                }
              } catch (error) {
                // Handle error
              }
            }}
          >
            {({ values, handleChange, handleBlur, handleSubmit, isSubmitting }) => (
              <form id="participant" onSubmit={handleSubmit}>
                <FormikText
                  name="key"
                  onChange={handleChange}
                  onBlur={handleBlur}
                  value={values.key}
                  placeholder="Paste your participant key"
                ></FormikText>
                <Button type="submit" disabled={isSubmitting || !values.key}>
                  Login
                </Button>
              </form>
            )}
          </Formik>
        </div>
        <div>
          <p>Or login with your user account</p>
        </div>
        <div>
          <Formik
            initialValues={defaultUserValues}
            onSubmit={async (values) => {
              try {
                if (values.username && values.password) {
                  const token = await api.auth.login({
                    username: values.username,
                    password: values.password,
                  });
                  auth.login(token);
                  navigate(redirect_uri ?? '/');
                }
              } catch (error) {
                // Handle error
              }
            }}
          >
            {({ values, handleChange, handleBlur, handleSubmit, isSubmitting }) => (
              <form id="user" onSubmit={handleSubmit}>
                <FormikText
                  name="username"
                  onChange={handleChange}
                  onBlur={handleBlur}
                  value={values.username}
                  placeholder="Username or Email"
                ></FormikText>
                <FormikText
                  name="password"
                  type="password"
                  onChange={handleChange}
                  onBlur={handleBlur}
                  value={values.password}
                  placeholder="Enter your password"
                  autoComplete="on"
                ></FormikText>
                <Button
                  type="submit"
                  disabled={isSubmitting || !values.username || !values.password}
                >
                  Login
                </Button>
              </form>
            )}
          </Formik>
        </div>
      </div>
    </styled.Login>
  );
};
